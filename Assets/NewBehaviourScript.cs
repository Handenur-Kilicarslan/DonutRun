using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController characterController; //kendi rigidbodysi var

    private float currentSpeed;
    private float horizontalInput;
    private float verticalInput;


    Vector3 movementVector;
    public float runSpeed;
    public float walkSpeed;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInputFunc();
    }
    private void FixedUpdate()
    {
        Vector3 localVertical = transform.forward * verticalInput;
        Vector3 localHorizontal = transform.right * horizontalInput;

        movementVector = localHorizontal + localVertical;
        movementVector.Normalize(); // vektörü 1'e eþitledi

        movementVector *= currentSpeed * Time.deltaTime;

        characterController.Move(movementVector);
    }

    private void KeyboardInputFunc()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.LeftShift)) //koþsun tuþa basýnca
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }
}
