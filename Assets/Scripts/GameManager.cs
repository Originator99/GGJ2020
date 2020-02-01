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
}
