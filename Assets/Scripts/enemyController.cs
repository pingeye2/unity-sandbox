using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Vector3 pos;
    private float dist;
    private Transform target;
    public float speed;
    public float howclose;
    private List<string> enemyBackpack = new List<string>();

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // finds a new pos every 10 seconds
        InvokeRepeating("randomPos", 2f, 10f);
    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        if (dist <= howclose)
        {
            attack();
        }
        else if (enemyBackpack.Count < 20)
        {
            collectItems(transform.position, 10);
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    // enemy moves towards the player if set to attack and within dist
    public void attack()
    {
        if (Vector3.Distance(transform.position, target.position) > 3)
        {
            pos = target.position;
        }
    }

    // generates a random pos for the enemy to move towards
    public void randomPos()
    {
        float x = Random.Range((transform.position.x - 30), (transform.position.x + 30));
        float z = Random.Range((transform.position.z - 30), (transform.position.z + 30));
        pos = new Vector3(x, 5, z);
    }

    // finds any items with rigidbodies within a set radius and collects them into the enemies backpack
    public void collectItems(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Rigidbody>() != null && hitCollider.name != this.name && hitCollider.tag != "Player")
            {
                pos = hitCollider.transform.position;
                float distToPos = Vector3.Distance(pos, transform.position);
                transform.LookAt(pos);
                if (distToPos < 3)
                {
                    enemyBackpack.Add(hitCollider.gameObject.name);
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }


}
