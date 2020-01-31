using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanet : MonoBehaviour
{
    float health = 100;

    public GameObject sphere;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Deathstar.OnAttackTarget += OnAttackTarget;
    }

    private void OnAttackTarget()
    {
        health -= 100;
        if (health<=0)
        {
            sphere.SetActive(false);
            explosionPrefab.SetActive(true);
            Debug.Log("Planet Detroyed!!");
        }
    }
}
