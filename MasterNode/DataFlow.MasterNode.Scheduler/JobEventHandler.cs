namespace AISTek.DataFlow.MasterNode.Scheduler
{
    ///<summary>
    /// Represents the method that handles event from <see cref="IJobScheduler"/>.
    ///</summary>
    ///<param name="sender">
    /// The source of the event.
    /// </param>
    ///<param name="e">
    /// A <see cref="JobEventArgs"/> that contains the event data.
    /// </param>
    public delegate void JobEventHandler(object sender, JobEventArgs e);
}
