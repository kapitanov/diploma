using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Repository
{
    /// <summary>
    /// Provides the methods to convert repository module related from and into data transfer objects.
    /// </summary>
    public interface IFileClassesConverter
    {
        /// <summary>
        /// Converts object of class <see cref="DataModel.FileLink"/> into its 
        /// data transfer representation <see cref="FileLink"/>.
        /// </summary>
        /// <param name="link">
        /// An object to convert.
        /// </param>
        /// <returns>
        /// An instance of <see cref="FileLink"/> that corresponds value of <paramref name="link"/>
        /// </returns>
        FileLink ToContract(DataModel.FileLink link);

        /// <summary>
        /// Converts object of class <see cref="DataModel.FileInfo"/> into its 
        /// data transfer representation <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="info">
        /// An object to convert.
        /// </param>
        /// <returns>
        /// An instance of <see cref="FileInfo"/> that corresponds value of <paramref name="info"/>
        /// </returns>
        FileInfo ToContract(DataModel.FileInfo info);

        /// <summary>
        /// Converts object of data transfer class <see cref="FileLink"/> 
        /// into an instance of class <see cref="DataModel.FileLink"/>.
        /// </summary>
        /// <param name="link">
        /// An object to convert.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.FileLink"/> that corresponds value of <paramref name="link"/>
        /// </returns>
        DataModel.FileLink FromContract(FileLink link);

        /// <summary>
        ///  Converts object of data transfer class <see cref="FileInfo"/> 
        /// into an instance of class <see cref="DataModel.FileInfo"/>.
        /// </summary>
        /// <param name="info">
        /// An object to convert.
        /// </param>
        /// <returns>
        /// An instance of <see cref="DataModel.FileInfo"/> that corresponds value of <paramref name="info"/>
        /// </returns>
        DataModel.FileInfo FromContract(FileInfo info);
    }
}