using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  npcController -> all characteristics of the npc's are randomly selected on initialization  */
public class npcController : MonoBehaviour
{
    private Vector3 pos;
    private float dist;
    private Transform target;
    private string inRangePower;
    private string outRangePower;
    public float speed;
    public float howclose;
    public float health;
    public Transform firePos;
    public GameObject associatedObj;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("randomPos", 2f, 5f);
        selectPowers();

    }

    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        // destroy npc and drop loot
        if (health < 0)
        {
            Destroy(gameObject);
            dropItems(100);
        }
        // what the npc does if the player is in range
        if (dist <= howclose)
        {
            inRangeManager();
        }
        else
        {
            outRangeManager();
        }

        transform.LookAt(pos);
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }

    /********** core methods ***********/

    // generates a random pos for the npc to move towards
    void randomPos()
    {
        float x = Random.Range((transform.position.x - 30), (transform.position.x + 30));
        float z = Random.Range((transform.position.z - 30), (transform.position.z + 30));
        pos = new Vector3(x, 5, z);
    }

    // takes down npc's health based on the force a object hits them
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 100)
        {
            health -= 50;
        }
        else if (collision.relativeVelocity.magnitude > 60)
        {
            health -= 30;
        }
        else if (collision.relativeVelocity.magnitude > 40)
        {
            health -= 20;
        }
        else if (collision.relativeVelocity.magnitude > 20)
        {
            health -= 10;
        }
    }

    // loot dropping
    void dropItems(int dropAmount)
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Instantiate(associatedObj, transform.position, transform.rotation);
        }
    }

    // randomly selects what powers the npc has
    void selectPowers()
    {
        float rand = Random.Range(0, 2);
        if (rand == 1)
        {
            inRangePower = "shoot";
            outRangePower = "collect";
        }
    }

    void inRangeManager()
    {
        if (inRangePower == "shoot")
        {
            powerShoot();
        }
    }

    void outRangeManager()
    {
        if (outRangePower == "collect")
        {
            powerCollect(transform.position, 10);
        }
    }

    /********** power: collect ***********/

    // finds any items with rigidbodies within a set radius and removes them from the world
    void powerCollect(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Rigidbody>() != null && hitCollider.name != this.name && hitCollider.tag != "Player")
            {
                pos = hitCollider.transform.position;
                float distToPos = Vector3.Distance(pos, transform.position);
                if (distToPos < 3)
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }

    /********** power: shoot ***********/

    private bool canShoot = true;
    public float shootDelay;
    public float shootRange;
    void powerShoot()
    {
        if (dist <= shootRange)
        {
            pos = transform.position;
            transform.LookAt(target.position);
            if (canShoot)
            {
                StartCoroutine(powerShootDelay());
            }
        }
        else
        {
            pos = target.position;
        }
    }
    IEnumerator powerShootDelay()
    {
        GameObject projectile = Instantiate(associatedObj, firePos.position, firePos.rotation);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 2000);
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
