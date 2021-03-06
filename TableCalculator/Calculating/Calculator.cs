using Antlr4.Runtime;
using System.Collections.Generic;
using System;

namespace TableCalculator.Calculating
{
    /// <summary>
    /// калькулятор для обчислення виразів
    /// </summary>
    public class Calculator
    {
        /// поточний відвідувач
        private readonly CalculatorVisitor _visitor;

        public Calculator(Func<string, double> getValue)
            => _visitor = new(getValue);

        /// <summary>
        /// від яких комірок залежить заданий вираз
        /// </summary>
        /// <param name="expression">заданий вираз</param>
        /// <returns>шуканий список комірок</returns>
        public List<string> Depends(string expression)
        {
            var lexer = new CalculatorLexer(new AntlrInputStream(expression.ToUpper()));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ErrorListener());
            var tokens = new CommonTokenStream(lexer);
            var parser = new CalculatorParser(tokens);
            parser.compileUnit();
            if (parser.NumberOfSyntaxErrors != 0)
                throw new SyntaxErrorException();
            List<string> res = new();
            foreach(var t in tokens.GetTokens())
            {
                string s = t.Text;
                if ('A' <= s[0] && s[0] <= 'Z' && '0' <= s[^1] && s[^1] <= '9')
                    res.Add(s);
            }
            return res;
        }

        /// <summary>
        /// обчислює вираз
        /// </summary>
        /// <param name="expression">заданий вираз</param>
        /// <returns>результат</returns>
        public double Evaluate(string expression)
        {
            var lexer = new CalculatorLexer(new AntlrInputStream(expression.ToUpper()));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ErrorListener());
            var tokens = new CommonTokenStream(lexer);
            var parser = new CalculatorParser(tokens);
            var tree = parser.compileUnit();
            if (parser.NumberOfSyntaxErrors != 0)
                throw new SyntaxErrorException();
            var res = _visitor.Visit(tree);
            return res;
        }
    }
}
