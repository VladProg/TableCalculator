using Antlr4.Runtime;
using System;

namespace TableCalculator.Calculating
{
    class ErrorListener: IAntlrErrorListener<int>
    {
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            => throw new SyntaxErrorException();
    }
}
