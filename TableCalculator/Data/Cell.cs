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

        public void SetExpression(string expression) => Expression = expression;
        public void SetResult(double? result) => Result = result;
    }
}
