using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeedLR = 14;

    public float MoveSpeedForward;
    [SerializeField] private float desiredBoundaries = 2;
    private bool Move = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (Move)
        {
            TouchInputFunction();
            transform.position += Vector3.forward * MoveSpeedForward * Time.deltaTime;
        }
    }

    private void TouchInputFunction()
    {
        transform.position += Vector3.right * TouchInput.Instance.horizontal * MoveSpeedLR * Time.deltaTime;
        float xPos = Mathf.Clamp(transform.position.x, -1.7f, 8.5f);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    private void CharacterMove()
    {
        Move = true;
    }

    private void Win()
    {
        Move = false;
    }

    private void Lose()
    {
        Move = false;
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += CharacterMove;
        GameManager.OnGameWin += Win;
        GameManager.OnGameLose += Lose;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= CharacterMove;
        GameManager.OnGameWin -= Win;
        GameManager.OnGameLose -= Lose;
    }
}