using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Qml.Net.Internal.Qml;
using Qml.Net.Internal.Types;
using Xunit;

namespace Qml.Net.Tests.CodeGen
{
    public class CodeGenTests
    {
        private Mock<TestObject> _mock;
        private NetTypeInfo _typeInfo;

        public CodeGenTests()
        {
            _mock = new Mock<TestObject>();
            _typeInfo = NetTypeManager.GetTypeInfo<TestObject>();
            _typeInfo.EnsureLoaded();
        }

        public class TestObject
        {
            public virtual void Method1()
            {
            }

            public virtual int Method2()
            {
                return 0;
            }
        }

        [Fact]
        public void Can_build_delegate_for_method()
        {
            var method = _typeInfo.GetMethod(0);

            var del = global::Qml.Net.Internal.CodeGen.CodeGen.BuildInvokeMethodDelegate(method);

            del(NetReference.CreateForObject(_mock.Object), new List<NetVariant>(),null);

            _mock.Verify(x => x.Method1(), Times.Once);
        }

        [Fact]
        public void Can_build_delegate_for_method_with_return_value()
        {
            var method = _typeInfo.GetMethod(1);
            var del = global::Qml.Net.Internal.CodeGen.CodeGen.BuildInvokeMethodDelegate(method);

            _mock.Setup(x => x.Method2()).Returns(50);
            
            var result = new NetVariant();
            del(NetReference.CreateForObject(_mock.Object), new List<NetVariant>(), result);

            _mock.Verify(x => x.Method2(), Times.Once);
            
            result.VariantType.Should().Be(NetVariantType.Int);
            result.Int.Should().Be(50);
            
        }
    }
}