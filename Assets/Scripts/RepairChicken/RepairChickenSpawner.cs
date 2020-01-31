using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairChickenSpawner : MonoBehaviour
{

    public GameObject repairChickenPrefab;
    public GameObject deathStar;
    
    public List<RepairChicken> chickens = new List<RepairChicken>();

    public int chickensToSpawn; 

    private void Start()
    {
        for (int i = 0; i < chickensToSpawn; i++)
        {
            chickens.Add(repairChickenPrefab.GetComponent<RepairChicken>());
        }
        foreach (RepairChicken chicken in chickens)
        {
            Run(chicken);
        }
    }

    public void Run(RepairChicken chicken)
    {
        Vector3 spawnPosition = Random.onUnitSphere * ((deathStar.transform.localScale.x / 2) + chicken.transform.localScale.y * 0.5f) + deathStar.transform.position;
        Quaternion spawnRotation = Quaternion.identity;
        GameObject newCharacter = Instantiate(chicken.gameObject, spawnPosition, spawnRotation) as GameObject;
        newCharacter.transform.LookAt(deathStar.transform);
        newCharacter.transform.Rotate(-90, 0, 0);
    }
}
