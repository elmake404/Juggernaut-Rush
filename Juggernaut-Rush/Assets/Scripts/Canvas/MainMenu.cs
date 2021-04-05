using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _tyutorialUI;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerPrefs.GetInt("FirstEntey")<=0)
            {
                FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));
                gameObject.SetActive(false);
                _tyutorialUI.SetActive(true);
                PlayerPrefs.SetInt("FirstEntey", 1);
            }
            else
            {
                CanvasManager.IsStartGeme = true;
                gameObject.SetActive(false);
            }
        }
    }
}
