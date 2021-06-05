using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject player;
    private int worldSizeX = 40;
    private int worldSizeZ = 40;
    private int noiseHeight = 6;
    private Vector3 startPosition;
    private Hashtable blockContainer = new Hashtable();
    private List<Vector3> blockPositions = new List<Vector3>();
    private float randomSpawnNum;
    private GameObject objectToSpawn;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;

    void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    Vector3 pos = new Vector3(x * 1 + xPlayerLocation,
                    generateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight,
                    z * 1 + zPlayerLocation);

                    randomSpawnNum = Random.Range(0f, 50f);

                    if (!blockContainer.ContainsKey(pos))
                    {
                        if (randomSpawnNum < 20f && randomSpawnNum > 0f)
                        {
                            objectToSpawn = block1;
                        }
                        else if (randomSpawnNum < 30f && randomSpawnNum > 20f)
                        {
                            objectToSpawn = block2;
                        }
                        else if (randomSpawnNum < 45f && randomSpawnNum > 30f)
                        {
                            objectToSpawn = block3;
                        }
                        else if (randomSpawnNum < 50f && randomSpawnNum > 45f)
                        {
                            objectToSpawn = block4;
                        }

                        GameObject block = Instantiate(objectToSpawn,
                        pos,
                        Quaternion.identity) as GameObject;
                        blockContainer.Add(pos, block);
                        blockPositions.Add(block.transform.position);
                        block.transform.SetParent(this.transform);
                    }
                }

            }
        }
    }

    public int xPlayerMove
    {
        get
        {
            return (int)(player.transform.position.x - startPosition.x);
        }
    }

    public int zPlayerMove
    {
        get
        {
            return (int)(player.transform.position.z - startPosition.z);
        }
    }

    private int xPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x);
        }
    }

    private int zPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z);
        }
    }

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.z) / detailScale;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
