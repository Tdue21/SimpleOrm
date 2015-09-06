using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOrm
{
    public interface IDataContext
    {
        void Open();

        void Close();
    }
    public interface IDataEntity
    {
    }


    public interface IRepository<TEntity> where TEntity : IDataEntity
    {
        TEntity Get<TKey>(TKey id);

        IEnumerable<TEntity> GetAll();

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);
    }

    public class TableMapAttribute : Attribute
    {
        public TableMapAttribute(string _tableName)
        {
            TableName = _tableName;
        }

        public string TableName { get; set; }

        public Type References { get; set; }


    }

    public class PrimaryKeyAttribute : Attribute
    {

    }
}
