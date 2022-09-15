using System;
namespace Core.RequestFeatures
{
    /// <summary>
    /// Represents pagination header
    /// </summary>
    public class PaginationHeader
    {
        /// <summary>
        /// Gets or sets current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets items per page
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets total items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets total pages
        /// </summary>
        public int TotalPages { get; set; }

        #region Constructor

        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        #endregion Constructor
    }
}

