using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public AudioSource first_message, bg_music, enemy_spawn_sound;

    private bool game_over;
    private float spawnCooldown;
    private int MAX_SPAWN_COOLDOWN;
    public int current_enemy_count;

    private bool repair_circle_spawned;
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
                if (spawnCooldown <= 0 && !repair_circle_spawned) {
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
        if (type == EVENT_TYPE.REPAIR_CIRCLE_SPAWNED) {
            repair_circle_spawned = true;
        }
        if (type == EVENT_TYPE.REPAIR_COMPLETED) {
            repair_circle_spawned = false;
            spawnCooldown = MAX_SPAWN_COOLDOWN;
            SpawnRandomEnemies();
        }
    }

    private void startGame() {
        game_over = false;
        UIManager.instance.startUIGame();
        current_enemy_count = 0;
        Deathstar.instance.resetCharge();
        Deathstar.instance.resetRepair();
        LevelHelper.WAVE_NUMBER = 0;
        if (!bg_music.isPlaying) { 
            bg_music.Play();
        }
        Invoke("SpawnRandomEnemies", 2f);
    }

    private void SpawnRandomEnemies() {
        if(!game_over)
            StartCoroutine(countDownSpawn(2f));
    }

    private IEnumerator countDownSpawn(float seconds) {
        LevelHelper.WAVE_NUMBER += 1;
        spawnCooldown = MAX_SPAWN_COOLDOWN;
        yield return new WaitForSeconds(seconds);
        GameEvents.RaiseGameEvent(EVENT_TYPE.SPAWN_ENEMY, 0);
        enemy_spawn_sound.Play();
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
            if (instance != null && !game_over) {
                List<Item> items = new List<Item>();
                if (LevelHelper.canSpawnRepairCircle(Deathstar.instance.repair_amount, out items)) {
                    Deathstar.instance.spawnRequirementCircle(items);
                } else { 
                    SpawnRandomEnemies();
                }
            }
        }
        UIManager.instance.updateEnemyCount(current_enemy_count);
    }
    public IEnumerator doSomethingAfterDelay(float delay, System.Action callback) {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
