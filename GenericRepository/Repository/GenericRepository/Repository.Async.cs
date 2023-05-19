using GenericReositoryDll.Enumrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GenericRepository.Interfaces.Repository;
using GenericRepository.Models;

namespace GenericRepository.Repository;

public partial class Repository<T> : IRepository<T> where T : BaseModel
{

  private readonly DbContext _context;
  private readonly DbSet<T> _model;
  public Repository(DbContext context)
  {
    this._context = context;
    _model = _context.Set<T>();
  }
  public async Task AddAsync(T model)
  => await _model.AddAsync(model);
  
  public async Task AddRangeAsync(IEnumerable<T> models)
  => await _model.AddRangeAsync(models);
  


  public async Task DeleteAsync(long id)
  {
    T model = _context.FindAsync<T>(id).Result;
    if (model != null) DeleteAsync(model);
  }

  public async  Task DeleteAsync(T model)
  {
    if (_context.Entry(model).State is EntityState.Detached)
      _context.Attach(model);

    _model.Remove(model);
  }

  public async Task DeleteRangeAsync(IEnumerable<T> models)
  => _model.RemoveRange(models);
  
 
  public async Task UpdateAsync(T model)
  {
    _context.Attach(model);
    _context.Entry(model).State = EntityState.Modified;
  }
  
  public async Task UpdateRangeAsync(IEnumerable<T> models)
  => _model.UpdateRange(models);
   
  










}

