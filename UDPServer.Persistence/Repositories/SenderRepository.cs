using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Models;

namespace UDPServer.Persistence.Repositories
{
    public class SenderRepository : Repository<Sender>
    {
        public SenderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
