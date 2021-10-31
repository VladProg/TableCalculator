using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TableCalculator;

namespace UnitTests
{
    [TestClass]
    public class UtilsTest
    {
        static void CheckId(int colNum, string colId, int rowNum, string rowId)
        {
            Assert.AreEqual(colId.ToUpper(), Utils.ColumnNumberToId(colNum), "Utils.ColumnNumberToId({0})", colNum);
            Assert.AreEqual(colNum, Utils.ColumnIdToNumber(colId), "Utils.ColumnIdToNumber({0})", colId);
            Assert.AreEqual(rowId.ToUpper(), Utils.RowNumberToId(rowNum), "Utils.RowNumberToId({0})", rowNum);
            Assert.AreEqual(rowNum, Utils.RowIdToNumber(rowId), "Utils.RowIdToNumber({0})", rowId);
            Assert.AreEqual((colId + rowId).ToUpper(), Utils.CellNumbersToId(colNum, rowNum), "Utils.CellNumbersToId({0}, {1})", colNum, rowNum);
            Assert.AreEqual((colNum, rowNum), Utils.CellIdToNumbers(colId + rowId), "Utils.CellIdToNumbers({0})", colId + rowId);
        }

        [TestMethod]
        public void TestId()
        {
            CheckId(0, "A", 0, "1");
            CheckId(0, "a", 1, "2");
            CheckId(1, "B", 9, "10");
            CheckId(25, "z", 10, "11");
            CheckId(26, "Aa", 123, "124");
            CheckId(27, "aB", 1111, "1112");
            CheckId(26 + 26, "ba", 999999999, "1000000000");
            CheckId(26 + 26 * 26, "AaA", 1000000000, "1000000001");
        }

        [TestMethod]
        public void TestIdExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Utils.ColumnNumberToId(-1));

            Assert.ThrowsException<ArgumentException>(() => Utils.ColumnIdToNumber("1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.ColumnIdToNumber("A1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.ColumnIdToNumber("+-"));
            Assert.ThrowsException<ArgumentException>(() => Utils.ColumnIdToNumber(""));
            Assert.ThrowsException<ArgumentException>(() => Utils.ColumnIdToNumber("AAAAAAA"));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Utils.RowNumberToId(-1));

            Assert.ThrowsException<ArgumentException>(() => Utils.RowIdToNumber("A"));
            Assert.ThrowsException<ArgumentException>(() => Utils.RowIdToNumber("A1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.RowIdToNumber("1.1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.RowIdToNumber("-1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.RowIdToNumber("#"));

            Assert.ThrowsException<ArgumentException>(() => Utils.CellIdToNumbers("A1A"));
            Assert.ThrowsException<ArgumentException>(() => Utils.CellIdToNumbers("1A1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.CellIdToNumbers("1"));
            Assert.ThrowsException<ArgumentException>(() => Utils.CellIdToNumbers("A"));
            Assert.ThrowsException<ArgumentException>(() => Utils.CellIdToNumbers("!"));
        }
    }
}
