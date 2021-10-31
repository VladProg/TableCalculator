using System;
using System.Globalization;
using System.Threading;

namespace TableCalculator
{
    public static class Utils
    {
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
        public static string RowNumberToId(int num)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException(nameof(num));
            return (num + 1).ToString();
        }

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
        public static string CellNumbersToId(int column, int row)
        {
            return ColumnNumberToId(column) + RowNumberToId(row);
        }

        public static (int Column, int Row) CellIdToNumbers(string id)
        {
            for (int i = 0; i < id.Length; i++)
                if ('0' <= id[i] && id[i] <= '9')
                    return (ColumnIdToNumber(id[..i]), RowIdToNumber(id[i..]));
            throw new ArgumentException("Cell id is incorrect", nameof(id));
        }

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

        public static string DoubleToString(double d, int width)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (d == 0)
                return "0";
            int len = (int)(width / 8.2 - 1);
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
