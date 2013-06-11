using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.WebView
{
    public sealed class WebInterfaceBuilder : IWebInterfaceBuilder
    {
        public WebInterfaceBuilder()
        {
            Encoding = new UTF8Encoding();
        }

        #region Implementation of IWebInterfaceBuilder

        /// <summary>
        /// Gets and sets root path for web view pages.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Gets and sets encoding for views.
        /// </summary>
        /// <remarks>
        /// By default thi property is set to <see cref="UTF8Encoding"/>.
        /// </remarks>
        public Encoding Encoding { get; set; }


        /// <summary>
        /// Builds view that accords to "no task" state.
        /// </summary>
        /// <param name="status">
        /// A <see cref="string"/> that contains status message.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the builded view.
        /// </returns>
        public Stream BuildNoTaskView(string status)
        {
            return BuildView(NoTaskView, new Dictionary<string, string> { { NoTaskViewStatusField, status } });
        }

        /// <summary>
        /// Builds view that accords to "processing task" state.
        /// </summary>
        /// <param name="status">
        /// A <see cref="string"/> that contains status message.
        /// </param>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that is being currently processing.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the builded view.
        /// </returns>
        public Stream BuildTaskView(string status, Task task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds view that contains error information.
        /// </summary>
        /// <param name="message">
        /// A <see cref="string"/> that contains error message.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the builded view.
        /// </returns>
        public Stream BuildErrorView(string message)
        {
            return BuildView(ErrorView, new Dictionary<string, string> { { ErrorViewMessageField, message } });
        }

        /// <summary>
        /// Builds view that contains css classes.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains the css classes.
        /// </returns>
        public Stream GetCss()
        {
            return new System.IO.FileInfo(Path.Combine(RootPath, StylesheetFile)).Open(FileMode.Open);
        }

        #endregion

        private Stream BuildView(string viewName, IDictionary<string, string> values)
        {
            var doc = XDocument.Load(new System.IO.FileInfo(
                Path.Combine(RootPath, viewName + ViewFileExtension))
                .OpenText());
            Process(doc.Descendants().First(), values);
            return new MemoryStream(Encoding.GetBytes(doc.ToString()));
        }

        private static void Process(XElement element, IDictionary<string, string> values)
        {
            if (element.Name.NamespaceName == WebInterfaceNamespace)
            {
                var key = element.Name.LocalName;
                if (values.ContainsKey(key))
                    element.Value = values[key];
                else
                    element.Value = string.Format(UnkownKeyFormat, key);

                element.Name = HtmlSpan;

                return;
            }
            foreach (var descendant in element.Descendants())
            {
                Process(descendant, values);
            }
        }

        private const string HtmlSpan = "{http://www.w3.org/1999/xhtml}span";
        private const string WebInterfaceNamespace = "http://aistek.dataflow/computeNode/webInterface";
        private const string UnkownKeyFormat = "{{Value {0} doesn't correspond to any known parameters}}";
        private const string ViewFileExtension = ".xml";
        private const string NoTaskView = "NoTask";
        private const string NoTaskViewStatusField = "status";
        private const string ErrorView = "Error";
        private const string ErrorViewMessageField = "error";
        private const string StylesheetFile = "Stylesheet.css";
    }
}
