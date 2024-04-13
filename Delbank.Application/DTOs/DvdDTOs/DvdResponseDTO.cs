using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Enums;

namespace Delbank.Application.DTOs.DvdDTOs
{
    public class DvdResponseDTO
    {
        public string? Id { get; set; }                
        public string? Title { get; set; }
        public EGenre Genre { get; set; }
        public DateTime Published { get; set; }
        public int Copies { get; set; }
        public bool Avaliable { get; set; }
    }
}
