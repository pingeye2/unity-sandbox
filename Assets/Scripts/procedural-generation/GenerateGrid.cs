using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameObject blockGameObject;
    private int wordlSizeX = 10;
    private int wordlSizeZ = 10;
    private int gridOffSet = 2;

    void Start()
    {
        for (int x = 0; x < wordlSizeX; x++)
        {
            for (int z = 0; z < wordlSizeX; z++)
            {
                Vector3 pos = new Vector3(x * gridOffSet,
                0,
                wordlSizeZ * gridOffSet);

                GameObject block = Instantiate(blockGameObject,
                pos,
                Quaternion.identity) as GameObject;

                block.transform.SetParent(this.transform);
            }

        }
    }
}
