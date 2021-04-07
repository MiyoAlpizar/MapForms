using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.DataAccess.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<TDTO> Get<TDTO>(int id);

        Task<IEnumerable<T>> GetList(
            Expression<Func<T, bool>> filter = null);

        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null);

        Task<TDTO> Add<TCreation,TDTO>(TCreation creation) where TCreation : class;

        Task<T> Update<TCreation, TDTO>(int id, TCreation update);

        Task<T> Patch<TCreation, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) where TDTO : class;


        Task<bool> Remove(int id);

        Task<bool> Remove(T entity);
    }
}
