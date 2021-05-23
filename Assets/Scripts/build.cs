using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour
{
    public Transform buildPos;
    public GameObject buildObj;
    public Camera camera;
    Ray ray;
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildObj, buildPos.position, buildPos.rotation);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                if (hit.rigidbody)
                {
                    hit.rigidbody.gameObject.SetActive(false);
                }
            }
        }

    }
}
