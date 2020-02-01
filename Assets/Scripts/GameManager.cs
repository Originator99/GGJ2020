using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start() {
        InvokeRepeating("SpawnRandomEnemies", 1f, 300f);
    }

    private void SpawnRandomEnemies() {
        GameEvents.RaiseGameEvent(EVENT_TYPE.SPAWN_ENEMY, 0);
    }

    public void dropItem(List<Item> items, Vector3 position) {
        if (items != null) {
            int index = Random.Range(0, items.Count);
            Instantiate(items[index].prefab, position, Quaternion.identity);
            InventoryManager.instance.modifyInventory(items[index]);
        }
    }
}
