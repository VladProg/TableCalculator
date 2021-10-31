using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TableCalculator.Calculating;
using TableCalculator.Data;

namespace TableCalculator
{
    internal partial class Window : Form
    {
        private Table _table;
        private bool _wasCycle = false;

        private void WriteToCell(int col, int row)
        {
            if (dataGridView.CurrentCell is not null
                    && dataGridView.CurrentCell.ColumnIndex == col
                    && dataGridView.CurrentCell.RowIndex == row
                    && dataGridView.IsCurrentCellInEditMode)
                return;
            dataGridView.CellValueChanged -= dataGridView_CellValueChanged;
            string id = Utils.CellNumbersToId(col, row);
            DataGridViewCell cell = dataGridView[col, row];
            if (_table.Contains(id))
            {
                double? result = _table.GetResult(id);
                if (result is null && !_wasCycle)
                {
                    MessageBox.Show("Виявлені циклічні посилання. Комірки, що їх містять, позначені символом ⭯" +
                                    (expressionsToolStripMenuItem.Checked ? " в режимі відображення значень" : ""));
                    _wasCycle = true;
                }
                if (expressionsToolStripMenuItem.Checked)
                    cell.Value = _table.GetExpression(id);
                else if (result is null)
                    cell.Value = "⭯";
                else
                    cell.Value = Utils.DoubleToString((double)result, dataGridView.Columns[col].Width);
            }
            else
                cell.Value = "";
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
        }

        private int Column()
            => dataGridView.CurrentCell?.ColumnIndex ?? -1;

        private int Row()
            => dataGridView.CurrentCell?.RowIndex ?? -1;

        private string CellId()
            => Utils.CellNumbersToId(Column(),Row());

        private void FillDataGrivView()
        {
            dataGridView.CurrentCellChanged -= dataGridView_CurrentCellChanged;
            for (int col = dataGridView.ColumnCount - 1; col >= 0; col--)
                dataGridView.Columns.RemoveAt(col);
            for (int col = 0; col < _table.ColumnCount; col++)
            {
                dataGridView.Columns.Add(Utils.ColumnNumberToId(col), Utils.ColumnNumberToId(col));
                dataGridView.Columns[col].FillWeight = 1;
                dataGridView.Columns[col].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int row = 0; row < _table.RowCount; row++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[row].HeaderCell.Value = Utils.RowNumberToId(row);
            }
            for (int col = 0; col < _table.ColumnCount; col++)
                for (int row = 0; row < _table.RowCount; row++)
                    WriteToCell(col, row);
            dataGridView.CurrentCellChanged += dataGridView_CurrentCellChanged;
        }

