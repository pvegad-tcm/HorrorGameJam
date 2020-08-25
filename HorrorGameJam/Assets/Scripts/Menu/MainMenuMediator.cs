using System.Collections;
using UnityEditor;
using UnityEngine.Playables;

internal class MainMenuMediator
{
    private readonly MainMenuView _mainMenuView;
    private readonly SceneChanger _sceneChanger;

    public MainMenuMediator(
        MainMenuView mainMenuView, 
        SceneChanger sceneChanger)
    {
        _mainMenuView = mainMenuView;
        _sceneChanger = sceneChanger;

        _mainMenuView.OnQuitGameClicked.AddListener(QuitGame);
        _mainMenuView.OnStartGameClicked.AddListener(LoadGameSceneAndShowScreenLoad);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif    
    }

    private void LoadGameSceneAndShowScreenLoad()
    {
        _mainMenuView.DisableButtons();
        _mainMenuView.SceneChangeAnimation.Play();
        _mainMenuView.SceneChangeAnimation.stopped += AllowSceneChange;
    }

    private void AllowSceneChange(PlayableDirector obj)
    {
        _sceneChanger.ChangeScene();
    }
}