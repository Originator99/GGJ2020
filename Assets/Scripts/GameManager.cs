using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private bool game_over;
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
        GameEvents.OnEventAction += HandleGameManagerEvents;
        game_over = false;
        MAX_SPAWN_COOLDOWN = 300;
    }

    private void OnDestroy() {
        GameEvents.OnEventAction -= HandleGameManagerEvents;
    }

    private void Update() {
        if (!game_over) {
            if (spawnCooldown > 0) {
                spawnCooldown -= Time.deltaTime;
                if (spawnCooldown <= 0) {
                    SpawnRandomEnemies();
                }
            }
        }
    }

    private void HandleGameManagerEvents(EVENT_TYPE type, System.Object data = null) {
        if (type == EVENT_TYPE.GAME_START) {
            startGame();
        }
        if (type == EVENT_TYPE.GAME_OVER) {
            game_over = true;
            UIManager.instance.showGameOverPanel();
        }
    }

    private void startGame() {
        game_over = false;
        UIManager.instance.startUIGame();
        current_enemy_count = 0;
        Deathstar.instance.resetCharge();
        Deathstar.instance.resetRepair();
        Invoke("SpawnRandomEnemies", 2f);
    }

    private void SpawnRandomEnemies() {
        if(!game_over)
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
            if(instance!=null)
                SpawnRandomEnemies();
        }
    }
}
