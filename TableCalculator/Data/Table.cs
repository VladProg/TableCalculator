using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TableCalculator.Calculating;

namespace TableCalculator.Data
{
    class Table
    {
        private readonly Dictionary<string, Cell> _cells = new();
        private readonly Graph _graph = new();
        private readonly Calculator _calculator;
        public string FileName { get; private set; }
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

        public void Save()
        {
            if (FileName is null)
                throw new InvalidOperationException("File was never saved before");
            if (Saved)
                return;
            using StreamWriter sw = new(FileName);
            sw.WriteLine(ColumnCount + " x " + RowCount);
            foreach (var (id, cell) in _cells)
                sw.WriteLine(id + " = " + cell.Expression);
            Saved = true;
        }

        public void SaveAs(string fileName)
        {
            FileName = fileName;
            Saved = false;
            Save();
        }

        private int _columnCount = 1;

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
                        if (_graph.IsDependent(Utils.CellNumbersToId(_columnCount - 1, row)))
                            throw new DeleteLineDependentException();
                }
            }
        }

        private int _rowCount = 1;
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
                        if (_graph.IsDependent(Utils.CellNumbersToId(col, _rowCount - 1)))
                            throw new DeleteLineDependentException();
                }
            }
        }

        public bool Contains(string id) => _cells.ContainsKey(id);
        public string GetExpression(string id) => _cells[id].Expression;
        public double? GetResult(string id) => _cells[id].Result;

        public bool Exsists(string id)
        {
            var (col, row) = Utils.CellIdToNumbers(id);
            return col < ColumnCount && row < RowCount;
        }

        public List<string> ChangeOrDeleteCell(string id, string expression)
        {
            Saved = false;
            if (expression is not null && expression.Trim().Length > 0)
                return ChangeCell(id, expression);
            else
                return DeleteCell(id);
        }

        public List<string> ChangeCell(string id, string expression)
        {
            if (!Exsists(id))
                throw new ArgumentOutOfRangeException(nameof(id));
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
            return changed;
        }

        public List<string> DeleteCell(string id)
        {
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
            return changed;
        }
    }
}
