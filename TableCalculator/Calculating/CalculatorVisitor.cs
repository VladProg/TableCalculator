using Antlr4.Runtime.Misc;
using System;
using System.Linq;

namespace TableCalculator.Calculating
{
    /// <summary>
    /// патерн "відвідувач"
    /// </summary>
    internal class CalculatorVisitor : CalculatorBaseVisitor<double>
    {
        private readonly Func<string, double> _getValue;

        public CalculatorVisitor(Func<string, double> getValue)
            => _getValue = getValue;

        public override double VisitCompileUnit([NotNull] CalculatorParser.CompileUnitContext context)
            => Visit(context.expression());

        public override double VisitMmaxExpr([NotNull] CalculatorParser.MmaxExprContext context)
            => context.expression().ToList().ConvertAll(expr => Visit(expr)).Max();

        public override double VisitMminExpr([NotNull] CalculatorParser.MminExprContext context)
            => context.expression().ToList().ConvertAll(expr => Visit(expr)).Min();

        public override double VisitParenthesizedExpr([NotNull] CalculatorParser.ParenthesizedExprContext context)
            => Visit(context.expression());

        public override double VisitExponentialExpr([NotNull] CalculatorParser.ExponentialExprContext context)
            => Math.Pow(Visit(context.expression(0)), Visit(context.expression(1)));

        public override double VisitMultiplicativeExpr([NotNull] CalculatorParser.MultiplicativeExprContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));
            return context.operatorToken.Type switch
            {
                CalculatorLexer.MULTIPLY => left * right,
                CalculatorLexer.DIVIDE => left / right,
                CalculatorLexer.DIV => Math.Floor(left / right),
                CalculatorLexer.MOD => left - Math.Floor(left / right) * right,
                _ => throw new NotImplementedException(),
            };
        }

        public override double VisitAdditiveExpr([NotNull] CalculatorParser.AdditiveExprContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));
            return context.operatorToken.Type switch
            {
                CalculatorLexer.ADD => left + right,
                CalculatorLexer.SUBTRACT => left - right,
                _ => throw new NotImplementedException(),
            };
        }

        public override double VisitNumberExpr([NotNull] CalculatorParser.NumberExprContext context)
            => double.Parse(context.GetText());

        public override double VisitIdentifierExpr([NotNull] CalculatorParser.IdentifierExprContext context)
            => _getValue(context.GetText());
    }
}
