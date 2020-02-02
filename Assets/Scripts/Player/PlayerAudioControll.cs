using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControll : MonoBehaviour {
    public AudioSource engine_start, engine_running, engine_boost;

    private bool has_played_engine_start;
    private float boost_cooldown, engine_running_cooldown, engine_start_cooldown;

    private void Start() {
        has_played_engine_start = false;
    }

    private void Update() {
        if (engine_running_cooldown > 0) {
            engine_running_cooldown -= Time.deltaTime;
        }
        if (boost_cooldown > 0) {
            boost_cooldown -= Time.deltaTime;
        }
        if (engine_start_cooldown > 0) {
            engine_start_cooldown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) && !engine_running.isPlaying) {
            engine_start_cooldown = engine_start.clip.length;
            engine_start.Play();
            engine_running.Play();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && boost_cooldown <=0) {
            boost_cooldown = engine_boost.clip.length;
            engine_boost.Play();
        }
    }
}
