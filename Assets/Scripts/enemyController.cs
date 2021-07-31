using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Vector3 pos;
    private float x;
    private float z;
    private float dist;
    private Transform target;
    public bool shouldNpcAttack;
    public float speed;
    public float howclose;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("randomPos", 2f, 5f);
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= howclose)
        {
            attack();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        }
        collectItems(transform.position, 10);
    }

    // enemy moves towards the player if set to attack and within dist
    public void attack()
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

    // generates a random pos for the enemy to move towards
    public void randomPos()
    {
        x = Random.Range((transform.position.x - 30), (transform.position.x + 30));
        z = Random.Range((transform.position.z - 30), (transform.position.z + 30));

        pos = new Vector3(x, 5, z);
    }

    // finds any items with rigidbodies within a set radius and collects them into the enemies backpack
    public void collectItems(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Rigidbody>() != null && hitCollider.name != this.name)
            {
                // Debug.Log(hitCollider.name);
                pos = hitCollider.transform.position;
            }
        }
    }
}
