// *********************************************************************
// * Copyright � 2015 Thomas Due
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
using System.Data;

namespace SimpleOrm
{
    internal static class DataReaderExtensions
    {
        public static TValue GetFieldValue<TValue>(this IDataReader reader, string fieldName, TValue defaultValue)
        {
            if (reader == null || string.IsNullOrEmpty(fieldName))
            {
                return defaultValue;
            }

            var index = reader.GetOrdinal(fieldName);
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Field not found.");
            }

            var value = reader.GetValue(index);
            return (TValue) Convert.ChangeType(value, typeof (TValue));
        }

        public static TValue GetFieldValue<TValue>(this IDataReader reader, string fieldName)
        {
            if (reader == null || string.IsNullOrEmpty(fieldName))
            {
                return default(TValue);
            }

            var index = reader.GetOrdinal(fieldName);
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Field not found.");
            }

            var value = reader.GetValue(index);
            return (TValue) Convert.ChangeType(value, typeof (TValue));
        }

        public static object GetFieldValue(this IDataReader reader, string fieldName)
        {
            if (reader == null || string.IsNullOrEmpty(fieldName))
            {
                return null;
            }

            var index = reader.GetOrdinal(fieldName);
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Field not found.");
            }

            var value = reader.GetValue(index);
            return value;
        }
    }
}
