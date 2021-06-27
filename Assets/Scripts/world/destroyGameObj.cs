using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add to npcs so if they fall of the world they get destroyed
public class destroyGameObj : MonoBehaviour
{
    private Transform target;
    private float dist;
    private float howclose = 1000f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist > howclose)
        {
            Debug.Log("gone");
            Destroy(gameObject);
        }

    }
}
