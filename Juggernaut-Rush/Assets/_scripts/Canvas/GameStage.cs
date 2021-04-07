﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStageEvent
{
    public delegate void Empty();
    public static event Empty StartLevel;
    public static void InvokeStartLevel()
    {
        StartLevel.Invoke();
    }
}
public enum Stage { StartGame, StartLevel, WinGame, LostGame }
public class GameStage : MonoBehaviour
{
    public static GameStage Instance;

    private static bool _isGameStart;
    public static bool IsGameFlowe
    { get; private set; }

    [SerializeField]
    private CanvasManager _canvasManager;
    public Stage StageGame
    { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeStage(_isGameStart ? Stage.StartLevel : Stage.StartGame);
    }

    void Update()
    {

    }
    public void ChangeStage(Stage stage)
    {
        StageGame = stage;
        _canvasManager.GameStageWindow(StageGame);

        switch (stage)
        {
            case Stage.StartGame:

                _isGameStart = true;
                break;

            case Stage.StartLevel:

                GameStageEvent.InvokeStartLevel();
                IsGameFlowe = true;
                break;

            case Stage.WinGame:

                IsGameFlowe = false;
                //впиши сюда поднятие уровня и сцены 
                break;

            case Stage.LostGame:

                IsGameFlowe = false;
                break;
        }

    }
    public void LevelStart()
    {
        ChangeStage(Stage.StartLevel);
    }
}
