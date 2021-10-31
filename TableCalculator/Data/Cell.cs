namespace TableCalculator.Data
{
    /// <summary>
    /// комірка таблиці
    /// </summary>
    internal class Cell
    {
        /// <summary>
        /// string - вираз, що записаний у задану комірку
        /// </summary>
        public string Expression;

        /// <summary>
        /// значення, що знаходиться у цій комірці
        /// double? - це значення або null, якщо це циклічне посилання
        /// </summary>
        public double? Result;

        public Cell(string expression, double? result)
        {
            Expression = expression;
            Result = result;
        }
    }
}
