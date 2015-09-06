// *********************************************************************
// * Copyright © 2015 Thomas Due
// *
// * Permission is hereby granted, free of charge, to any person obtaining a copy
// * of this software and associated documentation files (the "Software"), to deal
// * in the Software without restriction, including without limitation the rights
// * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// * copies of the Software, and to permit persons to whom the Software is
// * furnished to do so, subject to the following conditions:
// * 
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// * 
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// * THE SOFTWARE.
// ********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleOrm.Interfaces;

namespace SimpleOrm
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class GenericSqlRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IDataEntity<TKey>, new()
    {
        private readonly IDataContext _dataContext;
        private readonly PropertyInfo[] _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericSqlRepository{TEntity,TKey}"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GenericSqlRepository(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            _dataContext = dataContext;

            Fields = new Dictionary<string, Type>();
            var table = typeof(TEntity).GetCustomAttribute<TableMapAttribute>();
            TableName = table != null ? table.TableName : typeof(TEntity).Name;

            _properties = typeof(TEntity).GetProperties();
            if (_properties.Any(p => p.GetCustomAttribute<DataFieldAttribute>() != null))
            {
                _properties = _properties.Where(p => p.GetCustomAttribute<DataFieldAttribute>() != null).ToArray();
            }

            var primary = _properties.FirstOrDefault(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);
            if (primary != null)
            {
                PrimaryKey = primary.Name;
            }

            foreach (var info in _properties)
            {
                var key = info.Name;
                var type = info.PropertyType;
                Fields.Add(key, type);
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public Dictionary<string, Type> Fields { get; }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; }

        /// <summary>
        /// Gets the primary key.
        /// </summary>
        /// <value>
        /// The primary key.
        /// </value>
        public string PrimaryKey { get; }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TEntity Get(TKey id)
        {
            var fields = Fields.Select(p => p.Key).Aggregate(string.Empty, (c, n) => c + ", " + n).Substring(2);
            var query = $"select {fields} from {TableName} where {PrimaryKey}=@{PrimaryKey}";
                                                                                             
            var parameters = new Dictionary<string, object> {{PrimaryKey, id}};
            var reader = _dataContext.ExecuteQuery(query, parameters);
            if (reader.Read())
            {
                var result = Activator.CreateInstance<TEntity>();
                foreach (var info in Fields)
                {
                    var key = info.Key;
                    var value = reader.GetFieldValue(key);
                    var prop = _properties.FirstOrDefault(p => p.Name == key);
                    prop?.SetValue(result, value);
                }

                return result;
            }

            return default(TEntity);
        }

        public bool Exists(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(TEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
