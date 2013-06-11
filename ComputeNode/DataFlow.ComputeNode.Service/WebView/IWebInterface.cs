using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AISTek.DataFlow.ComputeNode.Service.WebView
{
    [ServiceContract(ConfigurationName = "AISTek.DataFlow.ComputeNode.Service.WebView.IWebInterface")]
    public interface IWebInterface
    {
        [OperationContract]
        [WebGet(UriTemplate = @"Status")]
        Stream ViewStatus();
        
        [OperationContract]
        [WebGet(UriTemplate = @"Css")]
        Stream ViewCss();
    }
}


