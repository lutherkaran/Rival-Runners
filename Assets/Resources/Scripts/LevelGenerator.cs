using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> LevelList;
    [SerializeField] float playerDistance;

    float playerDistanceSquared;

    private Queue<Transform> levelQueue;
    private Vector3 LevelEndPosition;
    private Vector3 lastPosition = Vector3.zero;

    int spawnLevels = 0;

    bool playerAlive = true;

    private void OnEnable()
    {
        PlayerController.OnDied += PlayerAlive;
    }

    private void Awake()
    {
        LevelEndPosition = LevelList[0].transform.Find("EndPosition").position;
        levelQueue = new Queue<Transform>();
        spawnLevels = LevelList.Count;

    }

    private void Start()
    {
        for (int i = 0; i < spawnLevels; i++)
        {
            SpawnLevel();
        }
        StartCoroutine(PlayerDistanceCheck());
    }

    IEnumerator PlayerDistanceCheck()
    {
        float refreshRate = .25f;
        while (playerAlive != false)
        {
            //if (Vector3.Distance(player.position, GetEndPositionOfAllLevels()) < playerDistance)
            playerDistanceSquared = playerDistance * playerDistance;
            if ((player.position - GetEndPositionOfAllLevels()).sqrMagnitude < playerDistanceSquared)
            {
                ShiftLevels();
            }
            yield return new WaitForSeconds(refreshRate);

        }
    }

    private void ShiftLevels()
    {
        if (levelQueue.Count > 0)
        {
            Transform currentLevel = levelQueue.Dequeue();
            currentLevel.position = GetEndPositionOfAllLevels();
            levelQueue.Enqueue(currentLevel);
        }
    }

    private Vector3 GetEndPositionOfAllLevels()
    {
        lastPosition = Vector3.zero;

        foreach (Transform level in levelQueue)
        {
            Transform endPositionObject = level.Find("EndPosition");
            if (endPositionObject != null)
            {
                lastPosition = endPositionObject.position;
            }
        }

        return lastPosition;
    }

    private void SpawnLevel()
    {
        Transform k = LevelList[Random.Range(0, LevelList.Count)].transform;
        Transform t = SpawnLevel(k, LevelEndPosition);
        LevelEndPosition = t.Find("EndPosition").position;
    }

    private Transform SpawnLevel(Transform k, Vector3 SpawnPosition)
    {
        GameObject g = GameObject.Instantiate(k.gameObject, SpawnPosition, Quaternion.identity);
        Transform t = g.transform;
        levelQueue.Enqueue(t);
        return t;
    }

    void PlayerAlive()
    {
        playerAlive = false;
    }
    private void OnDisable()
    {
        PlayerController.OnDied -= PlayerAlive;
    }
}