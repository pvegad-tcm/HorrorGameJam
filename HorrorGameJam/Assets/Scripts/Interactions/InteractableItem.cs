using System.Collections.Generic;
using NaughtyAttributes;
using TaskLists;
using UnityEngine;
using VHS;

namespace Interactions
{
    public abstract class InteractableItem : InteractableBase
    {
        [SerializeField] private TaskListView _taskListView;

        [BoxGroup("Task Lists")] [SerializeField] private TaskList _taskList;
        [BoxGroup("Task Lists")] [SerializeField] private int _taskId;
        
        [Space] [SerializeField] private List<InteractableItem> _itemsToActivateAfterInteract;

        public override void OnInteract()
        {
            SetLayer((int) LayerValue.Default);
        }

        protected override void OnFinishedInteraction()
        {
            _taskListView.CheckTask(_taskList, _taskId);
            _itemsToActivateAfterInteract.ForEach(item => item.SetLayer((int)LayerValue.Interactable));
        }
    }
}