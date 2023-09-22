using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] molePrefabs;
    
    [SerializeField] private float randomLifeTimeMin;
    [SerializeField] private float randomLifeTimeMax;

    [SerializeField] private float randomSpawnWaitTimeMin;
    [SerializeField] private float randomSpawnWaitTimeMax;

    private bool _isSpawning;
    public GameObject myMole;
    
    // Start is called before the first frame update
    void Start()
    {
        float waitTime = Random.Range(randomSpawnWaitTimeMin, randomSpawnWaitTimeMax);
        float lifeSpan = Random.Range(randomLifeTimeMin, randomLifeTimeMax);
        
        _isSpawning = true;

        StartCoroutine(SpawnRandomMoleType(waitTime, lifeSpan));
    }

    // Update is called once per frame
    void Update()
    {
        if (!myMole && !_isSpawning)
        {
            _isSpawning = true;
            float waitTime = Random.Range(randomSpawnWaitTimeMin, randomSpawnWaitTimeMax);
            float lifeSpan = Random.Range(randomLifeTimeMin, randomLifeTimeMax);
            
            StartCoroutine(SpawnRandomMoleType(waitTime, lifeSpan));
        }
    }

    private IEnumerator SpawnRandomMoleType(float spawnWaitTime, float moleLifeSpan)
    {
        GameObject prefabToSpawn = molePrefabs[Random.Range(0, molePrefabs.Length)];
        yield return new WaitForSeconds(spawnWaitTime);
        myMole = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        
        // set life span of mole
        SimpleMole simpleMole = myMole.GetComponent<SimpleMole>();
        simpleMole.myLifeSpan = moleLifeSpan;
        simpleMole.mySpawner = this;
        
        _isSpawning = false;
    }
}
