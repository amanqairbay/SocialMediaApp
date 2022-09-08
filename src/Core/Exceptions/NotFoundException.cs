using System;
namespace Core.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public sealed class NotFoundException : Exception
    {
        #region Constructor

        public NotFoundException(string message) : base(message)
        {

        }

        #endregion Constructor
    }
}

