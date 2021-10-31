using System;

namespace TableCalculator.Data
{
    public abstract class DeleteLineException : Exception { }
    public class DeleteLineNotEmptyException : DeleteLineException { }
    public class DeleteLineDependentException : DeleteLineException { }
}
