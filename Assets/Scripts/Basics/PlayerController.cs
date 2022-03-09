using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int MoveSpeed = 14;
    private bool Move = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (Move)
        {
            TouchInputFunction();
            transform.position += Vector3.forward * MoveSpeed * Time.deltaTime;
        }
    }

    private void TouchInputFunction()
    {
        transform.position += Vector3.right * TouchInput.Instance.horizontal * MoveSpeed * Time.deltaTime;
        float xPos = Mathf.Clamp(transform.position.x, -6, 6);
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