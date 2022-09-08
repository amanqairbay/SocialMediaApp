using System;
namespace Core.DTOs
{
    public class CityDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty!;
        public long regionId { get; set; }
    }
}

