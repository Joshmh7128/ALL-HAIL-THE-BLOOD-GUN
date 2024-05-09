using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SampleMapGenerator : MonoBehaviour
{
    [SerializeField] int mapSize;
    [SerializeField] List<Sprite> groundTiles;
    [SerializeField] GameObject sampleWall;
    List<GameObject> walls = new List<GameObject>();

    private void Start()
    {
        GenerateMap();
    }

    // makes the whole map
    void GenerateMap()
    {
        // place a tile every X, Y units
        for (int x = -mapSize; x < mapSize; x++)
        {
            for (int y = -mapSize ; y < mapSize; y++)
            {
                if (!((x > mapSize / 1.7 && x < mapSize / 2.4) && (y > mapSize / 1.7 && y < mapSize / 2.4)))
                {
                    GameObject sprObj = new GameObject();
                    sprObj.AddComponent<SpriteRenderer>();
                    sprObj.GetComponent<SpriteRenderer>().sprite = groundTiles[Random.Range(0, groundTiles.Count)];
                    sprObj.transform.position = new Vector3(x, y, 5);
                    sprObj.transform.parent = transform;
                    // place either a wall or an enemy, this way we can't have both occur on the same tile
                    int j = Random.Range(0, 100);
                    if (j <= 1) walls.Add(Instantiate(sampleWall, new Vector3(x, y, -1), Quaternion.identity, transform));
                    // reuse this for enemy spawns later
                    if (j >= 99) walls.Add(Instantiate(sampleWall, new Vector3(x, y, -1), Quaternion.identity, transform));
                }
            }
        }
    
        // check to make sure no walls are placed over the player
        foreach (GameObject wall in walls)
        {
            if (Vector2.Distance(wall.transform.position, PlayerController.instance.transform.position) < 5)
            {
                Destroy(wall);
            }
        }
    }
}
