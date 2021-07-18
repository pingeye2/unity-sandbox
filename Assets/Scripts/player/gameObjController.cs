using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameObjController : MonoBehaviour
{
    public Transform buildPos;
    public GameObject buildObj;
    public Camera camera;
    private Ray ray;
    private RaycastHit hit;
    private BoxCollider bc;
    private float howclose = 10;
    private float dist;
    private Text blockCountUI;
    private List<GameObject> collectedObjects = new List<GameObject>();
    void Start()
    {
        blockCountUI = GameObject.Find("blockCountUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (collectedObjects.Count > 0)
            {
                Instantiate(collectedObjects[0], buildPos.position, buildPos.rotation);
                collectedObjects.RemoveAt(0);
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

                    if (dist <= howclose)
                    {
                        GameObject collectedObj = Instantiate(bc.gameObject, new Vector3(0, 0, 0), buildPos.rotation);
                        collectedObjects.Add(collectedObj);
                        Destroy(bc.gameObject);
                    }
                }
            }
        }
        blockCountUI.text = collectedObjects.Count.ToString();

    }
}
