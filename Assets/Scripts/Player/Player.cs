using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GunController[] gun_controllers;
    public GunSO current_gun;

    public float restocking_time = 2f;
    private float restocking_cooldown;

    private List<IGun> guns;
    private bool can_controll_player;

    private bool checkForRepairing;
    private Transform repair_circle;
    private float radius_of_repair_circle;

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
        if (type == EVENT_TYPE.REPAIR_CIRCLE_SPAWNED) {
            checkForRepairing = true;
            repair_circle = data as Transform;
            radius_of_repair_circle = repair_circle.GetComponent<RequirementCircle>().radius;
            restocking_cooldown = restocking_time;
        }
        if (type == EVENT_TYPE.REPAIR_COMPLETED) {
            checkForRepairing = false;
        }
    }

    private void Update() {
        if (guns == null)
            return;
        if (Input.GetMouseButtonDown(0) && can_controll_player) {
            foreach (IGun gun in guns)
                gun.Shoot();
        }
        if (checkForRepairing && repair_circle != null) {
            if (Vector3.Distance(repair_circle.position, transform.position) < radius_of_repair_circle) {
                restocking_cooldown -= Time.deltaTime;
                if (restocking_cooldown <= 0) {
                    checkForRepairing = false;
                    Debug.Log("Repair Complete!");
                    GameEvents.RaiseGameEvent(EVENT_TYPE.REPAIR_COMPLETED, repair_circle.GetComponent<RequirementCircle>().required_items);
                    Destroy(repair_circle.gameObject);
                }
            } else {
                restocking_cooldown = restocking_time;
            }
        }
    }

    private void getAndSetGun(int id) { 
        foreach(IGun gun in guns) {
            gun.setGun(GunPool.instance.getGunSO(id));
        }
    }
}
