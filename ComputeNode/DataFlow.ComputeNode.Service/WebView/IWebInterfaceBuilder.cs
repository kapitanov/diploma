using System.IO;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.WebView
{
    /// <summary>
    /// Provides methods for building web interface views.
    /// </summary>
    public interface IWebInterfaceBuilder
    {
        /// <summary>
        /// Gets and sets root path for web view pages.
        /// </summary>
        string RootPath { get; set; }

        /// <summary>
        /// Gets and sets encoding for views.
        /// </summary>
        /// <remarks>
        /// By default thi property is set to <see cref="UTF8Encoding"/>.
        /// </remarks>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Builds view that accords to "no task" state.
        /// </summary>
        /// <param name="status">
        /// A <see cref="string"/> that contains status message.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the builded view.
        /// </returns>
        Stream BuildNoTaskView(string status);

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
        Stream BuildTaskView(string status, Task task);

        /// <summary>
        /// Builds view that contains error information.
        /// </summary>
        /// <param name="message">
        /// A <see cref="string"/> that contains error message.
        /// </param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the builded view.
        /// </returns>
        Stream BuildErrorView(string message);

        /// <summary>
        /// Builds view that contains css classes.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains the css classes.
        /// </returns>
        Stream GetCss();
    }
}
