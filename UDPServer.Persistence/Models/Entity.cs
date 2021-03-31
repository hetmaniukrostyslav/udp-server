using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UDPServer.Persistence.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
