using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstar : MonoBehaviour
{

    public static Deathstar instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private float repair_amount;
    private float charge_amount;

    private bool can_use_death_star;

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
            Debug.Log("Repair Done");
            if (charge_amount < 100) {
                rechargeDeathStar(rc_info.recharge_per_second);
            } else {
                Debug.Log("Charge done");
                if (!can_use_death_star) {
                    can_use_death_star = true;
                }
            }
        }
    }

    public void UseDeathStar() {
        Debug.Log("Death star out !");
        can_use_death_star = false;
        resetCharge();
    }

    private void repairDeathStar(float amount) {
        repair_amount += amount;
        if (repair_amount >= 100) {
            repair_amount = 100;
        }
    }

    private void rechargeDeathStar(float amount) {
        charge_amount += amount;
        if(charge_amount >= 100) {
            charge_amount = 100;
        }
    }
}
