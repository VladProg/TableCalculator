using System;
using System.Globalization;
using System.Threading;

namespace TableCalculator
{
    /// <summary>
    /// статичні методи, що не прив'язані до жодного класу
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// перетворює номер стовпчика на його ім'я
        /// </summary>
        /// <param name="num"> номер стовпчика (наприклад, 0)</param>
        /// <returns>ім'я стовпчика (наприклад, "A")</returns>
        public static string ColumnNumberToId(int num)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num));
            num++;
            int pow = 1;
            for (int len = 0; ; len++)
            {
                if (num < pow)
                {
                    string id = "";
                    for (int _ = 0; _ < len; _++)
                    {
                        id = (char)('A' + num % 26) + id;
                        num /= 26;
                    }
                    return id;
                }
                num -= pow;
                pow *= 26;
            }
        }

        /// <summary>
        /// перетворює ім'я стовпчика на його номер
        /// </summary>
        /// <param name="id"> ім'я стовпчика (наприклад, "A")</param>
        /// <returns>номер стовпчика (наприклад, 0)</returns>
        public static int ColumnIdToNumber(string id)
        {
            id = id.ToUpper();
            foreach (char c in id)
                if (c < 'A' || c > 'Z')
                    throw new ArgumentException("Id must contain only English letters", nameof(id));
            if (id.Length < 1 || id.Length > 6)
                throw new ArgumentException(nameof(id));
            id = id.ToUpper();
            int number = 0;
            int pow = 1;
            for (int i = id.Length - 1; i >= 0; i--)
            {
                number += pow;
                number += pow * (id[i] - 'A');
                pow *= 26;
            }
            return number - 1;
        }

        /// <summary>
        /// перетворює номер рядка на його ім'я
        /// </summary>
        /// <param name="num"> номер рядка (наприклад, 0)</param>
        /// <returns>ім'я рядка (наприклад, "1")</returns>
        public static string RowNumberToId(int num)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num));
            return (num + 1).ToString();
        }

        /// <summary>
        /// перетворює ім'я рядка на його номер
        /// </summary>
        /// <param name="id"> ім'я рядка (наприклад, "1")</param>
        /// <returns>номер рядка (наприклад, 0)</returns>
        public static int RowIdToNumber(string id)
        {
            try
            {
                int num = int.Parse(id) - 1;
                if (num < 0)
                    throw new ArgumentException(nameof(id));
                return num;
            }
            catch (FormatException fe)
            {
                throw new ArgumentException("Id is not a correct integer", nameof(id), fe);
            }
        }

        /// <summary>
        /// перетворює координати комірки на її ім'я
        /// </summary>
        /// <param name="column"> номер стовпчика (наприклад, 0)</param>
        /// <param name="row"> номер рядка (наприклад, 0)</param>
        /// <returns>ім'я комірки (наприклад, "A1")</returns>
        public static string CellNumbersToId(int column, int row)
        {
            return ColumnNumberToId(column) + RowNumberToId(row);
        }

        /// <summary>
        /// перетворює ім'я комірки на її координати
        /// </summary>
        /// <param name="id"> ім'я комірки (наприклад, "A")</param>
        /// <returns>пара з номера стовпчика і номера рядка (наприклад, (0, 0))</returns>
        public static (int Column, int Row) CellIdToNumbers(string id)
        {
            for (int i = 0; i < id.Length; i++)
                if ('0' <= id[i] && id[i] <= '9')
                    return (ColumnIdToNumber(id[..i]), RowIdToNumber(id[i..]));
            throw new ArgumentException("Cell id is incorrect", nameof(id));
        }

        /// <summary>
        /// перетворює double у string
        /// </summary>
        /// <param name="d">double, що треба перетворити</param>
        /// <param name="len">кількість цифр після десяткової крапки</param>
        /// <param name="format">формат - "E" (науковий) / "F" (фіксований)</param>
        /// <returns>рядкове представлення числа з видаленими зайвими символами</returns>
        private static string TryDoubleToString(double d, int len, string format)
        {
            string res = d.ToString(format + len.ToString());
            string end = "";
            if (res.Contains('E'))
            {
                int pos = res.IndexOf('E');
                end = res[pos..];
                res = res[..pos];
            }
            if(res.Contains('.'))
                while (res.EndsWith("0"))
                    res = res[0..^1];
            if (res.EndsWith("."))
                res = res[0..^1];
            if (res == "" || res == "-")
                return "0";
            while (end.Length >= 3 && end[2] == '0')
                end = end.Remove(2, 1);
            if (end.Length == 2)
                end = "";
            return res + end;
        }

        /// <summary>
        /// рахує кількість значущих цифр
        /// </summary>
        /// <param name="res">рядкове редставлення числа</param>
        /// <returns>кількість значущих цифр</returns>
        private static int SignificantDigits(string res)
        {
            int cnt = 0;
            foreach (char c in res)
                if (c == 'E')
                    return cnt;
                else if ('1' <= c && c <= '9' || c != '.' && cnt > 0)
                    cnt++;
            return cnt;
        }

        /// <summary>
        /// перетворює double у string
        /// </summary>
        /// <param name="d">double, що треба перетворити</param>
        /// <param name="width">ширина поля (DataGridView), у яке записується число</param>
        /// <returns></returns>
        public static string DoubleToString(double d, int width)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (d == 0)
                return "0";
            int len = (int)(width / 8.2 - 1); // приблизна кількість цифр, що вміщується у поле ширини width
            string ans = "…";
            for (int i = 0; i <= 15; i++)
            {
                string cur = TryDoubleToString(d, i, "E");
                if (cur != "0"
                        && cur.Length - (cur.Contains('.') ? 1 : 0) <= len
                        && SignificantDigits(cur) >= SignificantDigits(ans)
                        && SignificantDigits(cur) <= 15)
                    ans = cur;
            }
            for (int i = 0; i <= 15; i++)
            {
                string cur = TryDoubleToString(d, i, "F");
                if (cur != "0"
                        && cur.Length - (cur.Contains('.') ? 1 : 0) <= len
                        && SignificantDigits(cur) >= SignificantDigits(ans)
                        && SignificantDigits(cur) <= 15)
                    ans = cur;
            }
            return ans;
        }
    }
}
