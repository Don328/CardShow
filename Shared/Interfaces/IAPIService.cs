using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Interfaces
{
    public interface IAPIService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(int id);
        Task<HttpResponseMessage> Add(T newEntry);
        Task<HttpResponseMessage> Delete(int id);
    }
}
