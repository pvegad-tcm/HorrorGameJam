namespace TaskLists
{
    public class TaskListMediator
    {
        public TaskListMediator(
            TaskListModel model, 
            TaskListView view,
            OnTaskListCompletedCommand onTaskListCompletedCommand
        )
        {
            view.LoadList(model.List, onTaskListCompletedCommand.Execute);
        }
    }

    public class OnTaskListCompletedCommand
    {
        public OnTaskListCompletedCommand()
        {
            
        }
        
        public void Execute()
        {
            
        }
    }
}