using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public LevelScript From;
    public Door[] AllDoors;

    void InitAllDoors()
    {
        AllDoors = transform.GetComponentsInChildren<Door>();
    }

    private void Awake()
    {
        // InitAllDoors();
    }

    void Start()
    {       
        foreach(var door in AllDoors)
        {
            if (door.IsEnter)
            {
                door.gameObject.SetActive(false);
            }
        }
    }
}
