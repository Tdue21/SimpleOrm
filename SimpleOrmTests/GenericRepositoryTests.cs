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

        [SetUp]
        protected void SetUp()
        {
            _context = new Mock<IDataContext>();

        }
        [Test]
        public void Fields_Contains_Correct_Values_Test()
        {
            var repo = new GenericRepository<DataObject, int>(_context.Object);

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
            var repo = new GenericRepository<DataObjectWithAttribute, int>(_context.Object);

            repo.Fields
                .Should().HaveCount(2)
                .And
                .ContainKeys("Id", "Number")
                .And
                .ContainValues(typeof (int), typeof (string));
        }
    }
}
