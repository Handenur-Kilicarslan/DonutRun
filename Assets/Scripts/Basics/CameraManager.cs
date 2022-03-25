using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera FollowCam;
    public CinemachineVirtualCamera WinCam;
    public ParticleSystem Confetti;
    public CinemachineVirtualCamera StartCamLevel1;
    public CinemachineVirtualCamera StartCamOtherLevels;

    void Start()
    {
        Confetti.Stop();
        FollowCam.Follow = LevelManager.Instance.MainPlayer.transform;
        if (PlayerPrefs.GetInt("whichlevel") == 1)
        {
            StartCamLevel1.gameObject.SetActive(false);
            StartCamOtherLevels.gameObject.SetActive(true);
        }
        else
        {
            StartCamLevel1.gameObject.SetActive(true);
            StartCamOtherLevels.gameObject.SetActive(false);
        }
    }

    void Update()
    {

    }
    public void ConfettiManager()
    {
        Confetti.Play();
    }
    public void StartCameras()
    {
        FollowCam.Priority = 5;
    }

    public void SwitchWinCamera()
    {
        WinCam.Priority = 6;
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += StartCameras;
        GameManager.OnGameWin += ConfettiManager;
    }

    private void OnDisable()
    {

        GameManager.OnGameStart -= StartCameras;
        GameManager.OnGameWin -= ConfettiManager;
    }
}
