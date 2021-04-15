using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Image _rageBar,_levelBar;
    private PlayerLife _playerLife;

    private void Start()
    {
        _rageBar.fillAmount = 1;
        _playerLife = FindObjectOfType<PlayerLife>();
    }
    private void FixedUpdate()
    {
        _rageBar.fillAmount = Mathf.LerpUnclamped(_rageBar.fillAmount, _playerLife.GetAmoutRage(), 0.1f);
    }
    public void GameStageWindow(Stage stageGame)
    {
        switch (stageGame)
        {
            case Stage.StartGame:

                _menuUI.SetActive(true);
                _inGameUI.SetActive(false);
                break;

            case Stage.StartLevel:

                _menuUI.SetActive(false);
                _inGameUI.SetActive(true);
                break;

            case Stage.WinGame:

                _wimIU.SetActive(true);
                //впиши сюда поднятие уровня и сцены 
                break;

            case Stage.LostGame:

                _lostUI.SetActive(true);
                break;
        }
    }
}
