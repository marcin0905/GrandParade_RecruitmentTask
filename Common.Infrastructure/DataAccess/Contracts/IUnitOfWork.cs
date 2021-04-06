using Common.Domain;
using System.Threading.Tasks;
using NHibernate;

namespace Common.Infrastructure.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        ISession Session { get; }
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(BaseEntity entity);
        Task Delete(BaseEntity entity);
    }
}