namespace TableCalculator.Data
{
    internal class Cell
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
