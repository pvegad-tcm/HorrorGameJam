using UnityEngine.Playables;

namespace Menu
{
    public class CreditSceneMediator
    {
        private readonly SceneChanger _sceneChanger;

        public CreditSceneMediator(
            SceneChanger sceneChanger, 
            CreditSceneView creditSceneView)
        {
            _sceneChanger = sceneChanger;
            creditSceneView.CreditsAnimationTimeline.stopped += ChangeScene;
        }

        private void ChangeScene(PlayableDirector obj)
        {
            _sceneChanger.ChangeScene();
        }
    }
}