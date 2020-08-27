namespace TaskLists
{
    public class TaskListMediator
    {
        private readonly TaskListModel _model;
        private readonly TaskListView _view;

        public TaskListMediator(
            TaskListModel model, 
            TaskListView view
        )
        {
            _model = model;
            _view = view;
            
            view.LoadList(model.List, OnTaskListCompleted);
        }

        private void OnTaskListCompleted()
        {
            _model.SelectedListId++;
            
            if (_model.NextListExists)
            {
                _view.LoadList(_model.List, OnTaskListCompleted);
                return;
            }
            
            _view.HideList();
        }
    }
}