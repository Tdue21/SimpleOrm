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
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IDataEntity<TKey>, new() 
    {
        private readonly IDataContext _dataContext;

        public GenericRepository(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            _dataContext = dataContext;

            Fields = new Dictionary<string, Type>();

            var props = typeof (TEntity).GetProperties();
            if (props.Any(p => p.GetCustomAttribute<DataField>() != null))
            {
                props = props.Where(p => p.GetCustomAttribute<DataField>() != null).ToArray();
            }

            foreach (var info in props)
            {
                var key = info.Name;
                var type = info.PropertyType;
                Fields.Add(key, type);
            }
        }

        public Dictionary<string, Type> Fields { get; }

        public TEntity Get(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(TKey id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(TEntity id)
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

    internal static class PropertyInfoExtensions
    {
        public static T GetCustomAttribute<T>(this PropertyInfo propInfo) where T : Attribute
        {
            return propInfo != null
                       ? propInfo.GetCustomAttributes(true).OfType<T>().FirstOrDefault()
                       : null;
        }
    }
}
