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
    [SerializeField] Vector3 position;

    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private Vector2 _mouseMove;
    private float xRotation = 0f;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAround();
    }
    

    public void Move()
    {
        Vector2 moveDirection = _playerInput.actions.FindAction("Movement").ReadValue<Vector2>();
        _moveDirection = (moveDirection.y * transform.forward) + (moveDirection.x * transform.right);
        _characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    public void LookAround()
    {
        _mouseMove = _playerInput.actions.FindAction("LookAround").ReadValue<Vector2>();

        float mouseX = _mouseMove.x * mouseSensivity * Time.deltaTime;
        float mouseY = _mouseMove.y * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40, 40);

        gameCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.Rotate(Vector3.up * mouseX);
    }

}
