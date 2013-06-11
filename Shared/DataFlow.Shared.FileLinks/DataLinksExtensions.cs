using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.Shared.FileLinks
{
    public static class DataLinksExtensions
    {
        public static T ReadFile<T>(this FileLink link, IRepositoryService repository)
        {
            var utcPath = Encoding.UTF8.GetString(
                repository.Download(link.Id).FileStream.ReadToBuffer());

            using (var stream = new FileStream(utcPath, FileMode.Open))
            {
                
            }
        }
    }
}
