using System.ComponentModel.DataAnnotations;

namespace CodeRoute.Models
{
    public class VertexStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
