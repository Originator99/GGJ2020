﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstar : MonoBehaviour
{
    public Transform planets;
    public Transform repairChickens;
    public Transform requirement_circle_prefab;

    public GameObject deathstar0;
    public GameObject deathstar20;
    public GameObject deathstar40;
    public GameObject deathstar60;
    public GameObject deathstar80;
    public GameObject deathstar100;

    public static Deathstar instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }


    public float repair_amount;
    public float charge_amount;

    private bool can_use_death_star;

    public bool PAUSE_RECHARGE_REPAIR;

    private void Start() {
        GameEvents.OnEventAction += HandleDeathStarEvents;
        Invoke("spawnRequirementCircle", 2f);
    }

    private void OnDestroy() {
        GameEvents.OnEventAction += HandleDeathStarEvents;

    }

    private void HandleDeathStarEvents(EVENT_TYPE type, System.Object data = null) { 
        if(type == EVENT_TYPE.GAME_START) {
            foreach (Transform planet in planets)
                planet.gameObject.SetActive(true); //enabling after destroying
            PAUSE_RECHARGE_REPAIR = false;
        }else if(type == EVENT_TYPE.GAME_OVER) {
            PAUSE_RECHARGE_REPAIR = true;
        }
        if (type == EVENT_TYPE.REPAIR_CIRCLE_SPAWNED) {
            PAUSE_RECHARGE_REPAIR = true;
        } else if (type == EVENT_TYPE.REPAIR_COMPLETED) {
            PAUSE_RECHARGE_REPAIR = false;
            rechargeDeathStar(Random.Range(1, 5));
            repairDeathStar(Random.Range(10,15));
        }
    }

    private void Update() {
        if (can_use_death_star && Input.GetKeyDown(KeyCode.KeypadEnter)) { 
            UseDeathStar();
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            repair_amount = 100;
            charge_amount = 100;
            UIManager.instance.updateRepairUI(repair_amount);
            UIManager.instance.updateChargeUI(charge_amount);
            PAUSE_RECHARGE_REPAIR = true;
        }
    }

    public void resetRepair() {
        repair_amount = 40;
    }
    public void resetCharge() {
        charge_amount = 0;
    }

    public void repairOrRecharge(RepairChickenSO rc_info)
    {
        if (repair_amount < 100)
        {
            repairDeathStar(rc_info.repair_per_second);
        }
        else if (repair_amount >= 100)
        {
            //Debug.Log("Repair Done");
            if (charge_amount < 100)
            {
                rechargeDeathStar(rc_info.recharge_per_second);
            }
            else
            {
                //Debug.Log("Charge done");
                if (!can_use_death_star)
                {
                    if (GameManager.instance.current_enemy_count == 0) { 
                        UIManager.instance.showUseDeathStar(true);
                        can_use_death_star = true;
                    }
                }
            }
        }

        UpdateDeathStarState();
        Debug.Log("Repair Amount : " + repair_amount + " Charge Amount : " + charge_amount);
    }

    private void UpdateDeathStarState()
    {
        if (repair_amount >= 100)
        {
            deathstar100.SetActive(true);
            deathstar0.SetActive(false);
            deathstar20.SetActive(false);
            deathstar40.SetActive(false);
            deathstar60.SetActive(false);
            deathstar80.SetActive(false);
        }
        else if (repair_amount >= 80)
        {
            deathstar100.SetActive(false);
            deathstar0.SetActive(false);
            deathstar20.SetActive(false);
            deathstar40.SetActive(false);
            deathstar60.SetActive(false);
            deathstar80.SetActive(true);
        }
        else if (repair_amount >= 60)
        {
            deathstar100.SetActive(false);
            deathstar0.SetActive(false);
            deathstar20.SetActive(false);
            deathstar40.SetActive(false);
            deathstar60.SetActive(true);
            deathstar80.SetActive(false);
        }
        else if (repair_amount >= 40)
        {
            deathstar100.SetActive(false);
            deathstar0.SetActive(false);
            deathstar20.SetActive(false);
            deathstar40.SetActive(true);
            deathstar60.SetActive(false);
            deathstar80.SetActive(false);
        }
        else if (repair_amount > 20)
        {
            deathstar100.SetActive(false);
            deathstar0.SetActive(false);
            deathstar20.SetActive(true);
            deathstar40.SetActive(false);
            deathstar60.SetActive(false);
            deathstar80.SetActive(false);
        }
        else
        {
            deathstar100.SetActive(false);
            deathstar0.SetActive(true);
            deathstar20.SetActive(false);
            deathstar40.SetActive(false);
            deathstar60.SetActive(false);
            deathstar80.SetActive(false);
        }
    }

    public void UseDeathStar() {
        Debug.Log("Death star out !");
        can_use_death_star = false;
        UIManager.instance.playerDeathstarKillVideo(delegate () {
            StartCoroutine(DeathStarAnimation());
        });
    }

    private IEnumerator DeathStarAnimation() {
        Transform target = findClosetPlanet();
        if (target == null) {
            GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_OVER);
        } else {
            target.gameObject.SetActive(false);
            resetCharge();
        }
        yield break;
    }


    public void TakeDamage(float damage) {
        float amount = (float)damage / 100 * (100 - repair_amount);
        repair_amount -= amount;
        if (repair_amount <= 0) {
            GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_OVER);
        }
    }

    public void spawnRequirementCircle(List<Item> required_items) {
        Debug.Log("REPAIR THIS NOW!!!");
        int randomIndex = Random.Range(0, repairChickens.childCount);
        Transform temp = Instantiate(requirement_circle_prefab);
        temp.GetComponent<RequirementCircle>().DoRenderer(Random.Range(50f, 70f));
        temp.GetComponent<RequirementCircle>().renderUI(required_items);
        temp.position = repairChickens.GetChild(randomIndex).position + repairChickens.GetChild(randomIndex).position * 0.07f;
        temp.LookAt(transform);
        temp.Rotate(new Vector3(temp.rotation.x - 90, temp.rotation.y, temp.rotation.z));

        GameEvents.RaiseGameEvent(EVENT_TYPE.REPAIR_CIRCLE_SPAWNED, temp);
    }

    private void repairDeathStar(float amount) {
        if (!PAUSE_RECHARGE_REPAIR) {
            repair_amount += amount;
            if (repair_amount >= 100) {
                repair_amount = 100;
            }
            UIManager.instance.updateRepairUI(repair_amount);
        }
    }

    private void rechargeDeathStar(float amount) {
        if (!PAUSE_RECHARGE_REPAIR) {
            charge_amount += amount;
            if (charge_amount >= 100) {
                charge_amount = 100;
            }
            UIManager.instance.updateChargeUI(charge_amount);
        }
    }

    private Transform findClosetPlanet() {
        Transform planet_to_destroy = null;
        int closest_dist = 100000;
        foreach (Transform child in planets) {
            if (Vector3.Distance(child.position, transform.position) < closest_dist && child.name.ToLower().Contains("sphere")) {
                planet_to_destroy = child;
            }
        }
        return planet_to_destroy;
    }
}
