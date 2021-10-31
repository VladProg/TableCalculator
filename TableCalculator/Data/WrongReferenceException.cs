using System;

namespace TableCalculator.Data
{
    public class WrongReferenceException:Exception
    {
        public readonly string CellId;
        public WrongReferenceException(string cellId)
            => CellId = cellId;
    }
}
