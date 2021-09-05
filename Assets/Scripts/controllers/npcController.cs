using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public Transform firePos;
    public GameObject[] possibleObjArr;
    public Mesh[] meshes;
    public Material[] materials;

    Vector3 pos;
    float distToPlayer;
    float distToPos;
    Transform target;
    string npcPower;
    float speed;
    float howclose;
    float health;
    bool shouldAttack;
    GameObject associatedObj;

    void Start()
    {
        selectCharacteristics();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("randomPos", 2f, 5f);
    }

    void Update()
    {
        distToPlayer = Vector3.Distance(target.position, transform.position);
        // if npc has reached its pos generate a new one
        distToPos = Vector3.Distance(pos, transform.position);
        if (distToPos < 5)
        {
            randomPos();
        }
        // destroy npc and drop loot
        if (health < 0)
        {
            Destroy(gameObject);
            dropItems(100);
        }
        if (distToPlayer <= howclose && shouldAttack)
        {
            threatNpcPowerManager();
        }
        else if (!shouldAttack)
        {
            harmlessNpcPowerManager();
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

    // randomly selects the npc's characteristics on initialization
    void selectCharacteristics()
    {
        // assigns random mesh to gameObjects meshFilter & meshCollider
        Mesh selectedMesh = meshes[Random.Range(0, meshes.Length)];
        GetComponent<MeshFilter>().mesh = selectedMesh;
        GetComponent<MeshCollider>().sharedMesh = selectedMesh;
        // assigns a random material to gameObjects Renderer
        gameObject.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
        associatedObj = possibleObjArr[Random.Range(0, possibleObjArr.Length)];
        float rand = Random.Range(0, 5);
        if (rand == 1)
        {
            npcPower = "shoot";
            shouldAttack = true;
            health = 100;
            speed = 10;
            howclose = 10;
        }
        else if (rand == 2)
        {
            npcPower = "collect";
            health = 100;
            speed = 10;
            howclose = 10;
        }
        else
        {
            npcPower = "shoot";
            health = 100;
            speed = 10;
            howclose = 10;
        }
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

    // calls the relevant power method if npc is a threat
    void threatNpcPowerManager()
    {
        switch (npcPower)
        {
            case "shoot":
                powerShoot();
                break;
        }
    }
    // calls the relevant power method if npc is harmless
    void harmlessNpcPowerManager()
    {
        switch (npcPower)
        {
            case "collect":
                powerCollect(transform.position, 10);
                break;
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
                distToPos = Vector3.Distance(pos, transform.position);
                if (distToPos < 3)
                {
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }

    /********** power: shoot ***********/

    bool canShoot = true;
    void powerShoot()
    {
        float shootDelay = Random.Range(1, 5);
        float shootPower = Random.Range(1000, 4000);
        float shootRange = 10;

        if (distToPlayer <= shootRange)
        {
            pos = transform.position;
            transform.LookAt(target.position);
            if (canShoot)
            {
                StartCoroutine(powerShootDelay(shootDelay, shootPower));
            }
        }
        else
        {
            pos = target.position;
        }
    }
    IEnumerator powerShootDelay(float shootDelay, float shootPower)
    {
        GameObject projectile = Instantiate(associatedObj, firePos.position, firePos.rotation);
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shootPower);
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
