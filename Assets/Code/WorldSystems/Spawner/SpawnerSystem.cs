using Notteam.World;
using System.Collections;
using UnityEngine;

public class SpawnerSystem : WorldSystem<SpawnPoint>
{
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;

    [Space]
    [SerializeField] private GameObject[] spawnObjects;

    private int _currentIndexSpawnObject;
    private int _currentIndexSpawnPoint;
    private int _currentTime;

    protected override void OnStart()
    {
        SetRandomTime();
        SetRandomIndexSpawnObject();

        StartCoroutine(SpawnCycle());
    }

    private void SetRandomTime()
    {
        _currentTime = Random.Range(minTime, maxTime);
    }

    private void SetRandomIndexSpawnObject()
    {
        _currentIndexSpawnObject = Random.Range(0, spawnObjects.Length);
        _currentIndexSpawnPoint = Random.Range(0, entities.Count);
    }

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            World.Instance.GetSystem<PoolObjectSystem>().Create(
                spawnObjects[_currentIndexSpawnObject].name,
                entities[_currentIndexSpawnPoint].Position,
                entities[_currentIndexSpawnPoint].Rotation);

            yield return new WaitForSeconds(_currentTime);

            SetRandomTime();
            SetRandomIndexSpawnObject();
        }
    }
}
