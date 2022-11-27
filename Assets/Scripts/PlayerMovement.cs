using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] Animator animator;

    [SerializeField] float Speed = 5f; 

    [SerializeField] float rotationSpeed;

    [SerializeField] Transform cameraTransform;

    float horizontalInput;
    float verticalInput;
    float inputMagnitude;

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        animator.SetFloat("inputMag", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        
        //Player Rotate
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


        //Player Move
        characterController.Move(movementDirection * Time.deltaTime * Speed);

    }


    



}
