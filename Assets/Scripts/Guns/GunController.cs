using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour, IGun {

    [SerializeField]
    private GameObject flashOnShoot;

    private GunSO _gun;
    private new Transform camera;
    private bool isOnCoolDown;

	void Start () {
        camera = Camera.main.transform;
	}

    public void Shoot() {
        if (isOnCoolDown) return;
        //var effect = Instantiate(shootEffect, transform.position, Quaternion.identity) as GameObject;
        //effect.transform.parent = transform;
        //Destroy(effect, .1f);

        var bullet = Instantiate(_gun.bullet_prefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().damage = _gun.damage;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 80, ForceMode.Impulse);

        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit)) {
            bullet.transform.LookAt(hit.point);
        }

        isOnCoolDown = true;
        StartCoroutine(Cooldown());
    }
	
	private IEnumerator Cooldown() {
        yield return new WaitForSeconds(_gun.firerate);
        isOnCoolDown = false;
    }

    public void setGun(GunSO gunSO) {
        _gun = gunSO;
    }
}
