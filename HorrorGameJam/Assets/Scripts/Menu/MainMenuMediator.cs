using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

internal class MainMenuMediator
{
    private readonly MainMenuView _mainMenuView;
    private readonly SceneAsset _gameplayScene;

    public MainMenuMediator(
        MainMenuView mainMenuView, 
        SceneAsset gameplayScene)
    {
        _mainMenuView = mainMenuView;
        _gameplayScene = gameplayScene;

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
        SceneManager.LoadScene(_gameplayScene.name);
    }
}