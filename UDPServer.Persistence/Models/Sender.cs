using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer.Persistence.Models
{
    public class Sender : Entity
    {
        public string IpAddress { get; set; }
    }
}
