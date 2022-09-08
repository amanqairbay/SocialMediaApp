using System;
using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Photo
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; } = String.Empty;
        public IFormFile File { get; set; } = default!;
        public string Description { get; set; } = String.Empty;
        public DateTime? DateAdded { get; set; }
        public string PublicId { get; set; } = String.Empty;

        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}

