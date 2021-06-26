using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundBlocks : MonoBehaviour
{
    public static void moveYBlock(GameObject block)
    {
        Vector3 pos = new Vector3(block.transform.position.x, (block.transform.position.y - block.transform.localScale.y), block.transform.position.z);
        Instantiate(block, pos, block.transform.rotation);
        Destroy(block);
    }
}
