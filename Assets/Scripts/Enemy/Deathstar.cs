using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstar : MonoBehaviour
{
    public Transform planets;

    public static Deathstar instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }


    private float repair_amount;
    private float charge_amount;

    private bool can_use_death_star;

    public bool PAUSE_RECHARGE_REPAIR;

    private void Start() {
        repair_amount = 50;
        PAUSE_RECHARGE_REPAIR = false;
    }

    private void Update() {
        if (can_use_death_star && Input.GetKeyDown(KeyCode.Space))
            UseDeathStar();
    }

    public void resetRepair() {
        repair_amount = 0;
    }
    public void resetCharge() {
        charge_amount = 0;
    }

    public void repairOrRecharge(RepairChickenSO rc_info) {
        if (repair_amount < 100) {
            repairDeathStar(rc_info.repair_per_second);
        } else if (repair_amount >= 100) {
            //Debug.Log("Repair Done");
            if (charge_amount < 100) {
                rechargeDeathStar(rc_info.recharge_per_second);
            } else {
                //Debug.Log("Charge done");
                if (!can_use_death_star) {
                    can_use_death_star = true;
                }
            }
        }
       // Debug.Log("Repair Amount : " + repair_amount + " Charge Amount : " + charge_amount);
    }

    public void UseDeathStar() {
        Debug.Log("Death star out !");
        can_use_death_star = false;
        StartCoroutine(DeathStarAnimation());
    }

    private IEnumerator DeathStarAnimation() {
        Transform target = findClosetPlanet();
        resetCharge();
        yield break;
    }


    public void TakeDamage(float damage) {
        float amount = (float)damage / 100 * (100 - repair_amount);
        repair_amount -= amount;
        if (repair_amount <= 0) {
            //Debug.Log("DEATH STAR DEAD!!");
            PAUSE_RECHARGE_REPAIR = true;
        }
    }

    private void repairDeathStar(float amount) {
        if (!PAUSE_RECHARGE_REPAIR) {
            repair_amount += amount;
            if (repair_amount >= 100) {
                repair_amount = 100;
            }
        }
    }

    private void rechargeDeathStar(float amount) {
        if (!PAUSE_RECHARGE_REPAIR) {
            charge_amount += amount;
            if (charge_amount >= 100) {
                charge_amount = 100;
            }
        }
    }

    private Transform findClosetPlanet() {
        Transform planet_to_destroy = null;
        int closest_dist = 100000;
        foreach (Transform child in planets) {
            if (Vector3.Distance(planet_to_destroy.position, transform.position) < closest_dist) {
                planet_to_destroy = child;
            }
        }
        return planet_to_destroy;
    }
}
