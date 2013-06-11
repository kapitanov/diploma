namespace AISTek.DataFlow.Tools.LogViewer
{
    internal interface IViewHost
    {
        void ShowProgressBar();

        void HideProgressBar();

        void SetStatus(string statusText);
    }
}
