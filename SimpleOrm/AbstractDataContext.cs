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
using System.Data;
using SimpleOrm.Interfaces;

namespace SimpleOrm
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractDataContext : IDataContext
    {
        /// <summary>
        /// The connection
        /// </summary>
        protected IDbConnection Connection;

        /// <summary>
        /// The command
        /// </summary>
        protected IDbCommand Command;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDataContext"/> class.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        protected AbstractDataContext(IDataConnectionInfo connectionInfo)
        {
            ConnectionInfo = connectionInfo ?? new DataConnectionInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractDataContext"/> class.
        /// </summary>
        protected AbstractDataContext() : this(null)
        {
        }

        /// <summary>
        /// Gets the connection information.
        /// </summary>
        /// <value>
        /// The connection information.
        /// </value>
        public IDataConnectionInfo ConnectionInfo { get; }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual IDataReader ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            PrepareCommand(query, parameters);
            return Command.ExecuteReader();
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            PrepareCommand(query, parameters);
            return Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            PrepareCommand(query, parameters);
            return Command.ExecuteScalar();
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(string parameter, Type type, object value);

        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">No active connection found.</exception>
        protected void CheckConnection()
        {
            if (Connection == null)
            {
                throw new InvalidOperationException("No active connection found.");
            }
        }

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        private void PrepareCommand(string query, Dictionary<string, object> parameters)
        {
            Command.CommandText = query;

            foreach (var parameter in parameters)
            {
                Command.Parameters.Add(CreateParameter(parameter.Key, parameter.Value.GetType(), parameter.Value));
            }
        }
    }
}
