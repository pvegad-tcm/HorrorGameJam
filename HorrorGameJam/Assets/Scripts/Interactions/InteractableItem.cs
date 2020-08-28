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
        [SerializeField] private GameObject _soundObject;
        
        [BoxGroup("Task Lists")] [SerializeField] private TaskList _taskList;
        [BoxGroup("Task Lists")] [SerializeField] private int[] _taskIds;
        
        [BoxGroup("Items to Activate after Interact")] [SerializeField] 
        private List<InteractableItem> _itemsToActivateAfterInteract;

        public override void OnInteract()
        {
            SetLayer((int) LayerValue.Default);
            _soundObject?.SetActive(true);
        }

        protected override void OnFinishedInteraction()
        {
            _taskListView.CheckTask(_taskList, _taskIds);
            _itemsToActivateAfterInteract.ForEach(item => item.SetLayer((int)LayerValue.Interactable));
            _soundObject?.SetActive(false);
        }
    }
}