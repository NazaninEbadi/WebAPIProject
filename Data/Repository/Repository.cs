using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utilities;
using System.Linq.Expressions;
using Entities.Common;
using Data.ContractRepo;
//using System.Linq.Expressions;

namespace Data.Repository
{
    public class Repository<TEntity> :  IRepository<TEntity> where TEntity : class, IEntity
    {


        #region [- props -]

        protected readonly ApplicationDbContext DbContext;
        public DbSet<TEntity> Entities { get; }

        //For edit 
        public virtual IQueryable<TEntity> Table => Entities;

        //Just for view
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        #endregion

        #region [- ctor -]
        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }
        #endregion

        #region [- virtual async Task  AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true) -]
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region [- virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true) -]
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region [- virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool save -]
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region [- virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true) -]
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region [- virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true) -]
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region [-  virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids) -]
        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }
        #endregion

        #region [- virtual TEntity GetById(params object[] ids) -]
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }
        #endregion

        #region [- virtual void Add(TEntity entity, bool saveNow = true) -]
        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region [- virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true) -]
        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region [- virtual void Update(TEntity entity, bool saveNow = true) -]
        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow)
                DbContext.SaveChanges();
        }
        #endregion

        #region [- virtual void Detach(TEntity entity) -]
        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DbContext.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }

        }

        #endregion

        #region [- virtual void Attach(TEntity entity) -]
        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }

        }

        #endregion

        //public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity,
        //    Expression<Func<TEntity,IEnumerable<TProperty>>> collectionProperty,
        //    CancellationToken cancellationToken)
        //    where TProperty : class
        //{
        //    Attach(entity);
        //    var collection = 
        //}


        //public virtual async Task LoadCollection<TProperty>(TEntity entity,
        //    Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)

        //public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity,
        //    Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty,
        //    CancellationToken cancellationToken)





    }
}
