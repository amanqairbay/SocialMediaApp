namespace Core.RequestFeatures
{
    /// <summary>
    /// Represents the meta data class.
    /// </summary>
    public class MetaData
    {
        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count
        /// </summary>
        public int TotalCount { get; set; }
    }
}

