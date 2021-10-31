using System;

namespace TableCalculator.Data
{
    /// <summary>
    /// комірки, на яку посилається формула, не існує
    /// </summary>
    public class WrongReferenceException : Exception
    {
        /// <summary>
        /// ім'я комірки
        /// </summary>
        public readonly string CellId;

        public WrongReferenceException(string cellId)
            => CellId = cellId;
    }
}
