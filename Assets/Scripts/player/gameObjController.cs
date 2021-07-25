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
    private Transform backpackPos;

    void Start()
    {
        backpackPos = GameObject.FindGameObjectWithTag("backpack").GetComponent<Transform>();
        blockCountUI = GameObject.Find("blockCountUI").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (collectedObjects.Count > 0)
            {
                GameObject projectile = Instantiate(collectedObjects[0], buildPos.position, buildPos.rotation);
                projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 8000);
                Destroy(projectile.GetComponent<backpackFollow>());
                Destroy(collectedObjects[0]);
                collectedObjects.RemoveAt(0);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                bc = hit.collider as BoxCollider;

                if (bc != null && bc.gameObject.GetComponent<Rigidbody>() != null)
                {
                    dist = Vector3.Distance(transform.position, bc.gameObject.transform.position);

                    if (dist <= howclose)
                    {
                        GameObject collectedObj = Instantiate(bc.gameObject, backpackPos.position, backpackPos.rotation);
                        collectedObj.AddComponent<backpackFollow>();
                        collectedObjects.Add(collectedObj);
                        Destroy(bc.gameObject);
                    }
                }
            }
        }
        blockCountUI.text = collectedObjects.Count.ToString();

    }
}
