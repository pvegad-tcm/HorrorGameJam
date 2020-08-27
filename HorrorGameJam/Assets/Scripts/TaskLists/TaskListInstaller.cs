using UnityEngine;

namespace TaskLists
{
    public class TaskListInstaller : MonoBehaviour
    {
        [SerializeField] private TaskListView _view;

        public void Install(TaskList list)
        {
            var model = new TaskListModel(list);
            var mediator = new TaskListMediator(model, _view, null);
        }
    }
}