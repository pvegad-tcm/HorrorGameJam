using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TaskLists;
using UnityEngine;
using VHS;

namespace Interactions
{
    public class InteractableItem : InteractableBase
    {
        [SerializeField] private QTEInstaller _qteInstaller;
        [SerializeField] private QuickTimeEventTemplate _template;
        [SerializeField] private TaskListView _taskListView;

        [BoxGroup("Task Lists")] [SerializeField] private TaskList _taskList;
        [BoxGroup("Task Lists")] [SerializeField] private int _taskId;
        
        [Space] [ReorderableList] [SerializeField] 
        private List<InteractableItem> _itemsToActivateAfterInteract;
        
        public override void OnInteract()
        {
            gameObject.layer = (int)LayerValue.Default;
            StartCoroutine(NextFrameInteraction());
        }

        protected override void OnFinishedInteraction()
        {
            _taskListView.CheckTask(_taskList, _taskId);
            _itemsToActivateAfterInteract.ForEach(item => item.gameObject.layer = (int)LayerValue.Interactable);
        }

        private IEnumerator NextFrameInteraction()
        {
            yield return null;
            _qteInstaller.Install(_template, OnFinishedInteraction);
            
            yield return null;
            Destroy(this);
        }
    }
}