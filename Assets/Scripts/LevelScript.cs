using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelScript : MonoBehaviour
{
    public LevelScript From;
    public Door[] AllDoors;
    public Action OnCompletingLevel;
    public Action OnEnterLevel;
    
    private bool _isCompleted = false;

    // todo переделать как появятся пазлы или враги
    public GameObject[] Enemies;
    public GameObject[] Puzzles;

    private void Update()
    {
        if(Enemies.Length == 0 && Puzzles.Length == 0 && !_isCompleted)
        {
            _isCompleted = true;
            OnCompletingLevel.Invoke();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"other is {other.name} {other.tag}");
        if(other.tag == "Player")
        {
            Debug.Log($"player entered {gameObject.name}");
            
            OnEnterLevel.Invoke();
        }
        
    }

    void Start()
    {       
        foreach(var door in AllDoors)
        {
            if (door.IsEnter)
            {
                door.gameObject.SetActive(false);
            }
            else
            {
                OnCompletingLevel += door.OpenDoor;
            }
        }
    }
}
