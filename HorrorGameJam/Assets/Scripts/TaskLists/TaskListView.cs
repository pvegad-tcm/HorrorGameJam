using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TaskLists
{
    public class TaskListView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleList;
        [SerializeField] private Transform _parent;
        [SerializeField] private TaskView _prefab;

        private TaskList _list;
        private readonly List<TaskView> _taskViews = new List<TaskView>();

        public void LoadList(TaskList list, Action onListChecked)
        {
            _list = list;
            HideList();
            if (gameObject.activeSelf)
            {
                SendMessage("Play");
            }

            _titleList.text = list.Title;
            
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

        public void CheckTask(TaskList list, int[] taskId)
        {
            if (list != _list) return;
            
            foreach (var id in taskId)
            {
                _taskViews[id].Check();
            }
        }
        public void CheckAllTasks()
        {
            for (int i = 0; i < _taskViews.Count; i++)
            {
                _taskViews[i].Check();
            }
        }

        public void HideList()
        {
            _titleList.text = "";
            _taskViews.ForEach(view => view.gameObject.SetActive(false));
        }
    }
}