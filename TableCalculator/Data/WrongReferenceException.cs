using System;

namespace TableCalculator.Data
{
    class WrongReferenceException:Exception
    {
        public readonly string CellId;
        public WrongReferenceException(string cellId)
            => CellId = cellId;
    }
}
