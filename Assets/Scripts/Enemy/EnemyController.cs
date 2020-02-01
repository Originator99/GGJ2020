using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public EnemySO enemy_info;
    public GunController[] shoot_points;
    public Transform flash;
    
    private GunSO current_gun;
    private List<IGun> guns;
    private Transform target_to_hit;

    private bool canShoot, canMove;
    private float spaceJumpSpeed, distanceTopStop;
    private Rigidbody rb;

    private void Start() {
        if(enemy_info == null) {
            Debug.LogError("Enemy data not assigned");
            return;
        }
        guns = new List<IGun>();
        foreach (var gun in shoot_points) {
            guns.Add(gun.GetComponent<IGun>());
        }
        int gun_id = enemy_info.gun_id;
        getAndSetGun(gun_id);
        rb = GetComponent<Rigidbody>();
        target_to_hit = GameObject.FindGameObjectWithTag("DeathStar").transform;
    }

    private void Update() {
        Quaternion rotation = Quaternion.LookRotation(target_to_hit.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);

        if (canMove) {
            Vector3 heading = target_to_hit.position - transform.position;
            Vector3 direction = heading / heading.magnitude;
            transform.position += direction * spaceJumpSpeed * Time.deltaTime;
        }

        if (Vector3.Distance(target_to_hit.position,transform.position) < distanceTopStop) {
            canShoot = true;
            canMove = false;
        }

        if (canShoot) {
            foreach (var b in guns) {
                b.Shoot();
            }
        }
    }

    public void Spawn(Vector3 spawn_point) {
        canShoot = false;
        transform.position = spawn_point;
        transform.LookAt(target_to_hit);
        canMove = true;
        StartCoroutine(SpaceJump());
    }

    private IEnumerator SpaceJump() {
        yield return new WaitForSeconds(0.5f);
        flash.gameObject.SetActive(true);
        spaceJumpSpeed = 1000f;
        distanceTopStop = Random.Range(280, 320);
        float time_taken = Vector3.Distance(target_to_hit.position, transform.position) / spaceJumpSpeed;
        yield return new WaitForSeconds(time_taken);
        flash.gameObject.SetActive(false);
        yield break;
    }

    private void getAndSetGun(int id) {
        foreach (IGun gun in guns) {
            gun.setGun(GunPool.instance.getGunSO(id));
        }
    }
}
