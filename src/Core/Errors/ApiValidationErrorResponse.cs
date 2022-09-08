using System;
namespace Core.Errors
{
    /// <summary>
    /// Api validation error response
    /// </summary>
    public class ApiValidationErrorResponse : ApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets validation error responses
        /// </summary>
        public IEnumerable<string> Errors { get; set; } = default!;

        #endregion Properties

        #region Constructor

        public ApiValidationErrorResponse() : base(400)
        {

        }

        #endregion Constructor
    }
}

