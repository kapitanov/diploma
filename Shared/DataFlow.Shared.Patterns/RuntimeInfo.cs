using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal class RuntimeInfo
    {
        public RuntimeInfo(Type runtimeType)
        {
            this.runtimeType = runtimeType;
        }

        public object Instance { get; private set; }

        public MethodInfo ExecuteMethod { get; private set; }

        public Type ParameterType { get; private set; }

        public bool IsScalarParameter { get; private set; }

        public Type ResultType { get; private set; }

        public RuntimeInfo BindExecuteMethod()
        {
            using (Perfomance.Trace("RuntimeInfo::BindExecuteMethod()").BindToConsole())
            {
                // we expect a method TY Execute(TX x);
                ExecuteMethod = runtimeType.GetMethod("Execute");

                var parameters = ExecuteMethod.GetParameters();
                if (parameters.Length != 1)
                {
                    Debug.Print("{0}.Execute(..) must have one parameter", runtimeType);
                    throw new InvalidOperationException("Wrong parameters count");
                }

                var parameter = parameters[0];

                if (parameter.ParameterType.IsGenericType &&
                    parameter.ParameterType.GetGenericTypeDefinition() == typeof(IGather<>))
                {
                    // Vector parameter
                    IsScalarParameter = false;

                    var genericArguments = parameter.ParameterType.GetGenericArguments();

                    // parameter has type IGather<T>, so there's only one generic parameter
                    // and it's T
                    ParameterType = genericArguments[0];
                }
                else
                {
                    // Scalar parameter
                    IsScalarParameter = true;
                    ParameterType = parameter.ParameterType;
                }

                ResultType = ExecuteMethod.ReturnType;
                return this;
            }
        }

        public RuntimeInfo CreateInstance()
        {
            using (Perfomance.Trace("RuntimeInfo::CreateInstance()").BindToConsole())
            {
                Instance = Activator.CreateInstance(runtimeType);
                return this;
            }
        }

        public bool ValidateResultType(Type outputType)
        {
            return outputType.IsAssignableFrom(ResultType);
        }

        public bool ValidateParameterType(Type inputType)
        {
            return ParameterType.IsAssignableFrom(inputType);
        }

        public object Invoke(object arg)
        {
            using (Perfomance.Trace("RuntimeInfo::Invoke()").BindToConsole())
            {
                return ExecuteMethod.Invoke(Instance, new object[] { arg });
            }
        }

        private readonly Type runtimeType;
    }
}
