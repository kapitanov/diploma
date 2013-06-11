using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{
    /// <summary>
    /// Represents a request to create a new empty file.
    /// </summary>
    [Serializable]
    public class CreateFile : File
    {
        /// <summary>
        /// Initializes new instance of <see cref="CreateFile"/>.
        /// </summary>
        /// <param name="name">
        /// New file's name.
        /// </param>
        public CreateFile(string name) 
            : base(name)
        { }

        protected override FileLink SaveFilePerform(IRepository repository)
        {
            var id = repository.CreateNew(Name);
            var link = new FileLink { Id = id, Metadata = Metadata };
            return link;
        }
    }
}
