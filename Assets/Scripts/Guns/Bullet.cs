using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private GameObject hit_effect;
    public float damage;

    [SerializeField]
    private bool isEnemy;

	void Start () {
        Destroy(gameObject, 3);
	}

    int collider_fix = 0;
    void OnTriggerEnter(Collider col) {
        if (collider_fix == 0) {
            collider_fix++;
            if (!isEnemy && col.tag == "Enemy") {
                col.GetComponent<EnemyController>().TakeDamage(damage);
            } else if (col.tag == "DeathStar" && transform.name == "enemy") {
                Deathstar.instance.TakeDamage(damage);
            }
            GameObject hit = Instantiate(hit_effect, transform.position, Quaternion.identity);
            Destroy(hit, 0.5f);
        }
        Destroy(gameObject, 1f);
    }
}
