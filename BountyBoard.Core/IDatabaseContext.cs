using BountyBoard.Core.Data;
using System.Linq;

namespace BountyBoard.Core
{
    public interface IDatabaseContext
    {

        int SaveChanges();
        void Add<T>(T item) where T : DatabaseObject;
        void Delete<T>(int id);
        void Update<T>(T item) where T : DatabaseObject;
        IQueryable<T> List<T>() where T : DatabaseObject;
    }
}