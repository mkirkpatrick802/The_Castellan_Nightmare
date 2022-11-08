using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : Spawner
{
    protected override void Spawn()
    {
        float yTransform = spawnZoneCenter.y;
        float spawnX = spawnZone.x / 2;
        float spawnY = spawnZone.y;
        for (int i = 0; i < numberToSpawn; i++)
        {
            AllyController ally = Instantiate(toSpawn, new Vector3(Random.Range(spawnX * -1, spawnX), Random.Range((spawnY * -1) + yTransform, spawnY + yTransform), 0), Quaternion.identity, transform).GetComponent<AllyController>();
            _spawned.Add(ally.transform);
        }
    }
}
