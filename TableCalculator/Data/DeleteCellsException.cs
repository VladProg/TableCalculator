using System;

namespace TableCalculator.Data
{
    abstract class DeleteLineException : Exception { }
    class DeleteLineNotEmptyException : DeleteLineException { }
    class DeleteLineDependentException : DeleteLineException { }
}