        public Window()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                try
                {
                    _table = new(args[1]);
                }
                catch
                {
                    MessageBox.Show("Не вдалося відкрити файл");
                    Load += (s, e) => Close();
                    return;
                }
            else
                _table = new(5, 5);
            FillDataGrivView();
            dataGridView.Select();
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.PreviewKeyDown -= dataGridView_TextBox_PreviewKeyDown;
                tb.PreviewKeyDown += dataGridView_TextBox_PreviewKeyDown;
            }
        }

        private bool Apply(string expression)
        {
            if (Column() == -1 || Row() == -1)
                return true;
            try
            {
                List<string> changed = _table.ChangeOrDeleteCell(Utils.CellNumbersToId(Column(), Row()), expression);
                foreach (var id in changed)
                {
                    var (col, row) = Utils.CellIdToNumbers(id);
                    WriteToCell(col, row);
                }
                dataGridView_CurrentCellChanged(dataGridView, new());
                return true;
            }
            catch (SyntaxErrorException)
            {
                MessageBox.Show("Вираз містить помилку");
                return false;
            }
            catch (WrongReferenceException wre)
            {
                MessageBox.Show($"Вираз містить посилання на неіснуючу комірку \"{wre.CellId}\"");
                return false;
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex,
                row = e.RowIndex;
            if (Apply(dataGridView[col, row].Value as string))
                WriteToCell(e.ColumnIndex, e.RowIndex);
            else
            {
                dataGridView.CurrentCell = dataGridView[col, row];
                dataGridView.CellBeginEdit -= dataGridView_CellBeginEdit;
                dataGridView.BeginEdit(false);
                dataGridView.CellBeginEdit += dataGridView_CellBeginEdit;
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int col = Column(),
                row = Row();
            if (col == -1 || row == -1)
                return;
            string id = Utils.CellNumbersToId(col, row);
            textBoxId.Text = id;
            textBoxExpression.Text = dataGridView[col, row].Value as string;
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
            => dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int col = e.ColumnIndex,
                row = e.RowIndex;
            if (_table.Contains(Utils.CellNumbersToId(col, row)))
                dataGridView[col, row].Value = _table.GetExpression(Utils.CellNumbersToId(col, row));
            else
                dataGridView[col, row].Value = "";
        }

        private void textBoxExpression_TextChanged(object sender, EventArgs e)
            => dataGridView.CurrentCell.Value = textBoxExpression.Text;

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            dataGridView.Select();
            _table.ColumnCount++;
            int i = dataGridView.ColumnCount;
            dataGridView.Columns.Add(Utils.ColumnNumberToId(i), Utils.ColumnNumberToId(i));
            dataGridView.Columns[i].FillWeight = 1;
            dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            dataGridView.Select();
            _table.RowCount++;
            int i = dataGridView.RowCount;
            dataGridView.Rows.Add();
            dataGridView.Rows[i].HeaderCell.Value = Utils.RowNumberToId(i);
        }

        private void buttonRemoveColumn_Click(object sender, EventArgs e)
        {
            dataGridView.Select();
            try
            {
                _table.ColumnCount--;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Залишився лише один стовпчик, його не можна видалити");
                return;
            }
            catch (DeleteLineNotEmptyException)
            {
                MessageBox.Show("У стовпчику є непорожні комірки, його не можна видалити");
                return;
            }
            catch (DeleteLineDependentException)
            {
                MessageBox.Show("На комірки цього стовпчика посилаються інші комірки, його не можна видалити");
                return;
            }
            dataGridView.Columns.RemoveAt(dataGridView.Columns.Count - 1);
        }

        private void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            dataGridView.Select();
            try
            {
                _table.RowCount--;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Залишився лише один рядок, його не можна видалити");
                return;
            }
            catch (DeleteLineNotEmptyException)
            {
                MessageBox.Show("В рядку є непорожні комірки, його не можна видалити");
                return;
            }
            catch (DeleteLineDependentException)
            {
                MessageBox.Show("На комірки цього рядка посилаються інші комірки, його не можна видалити");
                return;
            }
            dataGridView.Rows.RemoveAt(dataGridView.Rows.Count - 1);
        }

        private void textBoxId_Leave(object sender, EventArgs e)
            => textBoxId.Text = CellId();

        bool CanCLick()
        {
            if (dataGridView.IsCurrentCellInEditMode)
            {
                textBoxExpression.Focus();
                dataGridView.Focus();
                return !dataGridView.IsCurrentCellInEditMode;
            }
            else if (textBoxExpression.Focused)
            {
                dataGridView.Focus();
                return !textBoxExpression.Focused;
            }
            return true;
        }

        bool Save()
        {
            if (_table.FileName is null)
                return SaveAs();
            else
                try
                {
                    _table.Save();
                    return true;
                }
                catch
                {
                    MessageBox.Show("Не вдалося зберегти файл");
                    return false;
                }
        }

        bool SaveAs()
        {
            SaveFileDialog sfd = new();
            sfd.Filter = "Таблиця|*.tc";
            sfd.Title = "Зберегти як";
            sfd.DefaultExt = "tc";
            if (_table.FileName is not null)
            {
                sfd.FileName = Path.GetFileName(_table.FileName);
                sfd.InitialDirectory = Path.GetDirectoryName(_table.FileName);
            }
            if (sfd.ShowDialog() == DialogResult.OK)
                try
                {
                    _table.SaveAs(sfd.FileName);
                    return true;
                }
                catch
                {
                    MessageBox.Show("Не вдалося зберегти файл");
                    return false;
                }
            else
                return false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick())
                return;
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick())
                return;
            SaveAs();
        }

        private bool CheckSaved()
        {
            if (_table is null || _table.Saved)
                return true;
            return MessageBox.Show("Файл не збережено. Зберегти його?", "", MessageBoxButtons.YesNoCancel) switch
            {
                DialogResult.Yes => Save(),
                DialogResult.No => true,
                DialogResult.Cancel => false,
                _ => throw new NotImplementedException()
            };
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
            => e.Cancel = dataGridView.IsCurrentCellInEditMode || textBoxExpression.Focused || !CheckSaved();

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick())
                return;
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick()||!CheckSaved())
                return;
            _table = new(5, 5);
            FillDataGrivView();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick()||!CheckSaved())
                return;
            OpenFileDialog ofd = new();
            ofd.Filter = "Таблиці|*.tc";
            ofd.Title = "Відкрити";
            ofd.DefaultExt = "tc";
            if (_table.FileName is not null)
                ofd.InitialDirectory = Path.GetDirectoryName(_table.FileName);
            if (ofd.ShowDialog() == DialogResult.OK)
                try
                {
                    _table = new(ofd.FileName);
                    FillDataGrivView();
                }
                catch
                {
                    MessageBox.Show("Не вдалося відкрити файл");
                }
        }

        private void dataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            for (int row = 0; row < dataGridView.RowCount; row++)
                WriteToCell(e.Column.Index, row);
        }

        private void expressionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick()||expressionsToolStripMenuItem.Checked)
                return;
            expressionsToolStripMenuItem.Checked = true;
            valuesToolStripMenuItem.Checked = false;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            for (int col = 0; col < dataGridView.ColumnCount; col++)
                for (int row = 0; row < dataGridView.RowCount; row++)
                {
                    dataGridView[col, row].Style.WrapMode = DataGridViewTriState.False;
                    WriteToCell(col, row);
                }
        }

        private void valuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CanCLick()||valuesToolStripMenuItem.Checked)
                return;
            expressionsToolStripMenuItem.Checked = false;
            valuesToolStripMenuItem.Checked = true;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int col = 0; col < dataGridView.ColumnCount; col++)
                for (int row = 0; row < dataGridView.RowCount; row++)
                {
                    dataGridView[col, row].Style.WrapMode = DataGridViewTriState.True;
                    WriteToCell(col, row);
                }
        }

        private void textBoxExpression_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter when Apply(dataGridView.CurrentCell.Value as string):
                    dataGridView.Focus();
                    dataGridView.CurrentCell = dataGridView[Column(), Math.Min(Row() + 1, dataGridView.RowCount - 1)];
                    dataGridView_CurrentCellChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Tab when e.Shift && Apply(dataGridView.CurrentCell.Value as string):
                    dataGridView.Focus();
                    if (Column() != 0)
                        dataGridView.CurrentCell = dataGridView[Column() - 1, Row()];
                    else if (Row() != 0)
                        dataGridView.CurrentCell = dataGridView[dataGridView.ColumnCount - 1, Row() - 1];
                    dataGridView_CurrentCellChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Tab when Apply(dataGridView.CurrentCell.Value as string):
                    dataGridView.Focus();
                    if (Column() != dataGridView.ColumnCount - 1)
                        dataGridView.CurrentCell = dataGridView[Column() + 1, Row()];
                    else if (Row() != dataGridView.RowCount - 1)
                        dataGridView.CurrentCell = dataGridView[0, Row() + 1];
                    dataGridView_CurrentCellChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Escape:
                    Apply(_table.Contains(CellId()) ? _table.GetExpression(CellId()) : "");
                    dataGridView.Focus();
                    dataGridView_CurrentCellChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try
                    {
                        var (col, row) = Utils.CellIdToNumbers(textBoxId.Text);
                        if (col >= dataGridView.ColumnCount || row >= dataGridView.RowCount)
                            throw new ArgumentException();
                        dataGridView.CurrentCell = dataGridView[col, row];
                        dataGridView_CurrentCellChanged(dataGridView, new());
                        dataGridView.Focus();
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Такої комірки не існує");
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Escape:
                    textBoxId.Text = CellId();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void dataGridView_TextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                dataGridView.CurrentCell.Value = _table.Contains(CellId()) ? _table.GetExpression(CellId()) : "";
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
            => e.Cancel = dataGridView.IsCurrentCellInEditMode && !Apply(e.FormattedValue as string);

        private void textBoxExpression_Validating(object sender, System.ComponentModel.CancelEventArgs e)
            => e.Cancel = !Apply(textBoxExpression.Text);

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell is null)
                return;
            string id = CellId();
            textBoxId.Text = id;
            textBoxExpression.TextChanged -= textBoxExpression_TextChanged;
            textBoxExpression.Text = _table.Contains(id) ? _table.GetExpression(id) : "";
            textBoxExpression.TextChanged += textBoxExpression_TextChanged;
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Apply("");
            if (e.KeyCode == Keys.Back)
            {
                Apply("");
                dataGridView.BeginEdit(true);
            }
        }

        private void possibilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new();
            form.Size = new(810, 540);
            RichTextBox rtb = new();
            form.Controls.Add(rtb);
            rtb.Dock = DockStyle.Fill;
            rtb.ReadOnly = true;
            rtb.Text = "Табличний калькулятор дозволяє обчислювати вирази і записувати їх значення в таблицю.\n" +
                       "\n" +
                       "Детальніше про формат виразів: \"Довідка\" → \"Формат виразів\".\n" +
                       "\n" +
                       "Вирази можна вводити і редагувати як у комірці, так і в полі над таблицею.\n" +
                       "Якщо ввести некоректний вираз, одразу з'явиться повідомлення про це.\n" +
                       "Циклічні посилання позначаються спеціальним символом \"⭯\".\n" +
                       "У лівому верхньому куті є поле з виділеною коміркою, його можна редагувати, щоб потрапити в іншу комірку.\n" +
                       "\n" +
                       "У правому верхньому куті є кнопки \"+\" і \"-\", щоб додавати і видаляти останній стовпчик відповідно.\n" +
                       "У лівому нижньому куті є кнопки \"+\" і \"-\", щоб додавати і видаляти останній рядок відповідно.\n" +
                       "\n" +
                       "Меню \"Файл\" дозволяє:\n" +
                       "• \"Створити\" – створити нову порожню таблицю,\n" +
                       "• \"Відкрити\" – відкрити таблицю з існуючого файла, \n" +
                       "• \"Зберегти\" – зберегти поточний файл, \n" +
                       "• \"Зберегти як\" – зберегти поточну таблицю в новий файл,\n" +
                       "• \"Закрити\" – закрити програму.\n" +
                       "\n" +
                       "Можна змінювати ширину стовпчиків, при цьому всі числа оруглюються, щоб не виходити за межі комірки.\n" +
                       "\n" +
                       "Меню \"Вигляд\" дозволяє змінювати інформацію, що відображається в комірках: \"Вирази\"/\"Значення\".\n" +
                       "\n" +
                       "У меню \"Довідка\" можна знайти інформацію про цю програму і про те, як нею користуватися.";
            form.Show();
        }

        private void expressionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form form = new();
            form.Size = new(700, 320);
            RichTextBox rtb = new();
            form.Controls.Add(rtb);
            rtb.Dock = DockStyle.Fill;
            rtb.ReadOnly = true;
            rtb.Text = "При побудові виразів використовуються:\n" +
                       "1. цілі числа довільної довжини\n" +
                       "2. круглі дужки\n" +
                       "3. імена клітинок (напр., А3)\n" +
                       "4. операції та функції, які для кожного із варіантів лабораторної роботи визначаються окремо.\n" +
                       "\n" +
                       "У мене варіант 7 з набором операцій 1, 2, 4, 7:\n" +
                       "1) +, -, *, / (бінарні операції);\n" +
                       "2) mod, dіv;\n" +
                       "4) ^ (піднесення у степінь);\n" +
                       "7) mmax(x1,x2,...,xN), mmіn(x1,x2,...,xN) (N>=1).\n" +
                       "\n" +
                       "Всі літери можна вводити у будь-якому регістрі.";
            form.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new();
            form.Size = new(450, 120);
            RichTextBox rtb = new();
            form.Controls.Add(rtb);
            rtb.Dock = DockStyle.Fill;
            rtb.ReadOnly = true;
            rtb.Text = "Табличний калькулятор – версія 1.0.\n" +
                       "Розробник – Владислав Заводник\n" +
                       "Вихідний код – https://github.com/VladProg/TableCalculator.";
            rtb.LinkClicked += (sender, e) => Process.Start("explorer.exe", e.LinkText);
            form.Show();
        }
    }
}
