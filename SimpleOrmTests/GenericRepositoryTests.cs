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
using System.Linq;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SimpleOrm;
using SimpleOrm.Interfaces;
using SimpleOrmTests.TestData;

namespace SimpleOrmTests
{
    [TestFixture]
    public class GenericRepositoryTests
    {
        private Mock<IDataContext> _context;
        private Mock<IDataReader> _reader;

        [SetUp]
        protected void SetUp()
        {
            _reader = new Mock<IDataReader>();
            _context = new Mock<IDataContext>();

        }
        [Test]
        public void Fields_Contains_Correct_Values_Test()
        {
            var repo = new GenericSqlRepository<DataObject, int>(_context.Object);

            repo.Fields
                .Should().HaveCount(3)
                .And
                .ContainKeys("Id", "Number", "Created")
                .And
                .ContainValues(typeof (int), typeof (string), typeof (DateTime));
        }

        [Test]
        public void Fields_Contains_Only_DataField_Decorated_Correct_Values_Test()
        {
            var repo = new GenericSqlRepository<DataObjectWithAttribute, int>(_context.Object);

            repo.Fields
                .Should().HaveCount(2)
                .And
                .ContainKeys("Id", "Number")
                .And
                .ContainValues(typeof (int), typeof (string));
        }

        [Test]
        public void Get_Builds_Correct_Sql_Test()
        {
            const string expectedQuery = "select Id, Number, Created from DataObject where Id=@Id";
            var expectedParameters = new Dictionary<string, object> { { "Id", 1 } };

            var actualQuery = string.Empty;
            var actualParameters = new Dictionary<string, object>();

            _reader.Setup(r => r.Read()).Returns(false);
            _context.Setup(c => c.ExecuteQuery(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                    .Callback<string, Dictionary<string, object>>((q, p) =>
                                                                  {
                                                                      actualQuery = q;
                                                                      actualParameters = new Dictionary<string, object>(p);
                                                                  })
                    .Returns(_reader.Object);

            var repo = new GenericSqlRepository<DataObject, int>(_context.Object);

            var result = repo.Get(1);

            result.Should().BeNull();
            actualQuery.ShouldBeEquivalentTo(expectedQuery);
            actualParameters.ShouldBeEquivalentTo(expectedParameters);
        }
    }
}
