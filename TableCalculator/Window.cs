using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TableCalculator.Calculating;
using TableCalculator.Data;

namespace TableCalculator
{
    public partial class Window : Form
    {
        private Table _table;
        private bool _wasCycle = false;

        private bool Lock(object except, Control cur = null)
        {
            if (cur is null)
                cur = this;
            if (cur == except)
                return true;
            bool found = false;
            foreach (Control to in cur.Controls)
                found |= Lock(except, to);
            if (!found)
                cur.Enabled = false;
            return found;
        }

        private void Unlock(Control cur = null)
        {
            if (cur is null)
                cur = this;
            foreach (Control to in cur.Controls)
                Unlock(to);
            cur.Enabled = true;
        }

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
            dataGridView.SelectionChanged -= dataGridView_SelectionChanged;
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
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            Unlock();
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
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb)
            {
                tb.PreviewKeyDown -= dataGridView_TextBox_PreviewKeyDown;
                tb.PreviewKeyDown += dataGridView_TextBox_PreviewKeyDown;
                tb.KeyDown -= dataGridView_TextBox_KeyDown;
                tb.KeyDown += dataGridView_TextBox_KeyDown;
                tb.TabStop = false;
            }
        }

        private bool Apply(string expression)
        {
            if (Column() == -1 || Row() == -1)
                return true;
            try
            {
                List<string> changed = _table.ChangeOrDeleteCell(Utils.CellNumbersToId(Column(), Row()),
                                                                 expression);
                foreach (var id in changed)
                {
                    var (col, row) = Utils.CellIdToNumbers(id);
                    WriteToCell(col, row);
                }
                dataGridView_SelectionChanged(dataGridView, new());
                Unlock();
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
            if (!Apply(dataGridView.CurrentCell.Value as string))
            {
                void dataGridView_SelectionChanged_Error(object sender, EventArgs e1)
                {
                    dataGridView.CurrentCell = dataGridView[e.ColumnIndex, e.RowIndex];
                    dataGridView_SelectionChanged(dataGridView, new());
                    dataGridView.CellBeginEdit -= dataGridView_CellBeginEdit;
                    dataGridView.BeginEdit(true);
                    dataGridView.CellBeginEdit += dataGridView_CellBeginEdit;
                    dataGridView.SelectionChanged -= dataGridView_SelectionChanged_Error;
                };
                dataGridView.SelectionChanged += dataGridView_SelectionChanged_Error;
            }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            string id = CellId();
            textBoxId.Text = id;
            textBoxExpression.TextChanged -= textBoxExpression_TextChanged;
            textBoxExpression.Text = _table.Contains(id) ? _table.GetExpression(id) : "";
            textBoxExpression.TextChanged += textBoxExpression_TextChanged;
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int col = Column(),
                row = Row();
            if (col == -1 || row == -1)
                return;
            if (!dataGridView.Enabled)
                return;
            Lock(sender);
            string id = Utils.CellNumbersToId(col, row);
            textBoxId.Text = id;
            textBoxExpression.Text = dataGridView[col, row].Value as string;
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

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
        {
            if (!textBoxExpression.Enabled)
                return;
            Lock(sender);
            dataGridView.CurrentCell.Value = textBoxExpression.Text;
        }

        private void buttonAddColumn_Click(object sender, EventArgs e)
        {
            _table.ColumnCount++;
            int i = dataGridView.ColumnCount;
            dataGridView.Columns.Add(Utils.ColumnNumberToId(i), Utils.ColumnNumberToId(i));
            dataGridView.Columns[i].FillWeight = 1;
            dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            _table.RowCount++;
            int i = dataGridView.RowCount;
            dataGridView.Rows.Add();
            dataGridView.Rows[i].HeaderCell.Value = Utils.RowNumberToId(i);
        }

        private void buttonRemoveColumn_Click(object sender, EventArgs e)
        {
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

        private void textBoxExpression_Leave(object sender, EventArgs e)
        {
            if (Apply(textBoxExpression.Text))
            {
                dataGridView.Focus();
                dataGridView_SelectionChanged(dataGridView, new());
            }
            else
                textBoxExpression.Focus();
        }

        private void textBoxId_Leave(object sender, EventArgs e)
            => textBoxId.Text = CellId();

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
            sfd.Filter = "Таблиці|*.tc";
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
            => Save();

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
            => SaveAs();

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
            => e.Cancel = !menuStrip.Enabled || !CheckSaved();

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
            => Close();

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSaved())
                return;
            _table = new(5, 5);
            FillDataGrivView();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSaved())
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
            int col = e.Column.Index;
            for (int row = 0; row < dataGridView.RowCount; row++)
                    WriteToCell(col, row);
        }

        private void expressionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (expressionsToolStripMenuItem.Checked)
                return;
            expressionsToolStripMenuItem.Checked = true;
            valuesToolStripMenuItem.Checked = false;
            for (int col = 0; col < dataGridView.ColumnCount; col++)
                for (int row = 0; row < dataGridView.RowCount; row++)
                    WriteToCell(col, row);
            Unlock();
        }

        private void valuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (valuesToolStripMenuItem.Checked)
                return;
            expressionsToolStripMenuItem.Checked = false;
            valuesToolStripMenuItem.Checked = true;
            for (int col = 0; col < dataGridView.ColumnCount; col++)
                for (int row = 0; row < dataGridView.RowCount; row++)
                    WriteToCell(col, row);
            Unlock();
        }

        private void textBoxExpression_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter when Apply(dataGridView.CurrentCell.Value as string):
                    dataGridView.Focus();
                    dataGridView.CurrentCell = dataGridView[Column(), Math.Min(Row() + 1, dataGridView.RowCount - 1)];
                    dataGridView_SelectionChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Tab when Apply(dataGridView.CurrentCell.Value as string):
                    dataGridView.Focus();
                    if (Column() != dataGridView.ColumnCount - 1)
                        dataGridView.CurrentCell = dataGridView[Column() + 1, Row()];
                    else if (Row() != dataGridView.RowCount - 1)
                        dataGridView.CurrentCell = dataGridView[0, Row() + 1];
                    dataGridView_SelectionChanged(dataGridView, new());
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Escape:
                    Apply(_table.Contains(CellId()) ? _table.GetExpression(CellId()) : "");
                    dataGridView.Focus();
                    dataGridView_SelectionChanged(dataGridView, new());
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
                        dataGridView_SelectionChanged(dataGridView, new());
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

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug.WriteLine("dataGridView_KeyDown " + e.KeyCode);
            //e.Handled = true;
            //e.SuppressKeyPress = true;
        }

        private void dataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Debug.WriteLine("dataGridView_PreviewKeyDown " + e.KeyCode);
            //e.IsInputKey = true;
        }

        private void dataGridView_TextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Debug.WriteLine("dataGridView_TextBox_PreviewKeyDown " + e.KeyCode);
            //e.IsInputKey = false;
            if (e.KeyCode != Keys.Escape)
                return;
            dataGridView.CurrentCell.Value = _table.Contains(CellId()) ? _table.GetExpression(CellId()) : "";
        }

        private void dataGridView_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug.WriteLine("dataGridView_TextBox_KeyDown " + e.KeyCode);
            //e.Handled = true;
            //e.SuppressKeyPress = true;
        }
    }
}
