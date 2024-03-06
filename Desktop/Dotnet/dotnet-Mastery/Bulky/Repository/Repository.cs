﻿using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbset;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbset = _db.Set<T>();
        _db.Products.Include( u => u.Category).Include(u => u.CategoryId);
    }
    public void Add(T entity)
    {
        dbset.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
       IQueryable<T> query = dbset;
          query = query.Where(filter);
       if ( !string.IsNullOrEmpty(includeProperties)) {

             foreach (var includeProp in includeProperties
             .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                 query = query.Include(includeProp);
             }
       }
    
       return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll( string? includeProperties = null )
    {
      IQueryable<T> query = dbset;
         if ( !string.IsNullOrEmpty(includeProperties)) {

             foreach (var includeProp in includeProperties
             .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                 query = query.Include(includeProp);
             }
       }
        return query.ToList();
    }

    public void Remove(T entity)
    {
   dbset.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbset.RemoveRange(entity);
    }

 
}
