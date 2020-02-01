using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "New Enemy", order = 1)]
public class EnemySO : ScriptableObject {
    public int _id;
    public string _name;
    public float health;
    public int gun_id;
    public GameObject die_effect;
    public System.Collections.Generic.List<Item> drop;
}
