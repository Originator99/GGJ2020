using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairChicken : MonoBehaviour {
    public RepairChickenSO rc_info;

    public void StartRepairing(RepairChickenSO rc_info) {
        this.rc_info = rc_info;
        InvokeRepeating("CallRepairRecharge", 0f, 1f);
    }

    private void CallRepairRecharge() {
        Deathstar.instance.repairOrRecharge(rc_info);
    }
}
