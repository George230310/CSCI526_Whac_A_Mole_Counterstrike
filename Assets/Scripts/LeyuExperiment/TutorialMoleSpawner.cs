using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialMolePrefabs;

    public GameObject myMole;

    public void SpawnTutorialMole(int idx)
    {
        myMole = Instantiate(tutorialMolePrefabs[idx], transform.position, transform.rotation);
        myMole.GetComponent<SimpleMole>().isTutorialMole = true;
        myMole.GetComponent<SimpleMole>().myTutorialSpawner = this;
    }
}
