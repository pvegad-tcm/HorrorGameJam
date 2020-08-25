﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuInstaller : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private SceneChanger _sceneChanger;

    private void Start()
    {
        var mediator = new MainMenuMediator(_mainMenuView, _sceneChanger);
    }
}