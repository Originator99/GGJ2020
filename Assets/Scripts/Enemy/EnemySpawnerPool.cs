using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPool : MonoBehaviour {
    public List<GameObject> enemy_prefabs;
    public List<Transform> spawn_points;

    public void spawnEnemies() {

        int final_count = Random.Range(LevelHelper.getMinEnemySpawn(Deathstar.instance.repair_amount), LevelHelper.getMaxEnemySpawn(Deathstar.instance.repair_amount));
        GameManager.instance.current_enemy_count += final_count;
        UIManager.instance.updateEnemyCount(GameManager.instance.current_enemy_count);
        int spawn_point_index = Random.Range(0, spawn_points.Count);
        if (LevelHelper.WAVE_NUMBER == 1)
            spawn_point_index = 0;
        for (int i = 0;i< final_count; i++) {
            GameObject enemy = Instantiate(enemy_prefabs[Random.Range(0, enemy_prefabs.Count)]);
            Transform spawn_point = spawn_points[spawn_point_index];
            enemy.transform.SetParent(spawn_point);
            enemy.GetComponent<EnemyController>().Spawn(spawn_point.position);
        }
    }
}
