using System;
using Core.Entities;

namespace Core.RequestFeatures
{
    /// <summary>
    /// Represents the pagedlist class
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Gets or sets meta data.
        /// </summary>
        public MetaData MetaData { get; set; }

        /// <summary>
        /// Gets or sets items.
        /// </summary>
        public List<T> Items { get; set; } = new();

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
            };

            Items.AddRange(items);
        }
    }
}

