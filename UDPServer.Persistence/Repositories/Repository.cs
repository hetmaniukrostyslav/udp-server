using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Models;

namespace UDPServer.Persistence.Repositories
{
    public abstract class Repository<TEntity> where TEntity : Entity 
    {
        protected readonly ApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            Context = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> InsertOneAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                DbSet.Add(entity);
                await Context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> expression,
                                                               CancellationToken cancellationToken)
        {
            try
            {
                return await DbSet.FirstOrDefaultAsync(expression, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public virtual async Task<List<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> expression, 
                                                                CancellationToken cancellationToken)
        {
            try
            {
                return await DbSet.Where(expression).ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
