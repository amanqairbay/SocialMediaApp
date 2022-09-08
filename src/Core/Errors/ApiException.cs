using System;
namespace Core.Errors
{
    /// <summary>
    /// Api error exception
    /// </summary>
    public class ApiException : ApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets error details
        /// </summary>
        public string? Details { get; set; }

        #endregion Properties

        #region Constructor

        public ApiException(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        #endregion Constructor
    }
}

