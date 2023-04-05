using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance;
public interface IAsyncRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(string id);
    Task DeleteAsync(string id);
    Task InsertAsync(T item);
    Task UpdateAsync(T item);
}
