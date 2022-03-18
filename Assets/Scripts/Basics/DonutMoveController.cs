using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutMoveController : MonoBehaviour
{
    [SerializeField] private int MoveSpeedLR = 14;
    public float MoveSpeedForward;
    private bool Move = false;


    // Update is called once per frame
    void Update()
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
        float xPos = Mathf.Clamp(transform.position.x, -2.7f, 7f);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    private void CharacterMove()
    {
        Move = true;
    }
    private void CharacterStop()
    {
        Move = false;
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += CharacterMove;
        GameManager.OnGameWin += CharacterStop;
        GameManager.OnGameLose += CharacterStop;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= CharacterMove;
        GameManager.OnGameWin -= CharacterStop;
        GameManager.OnGameLose -= CharacterStop;
    }

}
