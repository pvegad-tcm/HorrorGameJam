using UnityEngine;

public class CoroutineMaker : MonoBehaviour
{
    public static CoroutineMaker Instance => instance;
    private static CoroutineMaker instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}