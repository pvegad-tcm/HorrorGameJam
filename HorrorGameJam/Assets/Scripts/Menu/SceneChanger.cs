using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;

        private void Start()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}