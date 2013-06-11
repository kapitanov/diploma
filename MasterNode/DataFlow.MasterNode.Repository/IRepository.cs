using System;
using System.Collections.Generic;
using System.IO;
using Dto = AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public interface IRepository
    {
        FileInfoContainer CreateFile(string name);

        FileInfoContainer QueryFile(Guid id);

        void DeleteFile(Guid id);

        IEnumerable<FileInfoContainer> BrowseFiles(int startFrom, int itemsCount);
    }
}
