namespace AISTek.DataFlow.MasterNode.Scheduler
{
    ///<summary>
    /// Represents the method that handles event from <see cref="ITaskQueue"/>.
    ///</summary>
    ///<param name="sender">
    /// The source of the event.
    /// </param>
    ///<param name="e">
    /// A <see cref="TaskEventArgs"/> that contains the event data.
    /// </param>
    public delegate void TaskEventHandler(object sender, TaskEventArgs e);
}
