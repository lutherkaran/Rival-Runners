using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LevelGenerator : NetworkBehaviour
{
    Transform playerTarget;
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

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialize();
    }

    private void Initialize()
    {
        if (!IsOwner) return;
        playerTarget = PlayerController.Instance.gameObject.transform;
        LevelEndPosition = LevelList[0].transform.Find("EndPosition").position;
        levelQueue = new Queue<Transform>();
        spawnLevels = LevelList.Count;
        for (int i = 1; i < spawnLevels; i++)
        {
            SpawnLevel(LevelList[i]);
        }
        if (playerTarget)
            StartCoroutine(PlayerDistanceCheck());
    }

    IEnumerator PlayerDistanceCheck()
    {

        float refreshRate = .25f;
        while (playerAlive != false)
        {
            playerDistanceSquared = playerDistance * playerDistance;
            if ((playerTarget.position - GetEndPositionOfAllLevels()).sqrMagnitude < playerDistanceSquared)
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

    private void SpawnLevel(Transform i)
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