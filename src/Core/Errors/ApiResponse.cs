using System;
namespace Core.Errors
{
    /// <summary>
    /// Api error response
    /// </summary>
    public class ApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets an error message
        /// </summary>
        public string? Message { get; set; }

        #endregion Properties

        #region Constructor

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        #endregion Constructor

        #region Method

        /// <summary>
        /// Gets default messages for status codes
        /// </summary>
        /// <param name="statusCode">Status code</param>
        /// <returns>
        /// The result contains a message corresponding to the status of the error code
        /// </returns>
        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Invalid syntax for this request was provided.",
                401 => "You are unauthorized to access the requested resource.",
                404 => "The resource you requested could not be found.",
                500 => "Unexpected internal server error.",
                _ => null
            };
        }

        #endregion Method
    }
}

