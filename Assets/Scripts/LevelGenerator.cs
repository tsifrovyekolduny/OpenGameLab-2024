using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singletone<LevelGenerator>
{
    public int CountsOfLevels;
    public LevelScript[] LevelPrefabs;
    
    void SpawnLevels(Vector3 spawnPoint)
    {
        LevelScript level;
        do
        {
            level = GetRandomLevelPrefab();
        }
        while (level.AllHiddenDoors.Length <= 1);        
    }

    LevelScript GetRandomLevelPrefab()
    {
        return new LevelScript();
    }


    void Update()
    {
        
    }
}
