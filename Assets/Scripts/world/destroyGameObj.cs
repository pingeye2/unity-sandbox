using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyGameObj : MonoBehaviour
{
    private Transform target;
    private float dist;
    private float howclose = 20f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist > howclose)
        {
            ProceduralGeneration.blockContainer.Remove(transform.position);
            Destroy(gameObject);
        }

    }
}
