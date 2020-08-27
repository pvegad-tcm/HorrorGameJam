using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TaskLists
{
    [CreateAssetMenu(fileName = "TaskListGroup", menuName = "Task List Group")]
    public class TaskListGroup : ScriptableObject
    {
        [ReorderableList] public List<TaskList> TaskLists;
    }
}