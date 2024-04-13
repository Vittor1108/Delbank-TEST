using Delbank.Domain.Enums;

namespace Delbank.Application.DTOs.DvdDTOs
{
    public class DvdRequestDTO
    {
        public string? Title { get; set; }
        public EGenre Genre { get; set; }
        public DateTime Published { get; set; }
        public int Copies { get; set; }
        public bool Avaliable { get; set; }        
        public Guid FkDirector { get; set; }
    }
}
