using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow, IsWinGame, IsLoseGame;
    public static int NamberEnemy;
    public static CanvasManager CanvasMain;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Image _progresBar;
    [SerializeField]
    private Text _txtGameUICurentLevel, _txtGameUITargetLevel, _txtWinUI;

    private float _fillNaber, _namberProgresBar;
    private void Awake()
    {
        CanvasMain = this;
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        FacebookManager.Instance.GameStart();

        if (PlayerPrefs.GetInt("Level") <= 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        _txtGameUICurentLevel.text = PlayerPrefs.GetInt("Level").ToString();
        _txtGameUITargetLevel.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        _txtWinUI.text = "Level " + PlayerPrefs.GetInt("Level");

        _fillNaber = 1f / NamberEnemy;
        NamberEnemy = 0;
        if (!IsGameFlow)
        {
            _menuUI.SetActive(true);
            IsGameFlow = true;
        }
        else
        {
            FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));

            IsStartGeme = true;
        }
    }

    private void Update()
    {
        if (!_inGameUI.activeSelf && IsStartGeme)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }

        if (!_wimIU.activeSelf && IsWinGame)
        {
            IsStartGeme = false;
            FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));
            PlayerPrefs.SetInt("Scenes", PlayerPrefs.GetInt("Scenes") + 1);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }

        if (!_lostUI.activeSelf && IsLoseGame)
        {
            IsStartGeme = false;
            FacebookManager.Instance.LevelFail(PlayerPrefs.GetInt("Level"));

            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_namberProgresBar > _progresBar.fillAmount)
        {
            _progresBar.fillAmount += 0.02f;
            if (_progresBar.fillAmount >= 1 && !IsLoseGame)
            {
                IsWinGame = true;
            }
        }
    }
    public void AddProgress()
    {
        _namberProgresBar += _fillNaber;
    }
}
