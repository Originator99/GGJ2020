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

    void OnTriggerEnter(Collider col) {
        if (!isEnemy && col.tag == "Enemy") {
            col.GetComponent<EnemyController>().TakeDamage(damage);
        }
        GameObject hit = Instantiate(hit_effect, transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);
        Destroy(gameObject,1f);
    }
}
