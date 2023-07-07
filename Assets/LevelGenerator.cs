using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> LevelList;

    [SerializeField] float playerDistance;
    private List<GameObject> newLevelList;
    public Queue<Transform> levelQueue;

    private Vector3 LevelEndPosition;
    private Vector3 LevelStartPosition = Vector3.zero;
    private Vector3 offset = new Vector3(0, 0, 2.5f);
    private bool isShifted = false;

    int spawnLevels = 0;

    private void Awake()
    {
        LevelEndPosition = LevelList[0].transform.Find("EndPosition").position;
        levelQueue = new Queue<Transform>();

        spawnLevels = LevelList.Count;
        for (int i = 0; i < spawnLevels; i++)
        {
            SpawnLevel();
        }
    }

    private void Update()
    {
        if (Player.instance.PlayerAlive())
        {

            if ((Vector3.Distance(player.position, LevelEndPosition + LevelStartPosition) < playerDistance) && !isShifted)
            {
                ShiftLevels();
            }
            isShifted = false;
        }

    }

    private void ShiftLevels()
    {
        if (levelQueue.Count > 0)
        {
            Transform t = levelQueue.Dequeue();
            t.gameObject.transform.position = LevelEndPosition;
            LevelStartPosition = LevelEndPosition;
            LevelEndPosition = t.gameObject.transform.Find("EndPosition").position;
            //LevelEndPosition = levelQueue.Peek().Find("EndPosition").position;
            levelQueue.Enqueue(t);

            Debug.Log("Moved");
        }
        isShifted = true;
    }

    private void SpawnLevel()
    {
        Transform k = LevelList[Random.Range(0, LevelList.Count)].transform;
        Transform t = SpawnLevel(k, LevelEndPosition);
        LevelStartPosition = LevelEndPosition;
        LevelEndPosition = t.Find("EndPosition").position;
    }

    private Transform SpawnLevel(Transform k, Vector3 SpawnPosition)
    {
        GameObject g = GameObject.Instantiate(k.gameObject, SpawnPosition, Quaternion.identity);
        Transform t = g.transform;
        levelQueue.Enqueue(t);
        return t;
    }
}

// First Spawn All
// Select one randomly and move its position to the next one which means the end of the last one.