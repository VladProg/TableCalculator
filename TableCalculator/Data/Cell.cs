namespace TableCalculator.Data
{
    class Cell
    {
        public string Expression;
        public double? Result;

        public Cell(string expression, double? result)
        {
            Expression = expression;
            Result = result;
        }
    }
}
