using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsEnter;
    public string From;
    public string To;
    public float OpeningSpeed = 100f;
    private Animator _animator;
    public void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void OpenDoor()
    {
        _animator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        _animator.SetTrigger("Close");
    }
}
