using System.Collections.Generic;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Repository
{
    /// <summary>
    /// Implements the methods to convert repository module related from and into data transfer objects.
    /// </summary>
    public class FileClassesConverter : IFileClassesConverter
    {
        #region Implementation of IFileClassesConverter

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
        public FileLink ToContract(DataModel.FileLink link)
        {
            return new FileLink
                       {
                           Id = link.Id,
                           Metadata = new Dictionary<string, string>(link.Metadata)
                       };
        }

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
        public FileInfo ToContract(DataModel.FileInfo info)
        {
            return new FileInfo
                       {
                           Id = info.Id,
                           Name = info.Name
                       };
        }

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
        public DataModel.FileLink FromContract(FileLink link)
        {
            return new DataModel.FileLink(link.Id, link.Name, link.Metadata);
        }

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
        public DataModel.FileInfo FromContract(FileInfo info)
        {
            return new DataModel.FileInfo
                       {
                           Id = info.Id,
                           Name = info.Name
                       };
        }

        #endregion
    }
}
