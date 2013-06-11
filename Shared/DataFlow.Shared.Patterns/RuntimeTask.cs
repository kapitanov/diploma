using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using System.Diagnostics;
using System.Reflection;
using AISTek.DataFlow.Shared.DataExchange;
using System.Linq.Expressions;
using AISTek.DataFlow.PerfomanceToolkit;
using System.IO;

namespace AISTek.DataFlow.Shared.Patterns
{
    public class RuntimeTask : ITask
    {
        public IRepository Repository { get; set; }

        public Task Task { get; set; }

        internal RuntimeInfo Runtime { get; private set; }

        internal DataContractOptions DtoOptions { get; private set; }

        public bool Validate(ISystemResources resources)
        {
            return true;
        }

        public void Execute()
        {
            using (Perfomance.Trace("RuntimeTask::Execute()").BindToConsole())
            {
                try
                {
                    // Load serialization settings
                    DtoOptions = DtoHelper.Read(this.Parameter<string>(Constants.DtoOptionsKey));

                    // Load type information
                    var typeName = this.Parameter<string>(Constants.TaskTypeNameKey);
                    var type = LoadType(typeName);

                    var outputType = Task.OutputFiles.First().GetDataType();
                    Runtime = new RuntimeInfo(type).BindExecuteMethod();

                    if (!Runtime.ValidateResultType(outputType))
                    {
                        Debug.Print("{0}.Execute(..) must have return type {1}", typeName, outputType);
                        throw new InvalidOperationException("Wrong return type");
                    }

                    Runtime.CreateInstance();

                    var result = Runtime.IsScalarParameter
                        ? ExecuteScalar()
                        : ExecuteVector();

                    result.Serialize(outputType, DtoOptions).ToExistingFile(Repository, Task.OutputFiles.First());
                }
                catch (TypeLoadException)
                {
                    throw;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }
                catch (FileLoadException)
                {
                    throw;
                }
                catch (BadImageFormatException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.ToString());
                    throw;
                }
            }
        }

        public void Teardown()
        { }

        private Type LoadType(string typeName)
        {
            using (Perfomance.Trace("RuntimeTask::LoadType()").BindToConsole())
            {
                return Type.GetType(typeName, true);
            }
        }

        private object LoadScalarParameter()
        {
            using (Perfomance.Trace("RuntimeTask::ExecuteScalar(): Load parameter").BindToConsole())
            {
                var file = Task.InputFiles.First();
                var inputType = file.GetDataType();

                if (!Runtime.ValidateParameterType(inputType))
                {
                    throw new InvalidOperationException("Scalar parameter type and input data type are not consistent.");
                }

                return Repository.Deserialize(file, inputType, DtoOptions);
            }
        }

        private object LoadVectorParameter()
        {
            using (Perfomance.Trace("RuntimeTask::LoadVectorParameter()").BindToConsole())
            {
                var typedFiles = Task.InputFiles.Select(file => new { File = file, Type = file.GetDataType() }).ToList();

                // Validate input data type
                if (!typedFiles.All(f => Runtime.ValidateParameterType(f.Type)))
                {
                    throw new InvalidOperationException("Vector parameter type and input data type are not consistent.");
                }

                // Read input data
                return GatheredData.Gather(Runtime.ParameterType, typedFiles.Select(f => Repository.Deserialize(f.File, f.Type, DtoOptions)));
            }
        }

        private object ExecuteScalar()
        {
            using (Perfomance.Trace("RuntimeTask::ExecuteScalar()").BindToConsole())
            {
                if (Task.InputFiles.Count != 1)
                {
                    throw new InvalidOperationException("Task parameter is scalar, but task has more than one input file.");
                }

                var parameterValue = LoadScalarParameter();

                return Runtime.Invoke(parameterValue);
            }
        }

        private object ExecuteVector()
        {
            using (Perfomance.Trace("RuntimeTask::ExecuteVector()").BindToConsole())
            {
                var parameterValue = LoadVectorParameter();

                return Runtime.Invoke(parameterValue);
            }
        }
    }
}
