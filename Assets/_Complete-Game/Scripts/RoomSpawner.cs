using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    // 1 = bottom door
    // 2 = top door
    // 3 = left door
    // 4 = right door
    private int rand;
    private RoomTemplates templates;
    private bool spawned = false;

    public float waitTime = 4f;

    private void Awake()
    {

    }


    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //top
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            if (openingDirection == 3)
            {
                //Left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //Right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }

            spawned = true;
        }





    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
           
            spawned = true;

        }

    }
}
