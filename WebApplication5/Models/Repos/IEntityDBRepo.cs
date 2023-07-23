using System.Collections.Generic;

namespace WebApplication5.Models.Repos
{
    public interface IEntityDBRepo<TEntity>
    {
        List<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        public List<TEntity> Search(string term);
    }
}
