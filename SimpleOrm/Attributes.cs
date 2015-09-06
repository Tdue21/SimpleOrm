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

namespace SimpleOrm
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryKeyAttribute : Attribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class ForeignKeyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the reference table.
        /// </summary>
        /// <value>
        /// The reference table.
        /// </value>
        public string ReferenceTable { get; set; }

        /// <summary>
        /// Gets or sets the reference field.
        /// </summary>
        /// <value>
        /// The reference field.
        /// </value>
        public string ReferenceField { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TableMapAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableMapAttribute"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public TableMapAttribute(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        /// <value>
        /// The references.
        /// </value>
        public Type References { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DataFieldAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; set; }
    }
}
