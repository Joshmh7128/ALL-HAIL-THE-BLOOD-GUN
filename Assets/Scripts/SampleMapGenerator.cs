using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SampleMapGenerator : MonoBehaviour
{
    [SerializeField] int mapSize;
    [SerializeField] List<Sprite> groundTiles;
    [SerializeField] GameObject sampleWall;

    private void Start()
    {
        GenerateMap();
    }

    // makes the whole map
    void GenerateMap()
    {
        // place a tile every X, Y units
        for (int x = -mapSize/2; x < mapSize; x++)
        {
            for (int y = -mapSize / 2; y < mapSize; y++)
            {
                GameObject sprObj = new GameObject();
                sprObj.AddComponent<SpriteRenderer>();
                sprObj.GetComponent<SpriteRenderer>().sprite = groundTiles[Random.Range(0, groundTiles.Count)];
                sprObj.transform.position = new Vector3(x, y, 5);
                sprObj.transform.parent = transform;
                // place either a wall or an enemy, this way we can't have both occur on the same tile
                int j = Random.Range(0, 100);
                if (j <= 1) Instantiate(sampleWall, new Vector3(x, y, -1), Quaternion.identity, transform);
                // reuse this for enemy spawns later
                if (j >= 99) Instantiate(sampleWall, new Vector3(x, y, -1), Quaternion.identity, transform);
            }
        }
    }
}
