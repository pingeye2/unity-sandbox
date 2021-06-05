using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    GameObject block1;
    GameObject block2;
    GameObject block3;
    GameObject block4;
    public string zoneName;
    void Start()
    {
        block1 = GameObject.FindGameObjectWithTag(createTag(zoneName, "1"));
        block2 = GameObject.FindGameObjectWithTag(createTag(zoneName, "2"));
        block3 = GameObject.FindGameObjectWithTag(createTag(zoneName, "3"));
        block4 = GameObject.FindGameObjectWithTag(createTag(zoneName, "4"));
    }

    public GameObject selectBlocks()
    {
        float randomSpawnNum = Random.Range(0f, 50f);
        if (randomSpawnNum < 20f && randomSpawnNum > 0f)
        {
            return block1;
        }
        else if (randomSpawnNum < 30f && randomSpawnNum > 20f)
        {
            return block2;
        }
        else if (randomSpawnNum < 45f && randomSpawnNum > 30f)
        {
            return block3;
        }
        else if (randomSpawnNum < 50f && randomSpawnNum > 45f)
        {
            return block4;
        }
        return block1;
    }

    public string createTag(string tag, string num)
    {
        Debug.Log(tag + "_block" + num);
        return tag + "_block" + num;
    }

}
