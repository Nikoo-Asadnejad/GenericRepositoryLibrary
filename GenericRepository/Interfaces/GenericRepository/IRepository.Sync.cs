using GenericReositoryDll.Enumrations;
using System.Linq.Expressions;

namespace GenericRepository.Interfaces.Repository;

public partial interface IRepository<T>
{
   
    void Add(T model);
    void AddRange(IEnumerable<T> models);
    void Update(T model);
    void UpdateRange(IEnumerable<T> models);
    void Delete(long id);
    void Delete(T model);
    void DeleteRange(IEnumerable<T> models);
    

}