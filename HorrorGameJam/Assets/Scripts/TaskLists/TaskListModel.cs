namespace TaskLists
{
    public class TaskListModel
    {
        public int SelectedListId = 0;
        private readonly TaskList[] _lists;

        public bool NextListExists => _lists.Length > SelectedListId;
        public TaskList List => _lists[SelectedListId];
        
        public TaskListModel(TaskList[] lists)
        {
            _lists = lists;
        }
    }
}