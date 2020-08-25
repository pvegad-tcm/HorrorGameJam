using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private SceneAsset _sceneToLoad;

        public void ChangeScene()
        {
            SceneManager.LoadScene(_sceneToLoad.name);
        }
    }
}