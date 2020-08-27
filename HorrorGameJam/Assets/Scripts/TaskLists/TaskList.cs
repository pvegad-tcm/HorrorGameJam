using UnityEngine;

namespace TaskLists
{
    [CreateAssetMenu(fileName = "Task List", menuName = "Task List")]
    public class TaskList : ScriptableObject
    {
        public string[] Tasks;
    }
}