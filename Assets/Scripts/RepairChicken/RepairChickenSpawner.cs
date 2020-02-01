using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairChickenSpawner : MonoBehaviour
{
    
    public List<RepairChickenSO> chickens = new List<RepairChickenSO>();

    public int chickensToSpawn;
    private Transform deathStar;

    private void Start() {
        deathStar = GameObject.FindGameObjectWithTag("DeathStar").transform;
        for (int i = 0; i < 10; i++) {
            Run(chickens[0]);
        }
    }

    public void Run(RepairChickenSO chicken)
    {
        GameObject newCharacter = Instantiate(chicken.prefab) as GameObject;
        newCharacter.transform.position = Random.onUnitSphere * ((deathStar.transform.localScale.x / 2) + newCharacter.transform.localScale.y * 0.5f) + deathStar.transform.position;
        newCharacter.transform.LookAt(deathStar.transform);
        newCharacter.transform.Rotate(-90, 0, 0);
        newCharacter.transform.SetParent(transform);
        newCharacter.GetComponent<RepairChicken>().StartRepairing(chicken);
    }
}
