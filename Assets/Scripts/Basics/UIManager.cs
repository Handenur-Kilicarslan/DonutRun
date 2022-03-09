using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [Header("Canvas System")]
    [SerializeField] private GameObject taptoStart;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelWin;

    //[SerializeField] private GameObject ActiveScore;

    private void Start()
    {
        taptoStart.SetActive(true);
    }

    private void OnStart()
    {
        taptoStart.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void OnWin()
    {
        if (panelLose.activeSelf == false)
        {
            mainPanel.SetActive(false);
            panelWin.SetActive(true);
        }
    }

    private void OnLose()
    {
        if (panelWin.activeSelf == false)
        {
            mainPanel.SetActive(false);
            panelLose.SetActive(true);
        }
    }

    //Updating score for score panel.
    /*
    public void UpdateScore(int addingScore)
    {
        ActiveScore.GetComponent<Text>().text = "" + addingScore;
    }
    */

    private void OnEnable()
    {
        GameManager.OnGameStart += OnStart;
        GameManager.OnGameWin += OnWin;
        GameManager.OnGameLose += OnLose;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnStart;
        GameManager.OnGameWin -= OnWin;
        GameManager.OnGameLose -= OnLose;
    }
}
