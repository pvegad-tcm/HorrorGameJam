using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaskLists
{
    public class TaskListView : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private TaskView _prefab;

        private TaskList _list;
        private readonly List<TaskView> _taskViews = new List<TaskView>();

        public void LoadList(TaskList list, Action onListChecked)
        {
            _list = list;
            HideList();
            
            for (var i = 0; i < list.Tasks.Length; i++)
            {
                if (_taskViews.Count <= i)
                {
                    var taskView = Instantiate(_prefab, _parent);
                    _taskViews.Add(taskView);
                }

                _taskViews[i].SubscribeToTaskCheck(() =>
                {
                    if (_taskViews.All(task => task.IsChecked))
                    {
                        onListChecked.Invoke();
                    }
                });
                
                _taskViews[i].gameObject.SetActive(true);
                _taskViews[i].SetText(list.Tasks[i]);
                _taskViews[i].Check(false);
            }
        }

        public void CheckTask(TaskList list, int taskId)
        {
            if (list == _list)
            {
                _taskViews[taskId].Check();
            }
        }

        public void HideList()
        {
            _taskViews.ForEach(view => view.gameObject.SetActive(false));
        }
    }
}