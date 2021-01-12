using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositorio<T> where T :BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> updateAsync(T item);
        Task deleteAsync(Guid id);
        Task<IEnumerable<T>> selectAsync();

    }
}
