using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public LevelScript From;
    public Door[] AllHiddenDoors;

    void Start()
    {
        var levels = (LevelScript[]) GameObject.FindObjectsOfType(typeof(LevelScript));
        gameObject.name = levels.Length.ToString();

        foreach(var door in AllHiddenDoors)
        {
            if (door.From == gameObject.name)
            {
                door.enabled = true;
            }
        }
    }
    
    void Update()
    {
        
    }
}
