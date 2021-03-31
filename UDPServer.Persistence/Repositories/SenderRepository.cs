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

        //public async Task<Sender> InsertIfNotExistOneAsync(Sender entity, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var result = await DbSet.FirstOrDefaultAsync(x => x.IpAddress == entity.IpAddress, cancellationToken);
        //        if (result == null)
        //        {
        //            result = await InsertOneAsync(entity, cancellationToken);
        //        }
        //        entity = result;
        //        return entity;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //}
    }
}
