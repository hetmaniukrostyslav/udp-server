using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer.Application.ViewModels
{
    public class MessageViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public SenderViewModel Sender { get; set; }
    }
}
