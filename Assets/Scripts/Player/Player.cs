using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GunController[] gun_controllers;
    public GunSO current_gun;

    private List<IGun> guns;
    private bool can_controll_player;

    private void Start() {
        guns = new List<IGun>();
        foreach(var gun in gun_controllers) {
            guns.Add(gun.GetComponent<IGun>());
        }
        int gun_id = LevelHelper.getGunIDRandom(); 
        getAndSetGun(gun_id);

        GameEvents.OnEventAction += HandlePlayerEvents;
    }

    private void HandlePlayerEvents(EVENT_TYPE type, System.Object data = null) { 
        if(type== EVENT_TYPE.GAME_START) {
            can_controll_player = true;
        }
        if(type == EVENT_TYPE.GAME_OVER) {
            can_controll_player = false;
        }
    }

    private void Update() {
        if (guns == null)
            return;
        if (Input.GetMouseButtonDown(0) && can_controll_player) {
            foreach (IGun gun in guns)
                gun.Shoot();
        }
    }

    private void getAndSetGun(int id) { 
        foreach(IGun gun in guns) {
            gun.setGun(GunPool.instance.getGunSO(id));
        }
    }
}
