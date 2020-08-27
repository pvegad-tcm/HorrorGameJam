using System.Collections;
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
        
        [Space]
        [SerializeField] private TaskList _taskList;
        [SerializeField] private int _taskId;
        
        public override void OnInteract()
        {
            gameObject.layer = (int)LayerValue.Default;
            StartCoroutine(NextFrameInteraction());
        }

        protected override void OnFinishedInteraction()
        {
            _taskListView.CheckTask(_taskList, _taskId);
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