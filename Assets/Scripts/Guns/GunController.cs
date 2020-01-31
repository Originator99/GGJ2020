using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour, IGun {
    public Transform shoot_point;
    public GunSO _gun;

    private float fireCooldown;

    private void Update() {
        if (fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
        }
    }

    public void Shoot() {
        if (_gun == null || shoot_point == null) {
            Debug.LogError("Shoot point or _gun is null, cannot shoot for object name : " + transform.name);
            return;
        }
        if (fireCooldown <= 0) {
            fireCooldown = _gun.firerate;
            GameObject bullet = Instantiate(_gun.bullet_prefab, shoot_point.position, Quaternion.identity);
            //bullet.transform.SetParent(transform.parent.parent);

        }
    }

    public void setGun(GunSO gunSO) {
        _gun = gunSO;
    }
}
