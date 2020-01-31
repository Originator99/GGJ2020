using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "New Gun", order = 1)]
public class GunSO : ScriptableObject {
    public int _id;
    public string gun_name;
    public float damage;
    public GameObject bullet_prefab;
    public float firerate;
}
