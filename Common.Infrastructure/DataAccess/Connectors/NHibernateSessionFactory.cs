using System.Linq;
using System.Threading.Tasks;
using Common.Domain;
using Common.Infrastructure.DataAccess.Contracts;
using NHibernate;

namespace Common.Infrastructure.DataAccess.Connectors
{
    public class NHibernateSessionFactory : IUnitOfWork
    {
        public ISession Session { get; }
        private ITransaction _transaction;

        public NHibernateSessionFactory(ISession session)
        {
            Session = session;
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction == null) return;

            _transaction.Dispose();
            _transaction = null;
        }

        public async Task Save(BaseEntity entity)
        {
            await Session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(BaseEntity entity)
        {
            await Session.DeleteAsync(entity);
        }
    }
}