using System;

namespace UDPServer.Persistence.Models
{
    public class Message : Entity
    {
        public string Text { get; set; }
        public string SenderId { get; set; }
        public Sender Sender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
