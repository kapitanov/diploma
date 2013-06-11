using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{
    /// <summary>
    /// Base class for files in repository.
    /// </summary>
    [Serializable]
    public abstract class File : IEnumerable<KeyValuePair<string, string>>
    {
        protected File(string name)
        {
            Name = name;
            Metadata = new Dictionary<string, string>();
        }

        protected File()
            : this(string.Empty)
        { }

        /// <summary>
        /// Gets and sets file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="FileLink"/> that represent link to current file.
        /// This field can be accessed only after <see cref="SaveFile"/> method is called.
        /// Otherwise throws an <see cref="InvalidOperationException"/> exception.
        /// </summary>
        [XmlIgnore]
        public FileLink Link
        {
            get
            {
                lock (this)
                {
                    if (link == null)
                        throw new InvalidOperationException(
                            "File.Link is accessible only after SaveFile(IRepository) method s called.");
                }
                return link;
            }
            private set { lock (this) link = value; }
        }

        /// <summary>
        /// Saves current file into repository.
        /// </summary>
        /// <param name="repository">
        /// An instance of <see cref="IRepositoryService"/> service client.
        /// </param>
        /// <returns>
        ///  An instance of <see cref="FileLink"/> that represent link to current file.
        /// </returns>
        public FileLink SaveFile(IRepository repository)
        {
            if(repository==null)
                throw new ArgumentNullException("repository");

            var link = SaveFilePerform(repository);
            if (link == null)
                throw new InvalidOperationException("File.SaveFilePerform() returned <null>");

            Link = link;
            return link;
        }

        protected abstract FileLink SaveFilePerform(IRepository repository);

        public Dictionary<string, string> Metadata { get; set; }

        public void Add(string metadataElementName, string metadataElementValue)
        {
            if (string.IsNullOrEmpty(metadataElementName))
                throw new ArgumentNullException("metadataElementName");
            if (string.IsNullOrEmpty(metadataElementValue))
                throw new ArgumentNullException("metadataElementValue");

            Metadata.Add(metadataElementName, metadataElementValue);
        }

        #region IEnumerable explicit implementations

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return Metadata.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Metadata.GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        [NonSerialized]
        private FileLink link;
    }
}
