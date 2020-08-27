using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaskLists
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _check;

        private Action _taskChecked;

        public bool IsChecked => _check.gameObject.activeSelf;
        
        public void Check(bool check = true)
        {
            _check.gameObject.SetActive(check);
            
            if (check)
            {
                _taskChecked.Invoke();
            }
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SubscribeToTaskCheck(Action action)
        {
            _taskChecked += action;
        }
    }
}