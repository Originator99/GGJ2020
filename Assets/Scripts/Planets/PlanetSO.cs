using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "New Planet", order = 3)]

public class PlanetSO : ScriptableObject {
    public int _id;
    public float health;
    public int max_enemy_spawn_count;
}
