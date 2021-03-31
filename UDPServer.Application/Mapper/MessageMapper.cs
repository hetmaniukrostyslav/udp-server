using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPServer.Application.ViewModels;
using UDPServer.Persistence.Models;

namespace UDPServer.Application.Mapper
{
    public static class MessageMapper
    {
        public static MessageViewModel ToViewModel(this Message message)
        {
            return new MessageViewModel
            {
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                Sender = message.Sender?.ToViewModel()
            };
        }
    }
}
