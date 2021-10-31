using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TableCalculator.Calculating;

namespace UnitTests
{
    [TestClass]
    public class CalculatorTest
    {
        private Dictionary<string, double> _values;
        private readonly Calculator _calculator;

        public CalculatorTest() => _calculator = new(id => _values[id]);

        private void CheckExpression(string expression, double result, Dictionary<string, double> values = null)
        {
            if (values is null)
                values = new();
            _values = values;

            HashSet<string> valuesKeys = new(values.Keys);
            List<string> calculatorDepends = _calculator.Depends(expression);
            Assert.IsTrue(valuesKeys.SetEquals(calculatorDepends),
                          "Calculator.Depends(\"{0}\"). Expected: \"{1}\". Got: \"{2}\".",
                          expression, string.Join(", ", valuesKeys), string.Join(", ", calculatorDepends));

            double calculatorEvaluate = _calculator.Evaluate(expression);
            Assert.AreEqual(result, calculatorEvaluate, 1e-9,
                            "Calculator.Evaluate(\"{0}\").", expression);
        }

        private void CheckSyntaxError(string expression)
        {
            _values = null;
            Assert.ThrowsException<SyntaxErrorException>(() => _calculator.Depends(expression),
                                                         "Calculator.Depends(\"{0}\").", expression);
            Assert.ThrowsException<SyntaxErrorException>(() => _calculator.Evaluate(expression),
                                                         "Calculator.Evaluate(\"{0}\").", expression);
        }

        [TestMethod]
        public void Tokens()
        {
            CheckExpression("123", 123);
            CheckExpression("-123", -123);
            CheckExpression("+123", 123);
            CheckExpression("ABC123", 456, new() { { "ABC123", 456 } });
            CheckSyntaxError("Á1");
            CheckSyntaxError("1A");
            CheckSyntaxError("A");
            CheckSyntaxError("A1A");
            CheckSyntaxError("1A1");

            string expression = "ABC123";
            _values = null;

            HashSet<string> valuesKeys = new() { "ABC123" };
            List<string> calculatorDepends = _calculator.Depends(expression);
            Assert.IsTrue(valuesKeys.SetEquals(calculatorDepends),
                          "Calculator.Depends(\"{0}\"). Expected: \"{1}\". Got: \"{2}\".",
                          expression, string.Join(", ", valuesKeys), string.Join(", ", calculatorDepends));

            Assert.ThrowsException<NullReferenceException>(() => _calculator.Evaluate(expression),
                                                           "Calculator.Evaluate(\"{0}\").", expression);
        }

        [TestMethod]
        public void Operators()
        {
            CheckExpression("24+10", 34);
            CheckExpression("24-10", 14);
            CheckExpression("24*10", 240);
            CheckExpression("24/10", 2.4);
            CheckExpression("24 div 10", 2);
            CheckExpression("24 mod 10", 4);
            CheckExpression("-24 div 10", -3);
            CheckExpression("-24 mod 10", 6);
            CheckExpression("24 div -10", -3);
            CheckExpression("24 mod -10", -6);
            CheckExpression("-24 div -10", 2);
            CheckExpression("-24 mod -10", -4);
            CheckExpression("24^10", 63403380965376);
            CheckExpression("mmin(24)", 24);
            CheckExpression("mmax(24)", 24);
            CheckExpression("mmin(24,10)", 10);
            CheckExpression("mmax(24,10)", 24);
            CheckExpression("mmin(24,10,-24,-10)", -24);
            CheckExpression("mmax(24,10,-24,-10)", 24);
        }

        [TestMethod]
        public void SyntaxErrors()
        {
            CheckSyntaxError("");
            CheckSyntaxError("1.1");
            CheckExpression("+1", 1);
            CheckSyntaxError("++1");
            CheckSyntaxError("1++");
            CheckSyntaxError("()");
            CheckSyntaxError("(1");
            CheckSyntaxError(")1(");
            CheckSyntaxError("*");
            CheckSyntaxError("1*");
            CheckSyntaxError("*1");
            CheckSyntaxError("*1*");
            CheckSyntaxError("mmax()");
            CheckSyntaxError("mmax 1,2");
            CheckSyntaxError("mmax(1 2)");
            CheckSyntaxError("1+1;");
        }

        [TestMethod]
        public void OperatorPrecedence()
        {
            CheckExpression("(3+4)*5", 35);
            CheckExpression("3+(4*5)", 23);
            CheckExpression("3+4*5", 23);

            CheckExpression("(3-4)*5", -5);
            CheckExpression("3-(4*5)", -17);
            CheckExpression("3-4*5", -17);

            CheckExpression("(3+4)/5", 1.4);
            CheckExpression("3+(4/5)", 3.8);
            CheckExpression("3+4/5", 3.8);

            CheckExpression("(3-4)/5", -0.2);
            CheckExpression("3-(4/5)", 2.2);
            CheckExpression("3-4/5", 2.2);

            CheckExpression("(4*5)^2", 400);
            CheckExpression("4*(5^2)", 100);
            CheckExpression("4*5^2", 100);

            CheckExpression("(2^2)^3", 64);
            CheckExpression("2^(2^3)", 256);
            CheckExpression("2^2^3", 64);

            CheckExpression("(-3)^2", 9);
            CheckSyntaxError("-(3^2)");
            CheckExpression("-3^2", 9);

            CheckExpression("(-3)^3", -27);
            CheckSyntaxError("-(3^3)");
            CheckExpression("-3^3", -27);
        }
    
        [TestMethod]
        public void ComplexExpressions()
        {
            CheckExpression("(A3^2+A4^2)^A5", 5, new() { { "A3", 3 }, { "A4", 4 }, { "A5", 0.5 } });
            CheckExpression("HELLO256-HELLO256", 0, new() { { "HELLO256", 324324732.984356432 } });
            CheckExpression("mmax(1^4,2^3,3^2,4^1)", 9);
            CheckExpression("((((((((((((((((1))))))))))))))))", 1);
        }
    }
}
