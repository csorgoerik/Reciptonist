using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance;
public interface IRepository<T>
{
    List<T> GetAll();
    T Get(string id);
    void Delete(string id);
    void Insert(T item);
    void Update(T item);

}
