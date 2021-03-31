using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UDPServer.Application.ViewModels;
using UDPServer.Persistence.Models;

namespace UDPServer.Application.Mapper
{
    public static class SenderMapper
    {
        public static SenderViewModel ToViewModel(this Sender sender)
        {
            return new SenderViewModel
            {
                IpAddress = sender.IpAddress
            };
        }
    }
}
