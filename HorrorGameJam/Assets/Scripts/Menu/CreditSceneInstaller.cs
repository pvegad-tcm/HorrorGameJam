using UnityEngine;

public class CreditSceneInstaller : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private CreditSceneView _creditSceneView;
    
    private void Start()
    {
        var mediator = new CreditSceneMediator(_sceneChanger,_creditSceneView);
    }
}