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
using System.Data.SqlClient;
using SimpleOrm.Interfaces;

namespace SimpleOrm
{
    public class SqlServerDataContext : AbstractDataContext
    {
        private SqlConnection _connection;

        public SqlServerDataContext()
        {
        }

        public SqlServerDataContext(IDataConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }

        public override void Open()
        {
            BuildConnect();
            _connection.Open();
        }

        public override void Close()
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("No active connection found.");
            }

            _connection.Close();
            _connection = null;
        }

        private void BuildConnect()
        {
            if (_connection != null)
            {
                throw new InvalidOperationException("A connection already exists. Close this first.");
            }

            // ReSharper disable once CollectionNeverQueried.Local
            var conn = new SqlConnectionStringBuilder
                       {
                           DataSource = ConnectionInfo.Server,
                           InitialCatalog = ConnectionInfo.Database,
                           UserID = ConnectionInfo.Trusted ? string.Empty : ConnectionInfo.UserId,
                           Password = ConnectionInfo.Trusted ? string.Empty : ConnectionInfo.Password,
                           IntegratedSecurity = ConnectionInfo.Trusted,
                           Pooling = true,
                           MultipleActiveResultSets = true
                       };

            _connection = new SqlConnection(conn.ConnectionString);
        }
    }
}
