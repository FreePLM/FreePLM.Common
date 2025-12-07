namespace FreePLM.Common.Helpers.Threading
{
    // The TaskCompletedEvents class is an abstract base class designed to handle task completion events.
    // It defines a delegate, an event, and event arguments for reporting the completion of a task, including any result or error.
    public abstract class TaskCompletedEvents
    {
        #region "Define Threaded Task"

        // Define a delegate for the event that handles task completion.
        // The delegate specifies that the event handler takes an object (the sender) and a TaskCompletedEventArgs instance (the event data).
        public delegate void TaskCompletedEventHandler(object sender, TaskCompletedEventArgs e);

        // Define the event based on the delegate.
        // This event is triggered when a task has completed, passing the event data to any subscribed handlers.
        public event TaskCompletedEventHandler? TaskCompleted;

        // Define the event arguments class that contains data related to the task completion.
        // This includes the result of the task and any potential error encountered.
        public class TaskCompletedEventArgs : EventArgs
        {
            // The result of the completed task, or null if the task has no result.
            public object? Result { get; set; } = null;

            // Any error encountered during the task execution. This can be null if no error occurred.
            public Exception? Error { get; set; }
        }

        // A protected virtual method to raise the TaskCompleted event.
        // This method is called after a task completes and triggers the event for all subscribers.
        protected virtual void OnTaskCompleted(TaskCompletedEventArgs e)
        {
            // If there are any subscribers to the event, invoke the event handler.
            // The event handler receives the current instance (this) and the event arguments (e).
            TaskCompleted?.Invoke(this, e);
        }

        #endregion
    }
}
