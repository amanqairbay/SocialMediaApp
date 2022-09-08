using System;
namespace Core.Exceptions
{
    /// <summary>
    /// Bad request exception
    /// </summary>
    public sealed class BadRequestException : Exception
    {
        #region Constructor

        public BadRequestException(string message) : base(message)
        {

        }

        #endregion Constructor
    }
}

