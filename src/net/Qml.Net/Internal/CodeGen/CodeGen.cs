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

            if (invokeMethod.ReturnType != null && invokeMethod.ReturnType != typeof(void))
            {
                var netValueProp = typeof(NetVariant).GetProperty("Int");
                // This method has a return type.
                var il = dynamicMethod.GetILGenerator();
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, instanceProperty.GetMethod);
                il.Emit(OpCodes.Castclass, invokeType);
                il.Emit(OpCodes.Callvirt, invokeMethod);
                il.Emit(OpCodes.Callvirt, netValueProp.SetMethod);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                var il = dynamicMethod.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, instanceProperty.GetMethod);
                il.Emit(OpCodes.Castclass, invokeType);
                il.Emit(OpCodes.Callvirt, invokeMethod);
                il.Emit(OpCodes.Ret);
            }
           
            return (InvokeMethodDelegate)dynamicMethod.CreateDelegate(typeof(InvokeMethodDelegate));
        }
        
        private NetVariantType GetPrefVariantType(Type typeInfo)
        {
            if (typeInfo == typeof(bool))
                return NetVariantType.Bool;
            if (typeInfo == typeof(char))
                return NetVariantType.Char;
            if (typeInfo == typeof(int))
                return NetVariantType.Int;
            if (typeInfo == typeof(uint))
                return NetVariantType.UInt;
            if (typeInfo == typeof(long))
                return NetVariantType.Long;
            if (typeInfo == typeof(ulong))
                return NetVariantType.ULong;
            if (typeInfo == typeof(float))
                return NetVariantType.Float;
            if (typeInfo == typeof(double))
                return NetVariantType.Double;
            if (typeInfo == typeof(string))
                return NetVariantType.String;
            if (typeInfo == typeof(DateTimeOffset))
                return NetVariantType.DateTime;

            if (typeInfo.IsGenericType &&
                typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // ReSharper disable TailRecursiveCall
                return GetPrefVariantType(typeInfo.GetGenericArguments()[0]);
                // ReSharper restore TailRecursiveCall
            }

            return NetVariantType.Object;
        }
    }
}