using Antlr4.Runtime;

namespace TableCalculator.Calculating
{
    /// <summary>
    /// описує дії, які треба робити при виявленні помилки у виразі
    /// </summary>
    internal class ErrorListener: IAntlrErrorListener<int>
    {
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            => throw new SyntaxErrorException();
    }
}
