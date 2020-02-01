using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;


    private float spawnCooldown;
    private int MAX_SPAWN_COOLDOWN;
    public int current_enemy_count;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start() {
        MAX_SPAWN_COOLDOWN = 300;
        Invoke("SpawnRandomEnemies", 2f);
    }

    private void Update() {
        if (spawnCooldown > 0) {
            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown <= 0) {
                SpawnRandomEnemies();
            }
        }
    }

    private void SpawnRandomEnemies() {
        StartCoroutine(countDownSpawn(2f));
    }

    private IEnumerator countDownSpawn(float seconds) {
        spawnCooldown = MAX_SPAWN_COOLDOWN;
        yield return new WaitForSeconds(seconds);
        GameEvents.RaiseGameEvent(EVENT_TYPE.SPAWN_ENEMY, 0);
    }

    public void dropItem(List<Item> items, Vector3 position) {
        if (items != null) {
            int index = Random.Range(0, items.Count);
            Instantiate(items[index].prefab, position, Quaternion.identity);
            InventoryManager.instance.modifyInventory(items[index]);
        }
    }
    public void decreaseEnemyCount() {
        current_enemy_count -= 1;
        Debug.Log("remaining enemies : " + current_enemy_count);
        if (current_enemy_count <= 0) {
            SpawnRandomEnemies();
        }
    }
}
