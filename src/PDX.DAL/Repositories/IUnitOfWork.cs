using System;
using System.Threading.Tasks;
using PDX.Domain;

namespace PDX.DAL.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
       IGenericRepository<T> Repository<T>() where T : BaseEntity;

       Task<bool> SaveAsync();
    }
}
