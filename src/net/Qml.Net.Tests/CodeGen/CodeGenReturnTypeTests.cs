using System;
using Xunit;

namespace Qml.Net.Tests.CodeGen
{
    public class CodeGenReturnTypeTests
    {
        public class TestObject
        {
            public virtual bool ReturnTypeBool()
            {
                return false;
            }

            public virtual bool? ReturnTypeBoolNullable()
            {
                return false;
            }

            public virtual char ReturnTypeChar()
            {
                return '0';
            }

            public virtual char ReturnTypeCharNullable()
            {
                return '0';
            }

            public virtual int ReturnTypeInt()
            {
                return 0;
            }

            public virtual int? ReturnTypeIntNullable()
            {
                return 0;
            }

            public virtual long ReturnTypeLong()
            {
                return 0;
            }

            public virtual long? ReturnTypeLongNullable()
            {
                return 0;
            }

            public virtual ulong ReturnTypeULong()
            {
                return 0;
            }

            public virtual ulong? ReturnTypeULongNullable()
            {
                return 0;
            }

            public virtual float ReturnTypeFloat()
            {
                return 0;
            }

            public virtual float? ReturnTypeFloatNullable()
            {
                return 0;
            }

            public virtual double ReturnTypeDouble()
            {
                return 0;
            }

            public virtual double? ReturnTypeDoubleNullable()
            {
                return 0;
            }

            public virtual string ReturnTypeString()
            {
                return "";
            }

            public virtual DateTimeOffset ReturnTypeDateTime()
            {
                return DateTimeOffset.Now;
            }

            public virtual DateTimeOffset? ReturnTypeDateTimeNullable()
            {
                return DateTimeOffset.Now;
            }

            public virtual object ReturnTypeObject()
            {
                return null;
            }

            public virtual INetJsValue ReturnTypeNetJsValue()
            {
                return null;
            }
        }

        [Fact]
        public void Can_return_type_int()
        {
            
        }
    }
}