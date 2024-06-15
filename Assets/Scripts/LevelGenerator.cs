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
        if (counter <= 0)
        {
            return;
        }

        LevelScript level;
        do
        {
            level = GetRandomLevelPrefab();
        }
        while (level.AllDoors.Length <= 1);

        level = Instantiate(level, spawnPoint.transform.position, Quaternion.identity);
        SetThisLevelInWorldSpace(level, spawnPoint, counter == CountsOfLevels);

        foreach (var door in level.AllDoors)
        {
            if (!door.IsEnter)
            {
                StartCoroutine(DoAfterTime(5f, door, counter));
            }
        }
    }

    IEnumerator DoAfterTime(float timer, Door door, int counter)
    {
        yield return new WaitForSeconds(timer);
        InitLevel(door, counter - 1);
    }

    IEnumerator Test_Positioning(float timer, Door door, Door otherDoor, LevelScript level)
    {
        yield return new WaitForSeconds(timer);

        float distanceX = otherDoor.transform.position.x - door.transform.position.x;
        float distanceZ = otherDoor.transform.position.z - door.transform.position.z;
        Debug.Log($"{level.name} with position {level.transform.position} to {otherDoor.transform.parent.position}:" +
            $" shift is X: {distanceX} Z: {distanceZ}");
        Debug.Log($"Door {door.transform.position} to {otherDoor.transform.position}");
        Vector3 shiftedPosition = new Vector3(level.transform.position.x + distanceX,
                                                0f,
                                                level.transform.position.z + distanceZ);
        level.transform.position = shiftedPosition;
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
        level.transform.Rotate(-otherDoor.transform.rotation.eulerAngles);
        SpawnedLevels.Add(level);

        Door door = level.AllDoors.First(d => d.IsEnter);
        if (isThisAFirstLevel)
        {
            door.gameObject.SetActive(true);
        }

        StartCoroutine(Test_Positioning(2f, door, otherDoor, level));
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
