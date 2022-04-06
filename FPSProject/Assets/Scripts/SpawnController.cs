using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    //Controls the spawning of enemies


    public float triggerDelay;
    public float spawnRate;
    public GameObject mob;
    public Transform mobPositions;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "Player") {

            InvokeRepeating("spawn", triggerDelay, spawnRate);
            Destroy(gameObject, 16f);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void spawn() {
        Vector3 randomPos = new Vector3(mobPositions.position.x + Random.Range(-10f, 10f), mobPositions.position.y, mobPositions.position.z + Random.Range(-10f, 10f));
        Instantiate(mob, randomPos, mobPositions.rotation, mobPositions);
    }
}
