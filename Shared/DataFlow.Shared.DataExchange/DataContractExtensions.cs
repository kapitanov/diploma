using System;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;


namespace AISTek.DataFlow.Shared.DataExchange
{
    public static class DataContractExtensions
    {
        #region Generic methods

        public static SerializedDataContract Serialize<T>(this T value, DataContractOptions options = null)
        {
            using (Perfomance.BeginTrace("DataContractExtensions::Serialize<{0}>()", typeof(T).Name)
                .BindToTrace()
                .Start())
            {
                options = options ?? DataContractOptions.Defaut;
                IDataContractSerializer<T> serializer;
                using (Perfomance.BeginTrace(
                        "DataContractExtensions::Serialize<{0}>(): create IDataContractSerializer<{0}>", typeof(T).Name)
                    .BindToTrace()
                    .Start())
                {
                    serializer = new DataContractSerializerFactory().CreateSerializer<T>(options);
                }
                using (Perfomance.BeginTrace(
                        "DataContractExtensions::Serialize<{0}>(): serialize", typeof(T).Name)
                    .BindToTrace()
                    .Start())
                {
                    return new SerializedDataContract(serializer.Serialize(value));
                }
            }
        }

        public static T Deserialize<T>(this IRepository repository, FileLink link, DataContractOptions options = null)
        {
            using (Perfomance.BeginTrace("DataContractExtensions::Deserialize<{0}>()", typeof(T).Name)
                .BindToTrace()
                .Start())
            {
                options = options ?? DataContractOptions.Defaut;
                using (var stream = repository.Download(link.Id))
                {
                    IDataContractSerializer<T> serializer;
                    using (Perfomance.BeginTrace(
                            "DataContractExtensions::Deserialize<{0}>(): create IDataContractSerializer<{0}>", typeof(T).Name)
                        .BindToTrace()
                        .Start())
                    {
                        serializer = new DataContractSerializerFactory().CreateSerializer<T>(options);
                    }
                    using (Perfomance.BeginTrace(
                            "DataContractExtensions::Deserialize<{0}>(): deserialize", typeof(T).Name)
                        .BindToTrace()
                        .Start())
                    {
                        return serializer.Deserialize(stream);
                    }
                }
            }
        } 

        #endregion

        #region Non-generic methods

        public static SerializedDataContract Serialize(this object value, Type type, DataContractOptions options = null)
        {
            using (Perfomance.Trace("DataContractExtensions::Serialize(type:{0})", type.Name)
                .BindToConsole())
            {
                options = options ?? DataContractOptions.Defaut;
                var serializer = new DataContractSerializerFactory().CreateSerializer(type, options);
                
                using (Perfomance.Trace("DataContractExtensions::Serialize(type:{0}): serialize", type.Name)
                    .BindToConsole())
                {
                    return new SerializedDataContract(serializer.Serialize(value));
                }
            }
        }

        public static object Deserialize(this IRepository repository, FileLink link, Type type, DataContractOptions options = null)
        {
            using (Perfomance.Trace("DataContractExtensions::Deserialize(type:{0})", type.Name)
                .BindToConsole())
            {
                options = options ?? DataContractOptions.Defaut;
                using (var stream = repository.Download(link.Id))
                {
                    var serializer = new DataContractSerializerFactory().CreateSerializer(type, options);
                    
                    using (Perfomance.Trace(
                            "DataContractExtensions::Deserialize(type:{0}): deserialize", type.Name)
                        .BindToConsole())
                    {
                        return serializer.Deserialize(stream);
                    }
                }
            }
        }

        #endregion

        #region Common methods

        public static void ToExistingFile(this SerializedDataContract dataContract, IRepository repository, FileLink link)
        {
            using (Perfomance.Trace("DataContractExtensions::To({0})", link.Id)
                .BindToConsole())
            {
                using (dataContract.Stream)
                {
                    using (Perfomance.Trace("DataContractExtensions::To({0}): write", link.Id)
                        .BindToConsole())
                    {
                        repository.Upload(link.Id, dataContract.Stream);
                    }
                }
            }
        }

        public static Guid ToNewFile(this SerializedDataContract dataContract, IRepository repository, string fileName)
        {
            using (Perfomance.Trace("DataContractExtensions::ToNewFile(\"{0}\")", fileName)
                .BindToConsole())
            {
                using (dataContract.Stream)
                {
                    return repository.UploadNew(fileName, dataContract.Stream);
                }
            }
        } 

        #endregion
    }
}
