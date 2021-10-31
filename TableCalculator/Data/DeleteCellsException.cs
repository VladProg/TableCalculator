using System;

namespace TableCalculator.Data
{
    /// <summary>
    /// неможливо видалити рядок/стовпчик
    /// </summary>
    public abstract class DeleteLineException : Exception { }

    /// <summary>
    /// неможливо видалити рядок/стовпчик, бо в ньому є непорожні комірки
    /// </summary>
    public class DeleteLineNotEmptyException : DeleteLineException { }

    /// <summary>
    /// неможливо видалити рядок/стовпчик, бо на його комірки посилаються інші комірки
    /// </summary>
    public class DeleteLineDependentException : DeleteLineException { }
}
