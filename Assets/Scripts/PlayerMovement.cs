using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSensivity;
    [SerializeField] float health;
    [SerializeField] Camera gameCamera;

    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private Vector2 _mouseMove;
    private float xRotation = 0f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        _characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>();
        _moveDirection = (moveDirection.y * transform.forward) + (moveDirection.x * transform.right);
    }

    public void OnVerticalCameraMove(InputAction.CallbackContext context)
    {
        _mouseMove = context.ReadValue<Vector2>();

        float mouseX = _mouseMove.x * mouseSensivity * Time.deltaTime;
        float mouseY = _mouseMove.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40, 40);

        gameCamera.transform.rotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

}
