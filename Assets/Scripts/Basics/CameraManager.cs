using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineVirtualCamera FollowCam;

    void Start()
    {
        FollowCam.Follow = LevelManager.Instance.MainPlayer.transform;
    }

    void Update()
    {
        
    }
}
