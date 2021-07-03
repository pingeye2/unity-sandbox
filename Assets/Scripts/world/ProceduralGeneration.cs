using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject player;
    private int worldSizeX = 40;
    private int worldSizeZ = 40;
    private int noiseHeight = 4;
    private Vector3 startPosition;
    public static Hashtable blockContainer = new Hashtable();
    private List<Vector3> blockPositions = new List<Vector3>();
    private GameObject objectToSpawn;
    private bool loadSceneOnStart = true;
    private GameObject[] blocks = new GameObject[4];
    private Vector3 startPos;

    //when adding new zone create new arr below and amend selectZone()
    public GameObject[] zone1Arr;
    public GameObject[] zone2Arr;
    public GameObject[] zone3Arr;

    void Start()
    {
        startPos = player.transform.position;
        selectZone();
    }

    void Update()
    {
        measurePlayerDis();
        if (Mathf.Abs(xPlayerMove) >= 1 || Mathf.Abs(zPlayerMove) >= 1 || loadSceneOnStart)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
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

    public void measurePlayerDis()
    {
        if (player.transform.position.z > startPos.z + 100 || player.transform.position.x > startPos.x + 100)
        {
            Debug.Log("change zone now");
            selectZone();
            startPos = player.transform.position;
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
        float randomSpawnNum = Random.Range(0f, 50f);

        if (randomSpawnNum < 0.1f && randomSpawnNum > 0f)
        {
            objectToSpawn = blocks[0];
        }
        else if (randomSpawnNum < 0.2f && randomSpawnNum > 0.1f)
        {
            objectToSpawn = blocks[1];
        }
        else if (randomSpawnNum < 0.3f && randomSpawnNum > 0.2f)
        {
            objectToSpawn = blocks[2];
        }
        else if (randomSpawnNum < 50f && randomSpawnNum > 0.3f)
        {
            objectToSpawn = blocks[3];
        }
    }

    private void zones(GameObject[] zoneArr)
    {
        blocks[0] = zoneArr[0];
        blocks[1] = zoneArr[1];
        blocks[2] = zoneArr[2];
        blocks[3] = zoneArr[3];
    }

    private void selectZone()
    {
        float changeZoneNum = Random.Range(0f, 30f);

        if (changeZoneNum > 0 && changeZoneNum < 10)
        {
            zones(zone1Arr);
        }
        else if (changeZoneNum > 10 && changeZoneNum < 20)
        {
            zones(zone2Arr);
        }
        else if (changeZoneNum > 20 && changeZoneNum < 30)
        {
            zones(zone3Arr);
        }
    }
}
