using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera FollowCam;
    public ParticleSystem Confetti;

    void Start()
    {
        Confetti.Stop();
        FollowCam.Follow = LevelManager.Instance.MainPlayer.transform;
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
