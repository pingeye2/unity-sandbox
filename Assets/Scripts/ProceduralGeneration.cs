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
    private GameObject block0;
    private GameObject block1;
    private GameObject block2;
    private GameObject block3;
    private float changeZone;
    public GameObject[] zone1Arr;
    public GameObject[] zone2Arr;
    public GameObject[] zone3Arr;

    void Start()
    {
        InvokeRepeating("zoneChangeTimer", 0f, 10f);
    }

    void Update()
    {
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    selectZone();
                    Vector3 pos = new Vector3(x * 1 + xPlayerLocation,
                    generateNoise(x + xPlayerLocation, z + zPlayerLocation, 8f) * noiseHeight,
                    z * 1 + zPlayerLocation);

                    if (!blockContainer.ContainsKey(pos))
                    {
                        selectBlocks();
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

    private void selectBlocks()
    {
        randomSpawnNum = Random.Range(0f, 50f);

        if (randomSpawnNum < 20f && randomSpawnNum > 0f)
        {
            objectToSpawn = block0;
        }
        else if (randomSpawnNum < 30f && randomSpawnNum > 20f)
        {
            objectToSpawn = block1;
        }
        else if (randomSpawnNum < 45f && randomSpawnNum > 30f)
        {
            objectToSpawn = block2;
        }
        else if (randomSpawnNum < 50f && randomSpawnNum > 45f)
        {
            objectToSpawn = block3;
        }
    }

    private void zones(GameObject[] zoneArr, int zoneNum)
    {
        block0 = zoneArr[0];
        block1 = zoneArr[1];
        block2 = zoneArr[2];
        block3 = zoneArr[3];

        switch (zoneNum)
        {
            case 1:
                noiseHeight = 5;
                break;
            case 2:
                noiseHeight = 10;
                break;
            case 3:
                noiseHeight = 2;
                break;
        }
    }

    private void selectZone()
    {
        if (changeZone > 0 && changeZone < 10)
        {
            zones(zone1Arr, 1);
        }
        else if (changeZone > 10 && changeZone < 20)
        {
            zones(zone2Arr, 2);
        }
        else if (changeZone > 20 && changeZone < 30)
        {
            zones(zone3Arr, 3);
        }
    }

    private void zoneChangeTimer()
    {
        changeZone = Random.Range(0f, 30f);
    }
}
