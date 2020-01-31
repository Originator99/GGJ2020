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
            // apply damage to enemy
            //col.GetComponent<BotAttackController>().ApplyDamage(damage);
        }
        Instantiate(hit_effect, transform.position, Quaternion.identity);
        Destroy(gameObject,1f);
    }
}
