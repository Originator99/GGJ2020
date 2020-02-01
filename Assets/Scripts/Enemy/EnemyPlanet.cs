using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanet : MonoBehaviour {
    public EnemySpawnerPool enemy_spawn;
    public PlanetSO planet_info;

    private float current_health;

    private void Start() {
        current_health = planet_info.health;
        GameEvents.OnEventAction += HandleEnemySpawnEvents;
    }

    private void HandleEnemySpawnEvents(EVENT_TYPE type, System.Object data = null) {
        if (type == EVENT_TYPE.SPAWN_ENEMY) {
            if ((int)data == planet_info._id) {
                enemy_spawn.spawnEnemies(planet_info.max_enemy_spawn_count);
            }
        }
    }
}
