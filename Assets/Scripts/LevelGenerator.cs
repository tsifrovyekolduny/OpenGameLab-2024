using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : Singletone<LevelGenerator>
{
    public int CountsOfLevels;
    public LevelScript[] LevelPrefabs;
    public Door FirstSpawnPoint;

    public List<LevelScript> SpawnedLevels;

    protected override void Start()
    {
        base.Start();
        InitLevel(FirstSpawnPoint, CountsOfLevels);
    }

    void InitLevel(Door spawnPoint, int counter)
    {
        if(counter <= 0)
        {
            return;
        }

        LevelScript level;
        do
        {
            level = GetRandomLevelPrefab();
        }
        while (level.AllDoors.Length <= 1);

        SetThisLevelInWorldSpace(level, spawnPoint, counter == CountsOfLevels);

        foreach (var door in level.AllDoors)
        {
            if (!door.IsEnter)
            {
                InitLevel(door, counter - 1);
            }
        }
    }

    void GenerateNewCycle()
    {
        RemoveLevels();
    }

    void RemoveLevels()
    {
                
    }

    void SetThisLevelInWorldSpace(LevelScript level, Door otherDoor, bool isThisAFirstLevel)
    {
        level = Instantiate(level, otherDoor.transform.position, Quaternion.identity);
        level.transform.Rotate(otherDoor.transform.rotation.eulerAngles);
        SpawnedLevels.Add(level);

        Door door = level.AllDoors.First(d => d.IsEnter);
        if (isThisAFirstLevel)
        {
            door.gameObject.SetActive(true);
        }
        float distanceX = otherDoor.transform.position.x - door.transform.position.x;
        float distanceZ = otherDoor.transform.position.z - door.transform.position.z;
        Vector3 shiftedPosition = new Vector3(level.transform.position.x + distanceX,
                                                0f,
                                                level.transform.position.z + distanceZ);
        level.transform.position = shiftedPosition;
    }

    LevelScript GetRandomLevelPrefab()
    {
        int randomIndex = Random.Range(0, LevelPrefabs.Count());
        return LevelPrefabs[randomIndex];
    }

    void Update()
    {

    }
}
