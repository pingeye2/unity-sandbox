using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour
{
    public Transform buildPos;
    public GameObject buildObj;
    public Camera camera;
    int blockCount = 0;
    Ray ray;
    RaycastHit hit;
    BoxCollider bc;

    void Update()
    {
        //build
        if (Input.GetMouseButtonDown(0))
        {
            if (blockCount > 0)
            {
                Instantiate(buildObj, buildPos.position, buildPos.rotation);
                blockCount--;
            }
        }
        //delete
        if (Input.GetMouseButtonDown(1))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    Destroy(bc.gameObject);
                    blockCount++;
                }
            }
        }

    }
}
