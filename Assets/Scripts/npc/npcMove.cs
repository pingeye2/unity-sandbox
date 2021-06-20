using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMove : MonoBehaviour
{
    private Vector3 pos;
    private float x;
    private float z;
    private float dist;
    private Transform target;
    public bool shouldNpcAttack;
    public float speed;
    public float howclose;
    private ProceduralGeneration world = new ProceduralGeneration();

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("RandomPos", 0f, 5f);
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= howclose)
        {
            if (shouldNpcAttack)
            {
                if (Vector3.Distance(transform.position, target.position) > 3)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }
    }

    public void RandomPos()
    {
        x = Random.Range((world.x - 50), (world.x + 50));
        z = Random.Range((world.z - 50), (world.z + 50));
        pos = new Vector3(x, 5, z);
    }

}
