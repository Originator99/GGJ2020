using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GunController[] gun_controllers;
    public GunSO current_gun;
    public GameObject dropItemBar;
    public Image fill_amount;

    public float restocking_time = 2f;
    private float restocking_cooldown;

    private List<IGun> guns;
    private bool can_controll_player;

    private bool isOutsideArea = true;
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
            dropItemBar.gameObject.SetActive(false);
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
                dropItemBar.SetActive(true);
                isOutsideArea = false;
                restocking_cooldown -= Time.deltaTime;
                fill_amount.fillAmount = restocking_cooldown / 2;
                if (restocking_cooldown <= 0) {
                    checkForRepairing = false;
                    Debug.Log("Repair Complete!");
                    fill_amount.fillAmount = 1;
                    dropItemBar.SetActive(false);
                    GameEvents.RaiseGameEvent(EVENT_TYPE.REPAIR_COMPLETED, repair_circle.GetComponent<RequirementCircle>().required_items);
                    Destroy(repair_circle.gameObject);
                }
            } else {
                restocking_cooldown = restocking_time;
                if(dropItemBar.activeSelf)
                    fill_amount.fillAmount = 1;
            }
        }
    }

    private void getAndSetGun(int id) { 
        foreach(IGun gun in guns) {
            gun.setGun(GunPool.instance.getGunSO(id));
        }
    }
}
