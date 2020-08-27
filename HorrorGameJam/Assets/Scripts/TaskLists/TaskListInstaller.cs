using UnityEngine;

namespace TaskLists
{
    public class TaskListInstaller : MonoBehaviour
    {
        [SerializeField] private TaskListView _view;
        [SerializeField] private TaskListGroup _taskListGroup;
        
        private void Start()
        {
            var model = new TaskListModel(_taskListGroup.TaskLists.ToArray());
            var mediator = new TaskListMediator(model, _view);
        }
    }
}