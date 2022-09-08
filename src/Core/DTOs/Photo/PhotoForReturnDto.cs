using System;
namespace Core.DTOs.Photo
{
    public class PhotoForReturnDto
    {
        public long Id { get; set; }
        public string Url { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime? DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; } = String.Empty;
    }
}

