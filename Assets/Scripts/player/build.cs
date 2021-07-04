using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class build : MonoBehaviour
{
    public Transform buildPos;
    public GameObject buildObj;
    public Camera camera;
    private int blockCount = 0;
    private Ray ray;
    private RaycastHit hit;
    private BoxCollider bc;
    private float howclose = 10;
    private float dist;
    private Text blockCountUI;
    void Start()
    {
        blockCountUI = GameObject.Find("blockCountUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (blockCount > 0)
            {
                Instantiate(buildObj, buildPos.position, buildPos.rotation);
                blockCount--;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    dist = Vector3.Distance(transform.position, bc.gameObject.transform.position);

                    if (bc.gameObject.tag == "build_block" && dist <= howclose)
                    {
                        Destroy(bc.gameObject);
                        blockCount++;
                    }
                }
            }
        }
        blockCountUI.text = blockCount.ToString();

    }
}
