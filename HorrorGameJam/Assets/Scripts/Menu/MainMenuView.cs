using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _quitGame;
    [SerializeField] private PlayableDirector _sceneChangeAnimation;

    public Button.ButtonClickedEvent OnStartGameClicked => _startGame.onClick;
    public Button.ButtonClickedEvent OnQuitGameClicked => _quitGame.onClick;

    public PlayableDirector SceneChangeAnimation => _sceneChangeAnimation;

    public void DisableButtons()
    {
        _startGame.enabled = false;
        _quitGame.enabled = false;
    }
}
