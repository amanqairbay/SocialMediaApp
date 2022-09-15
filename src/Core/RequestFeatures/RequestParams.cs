using System;
namespace Core.RequestFeatures
{
    /// <summary>
    /// Request parameters
    /// </summary>
    public abstract class RequestParameters
    {
        /// <summary>
        /// Maximum page size
        /// </summary>
        private const int MaxPageSize = 50;

        /// <summary>
        /// Page number
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Page size count
        /// </summary>
        private int _pageSize = 6;

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize ? MaxPageSize : value);
        }
    }
}

