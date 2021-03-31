using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Models;

namespace UDPServer.Persistence.Repositories
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public override async Task<Message> InsertOneAsync(Message entity, CancellationToken cancellationToken)
        {
            try
            {
                DbSet.Add(entity);
                Context.Entry(entity.Sender).State = EntityState.Detached;

                await Context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public override async Task<List<Message>> FindManyAsync(Expression<Func<Message, bool>> expression,
                                                                            CancellationToken cancellationToken)
        {
            try
            {
                return await DbSet.Where(expression)
                                    .Include(x=>x.Sender)
                                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
