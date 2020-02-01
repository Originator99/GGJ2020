using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPool : MonoBehaviour {
    public List<GameObject> enemy_prefabs;
    public List<Transform> spawn_points;

    public void spawnEnemies(int max_count) {
        int final_count = Random.Range(4, max_count);
        GameManager.instance.current_enemy_count += final_count;
        for (int i = 0;i< final_count; i++) {
            //float offset_x = Random.Range(spawn_point.position.x, spawn_point.position.x + 10f);
            //float offset_y = Random.Range(spawn_point.position.y + 10f, spawn_point.position.x - 10f);
            //float offset_z = Random.Range(spawn_point.position.z, spawn_point.position.x + 10f);
            GameObject enemy = Instantiate(enemy_prefabs[Random.Range(0, enemy_prefabs.Count)]);
            Transform spawn_point = spawn_points[Random.Range(0, spawn_points.Count)];
            enemy.transform.SetParent(spawn_point);
            enemy.GetComponent<EnemyController>().Spawn(spawn_point.position);
        }
    }
}
