using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairChickenData", menuName = "New Repair Chicken", order = 1)]

public class RepairChickenSO : ScriptableObject {
    public int _id;
    public string _name;
    public float repair_capacity;
    public float repair_per_second;
    public float recharge_per_second;
    public GameObject prefab;
}
