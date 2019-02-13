using System;
using System.Collections.Generic;
using Moq;
using Qml.Net.Internal.Qml;
using Qml.Net.Internal.Types;
using Xunit;

namespace Qml.Net.Tests.CodeGen
{
    public class CodeGenTests
    {
        public class TestObject
        {
            public virtual void Method1()
            {
            }
        }

        [Fact]
        public void Can_build_delegate_for_method()
        {
            var typeInfo = global::Qml.Net.Internal.Types.NetTypeManager.GetTypeInfo<TestObject>();
            typeInfo.EnsureLoaded();
            var method = typeInfo.GetMethod(0);

            var mock = new Mock<TestObject>();
            var del = global::Qml.Net.Internal.CodeGen.CodeGen.BuildInvokeMethodDelegate(method);

            del(NetReference.CreateForObject(mock.Object), new List<NetVariant>(),null);

            mock.Verify(x => x.Method1(), Times.Once);
        }
    }
}