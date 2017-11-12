using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebGame.Data.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Variables
        protected readonly GameDbContext Db;
        private bool _disposed;
        #endregion

        #region Ctor
        public UnitOfWork()
        {
            Db = new GameDbContext();
            //Initialize repositoies
            GameRepository = new Repository<Game>(Db);
            GenreRepository = new Repository<Genre>(Db);
            PlatformTypeRepository = new Repository<PlatformType>(Db);
            CommentRepository = new Repository<Comment>(Db);
        }
        #endregion

        public IRepository<Game> GameRepository { get; }
        public IRepository<Genre> GenreRepository { get; }
        public IRepository<PlatformType> PlatformTypeRepository { get; }
        public IRepository<Comment> CommentRepository { get; }

        public void Commit()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationError in
                    ex.EntityValidationErrors.SelectMany(entityValidationError => entityValidationError.ValidationErrors)
                    )
                {
                    Debug.Print(validationError.ErrorMessage);
                    
                }
                throw;
            }
            catch (DbUpdateException ue)
            {

                Debug.Print(ue.InnerException?.Message ?? ue.Message);

                throw;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                throw;
            }
        }

        #region Disposable
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}