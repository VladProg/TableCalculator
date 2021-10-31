using System;
using System.Collections.Generic;
using System.IO;
using TableCalculator.Calculating;

namespace TableCalculator.Data
{
    /// <summary>
    /// таблиця: її розміри, вміст, файл
    /// </summary>
    public class Table
    {
        /// словник комірок: ім'я -> Cell
        private readonly Dictionary<string, Cell> _cells = new();

        /// поточний граф
        private readonly Graph _graph = new();

        /// поточний калькулятор
        private readonly Calculator _calculator;

        /// <summary>
        /// ім'я файлу, у якому збережена таблиця (або null, якщо таблицю ще не зберігали)
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// false, якщо в таблиці є незбережені зміни
        /// </summary>
        public bool Saved { get; private set; }

        public Table(int columnCount = 1, int rowCount = 1)
        {
            _calculator = new(id => _cells.ContainsKey(id) ? (double)_cells[id].Result : 0);
            ColumnCount = columnCount;
            RowCount = rowCount;
            Saved = true;
        }

        public Table(string fileName) : this()
        {
            FileName = fileName;
            using StreamReader sr = new(fileName);
            static (string, string) Split(string str,char delim)
            {
                int pos = str.IndexOf(delim);
                return (str[..pos], str[(pos + 1)..]);
            }
            var (col, row) = Split(sr.ReadLine(), 'x');
            ColumnCount = int.Parse(col);
            RowCount = int.Parse(row);
            for (string line = sr.ReadLine(); line is not null; line = sr.ReadLine())
            {
                if (line.Trim().Length == 0)
                    continue;
                var (id, expression) = Split(line, '=');
                id = id.Trim();
                if (expression.StartsWith(' '))
                    expression = expression[1..];
                ChangeOrDeleteCell(id, expression);
            }
            Saved = true;
        }

        /// <summary>
        /// зберегти таблицю в поточний файл
        /// </summary>
        public void Save()
        {
            if (FileName is null)
                throw new InvalidOperationException("Table was never saved before");
            if (Saved)
                return;
            using StreamWriter sw = new(FileName);
            sw.WriteLine(ColumnCount + " x " + RowCount);
            foreach (var (id, cell) in _cells)
                sw.WriteLine(id + " = " + cell.Expression);
            Saved = true;
        }

        /// <summary>
        /// зберегти таблицю в новий файл
        /// </summary>
        /// <param name="fileName">ім'я файлу, куди зберігати таблицю</param>
        public void SaveAs(string fileName)
        {
            FileName = fileName;
            Saved = false;
            Save();
        }

        private int _columnCount = 1;
        /// <summary>
        /// кількість стовпчиків таблиці
        /// </summary>
        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                Saved = false;
                if (_columnCount <= value)
                {
                    _columnCount = value;
                    return;
                }
                for (; _columnCount > value; _columnCount--)
                {
                    if (_columnCount == 1)
                        throw new ArgumentOutOfRangeException(nameof(value));
                    for (int row = 0; row < RowCount; row++)
                        if (Contains(Utils.CellNumbersToId(_columnCount - 1, row)))
                            throw new DeleteLineNotEmptyException();
                    for (int row = 0; row < RowCount; row++)
                        if (_graph.HasDependent(Utils.CellNumbersToId(_columnCount - 1, row)))
                            throw new DeleteLineDependentException();
                }
            }
        }

        private int _rowCount = 1;
        /// <summary>
        /// кількість рядків таблиці
        /// </summary>
        public int RowCount
        {
            get => _rowCount;
            set
            {
                Saved = false;
                if (_rowCount <= value)
                {
                    _rowCount = value;
                    return;
                }
                for (; _rowCount > value; _rowCount--)
                {
                    if (_rowCount == 1)
                        throw new ArgumentOutOfRangeException(nameof(value));
                    for (int col = 0; col < ColumnCount; col++)
                        if (Contains(Utils.CellNumbersToId(col, _rowCount - 1)))
                            throw new DeleteLineNotEmptyException();
                    for (int col = 0; col < ColumnCount; col++)
                        if (_graph.HasDependent(Utils.CellNumbersToId(col, _rowCount - 1)))
                            throw new DeleteLineDependentException();
                }
            }
        }

        /// <summary>
        /// чи записано щось у задану комірку
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <returns>true, якщо комірка непорожня</returns>
        public bool Contains(string id) => _cells.ContainsKey(id);

        /// <summary>
        /// вираз, що записаний у задану комірку
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <returns>string - цей вираз</returns>
        public string GetExpression(string id) => _cells[id].Expression;

        /// <summary>
        /// значення, що знаходиться у заданій комірці
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <returns>double? - це значення або null, якщо це циклічне посилання</returns>
        public double? GetResult(string id) => _cells[id].Result;

        /// <summary>
        /// чи існує задана комірка
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <returns>true, якщо координати комірки менші за розміри таблиці</returns>
        public bool Exsists(string id)
        {
            var (col, row) = Utils.CellIdToNumbers(id);
            return col < ColumnCount && row < RowCount;
        }

        /// <summary>
        /// змінює задану комірку
        /// якщо заданий вираз непорожній, то записує його в комірку
        /// якщо заданий вираз порожній, то видаляє запис із комірки
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <param name="expression">потрібний вираз</param>
        /// <returns>список комірок, що змінилися</returns>
        public List<string> ChangeOrDeleteCell(string id, string expression)
        {
            if (expression is not null && expression.Trim().Length > 0)
                return ChangeCell(id, expression);
            else
                return DeleteCell(id);
        }

        /// <summary>
        /// змінює задану комірку, записує в неї заданий вираз
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <param name="expression">потрібний вираз</param>
        /// <returns>список комірок, що змінилися</returns>
        private List<string> ChangeCell(string id, string expression)
        {
            if (!Exsists(id))
                throw new ArgumentOutOfRangeException(nameof(id));
            if (Contains(id) && GetExpression(id) == expression)
                return new();
            List<string> depends = _calculator.Depends(expression);
            foreach (var cur in depends)
                if (!Exsists(cur))
                    throw new WrongReferenceException(cur);
            _cells[id] = new(expression, null);
            List<string> changed = _graph.ChangeNode(id, depends);
            foreach (string cur in changed)
            {
                Cell cell = _cells[cur];
                if (_graph.IsCycle(cur))
                    cell.Result = null;
                else
                    cell.Result = _calculator.Evaluate(cell.Expression);
            }
            Saved = false;
            return changed;
        }

        /// <summary>
        /// змінює задану комірку, видаляє з неї запис
        /// </summary>
        /// <param name="id">ім'я цієї комірки</param>
        /// <returns>список комірок, що змінилися</returns>
        private List<string> DeleteCell(string id)
        {
            if (!Contains(id))
                return new();
            _cells.Remove(id);
            List<string> changed = _graph.ChangeNode(id, new());
            foreach (string cur in changed)
            {
                if (cur == id)
                    continue;
                Cell cell = _cells[cur];
                if (_graph.IsCycle(cur))
                    cell.Result = null;
                else
                    cell.Result = _calculator.Evaluate(cell.Expression);
            }
            Saved = false;
            return changed;
        }
    }
}
