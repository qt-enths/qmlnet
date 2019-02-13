using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Qml.Net.Internal.Qml;
using Qml.Net.Internal.Types;

namespace Qml.Net.Internal.CodeGen
{
    internal class CodeGen
    {
        public delegate void InvokeMethodDelegate(NetReference reference, List<NetVariant> parameters, NetVariant result);

        public static InvokeMethodDelegate BuildInvokeMethodDelegate(NetMethodInfo methodInfo)
        {
            var invokeType = Type.GetType(methodInfo.ParentType.FullTypeName);
            var invokeMethod = invokeType.GetMethod(methodInfo.MethodName);
            var instanceProperty = typeof(NetReference).GetProperty("Instance");

            var dynamicMethod = new DynamicMethod("method",
                typeof(void),
                new Type[]{typeof(NetReference), typeof(List<NetVariant>), typeof(NetVariant)});

            var il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, instanceProperty.GetMethod);
            il.Emit(OpCodes.Castclass, invokeType);
            il.Emit(OpCodes.Callvirt, invokeMethod);
            il.Emit(OpCodes.Ret);

            return (InvokeMethodDelegate)dynamicMethod.CreateDelegate(typeof(InvokeMethodDelegate));
        }
    }
}