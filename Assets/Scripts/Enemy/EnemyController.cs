﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public Image radar_icon;

    public EnemySO enemy_info;
    public GunController[] shoot_points;
    public Transform flash;
    
    private GunSO current_gun;
    private List<IGun> guns;
    private Transform target_to_hit;
    private AudioSource flash_sound;

    private float current_health;
    private bool canShoot, canMove;
    private float spaceJumpSpeed, distanceTopStop;

    private void Start() {
        Radar.RegisterRadarObjects(this.gameObject, radar_icon);

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
        current_health = enemy_info.health;
        target_to_hit = GameObject.FindGameObjectWithTag("DeathStar").transform;
        GameEvents.OnEventAction += HandleEnemyEvents;
        flash_sound = flash.GetComponent<AudioSource>();
    }

    private void HandleEnemyEvents(EVENT_TYPE type, System.Object data = null) { 
        if(type == EVENT_TYPE.GAME_OVER) {
            Destroy(gameObject);
        }
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
                b.Shoot(true);
            }
        }
    }

    private void FixedUpdate() {
        if (canShoot && (Vector3.Distance(target_to_hit.position, transform.position) > 450)) {
            TakeDamage(5000);
        }
    }

    public void Spawn(Vector3 spawn_point) {
        canShoot = false;
        transform.position = spawn_point;
        transform.LookAt(target_to_hit);
        canMove = true;
        StartCoroutine(SpaceJump());
    }

    public void TakeDamage(float damage) {
        current_health -= damage;
        if (current_health <= 0) {
            Radar.RemoveRadarObject(this.gameObject);
            if (enemy_info.die_effect != null) {
                Instantiate(enemy_info.die_effect, transform.position, Quaternion.identity);
            }
            GameManager.instance.dropItem(enemy_info.drop, transform.position);
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        GameEvents.OnEventAction -= HandleEnemyEvents;
        GameManager.instance.decreaseEnemyCount();
    }

    private IEnumerator SpaceJump() {
        float random_wait_time = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(random_wait_time);
        flash.gameObject.SetActive(true);
        spaceJumpSpeed = 1000f;
        distanceTopStop = Random.Range(280, 320);
        float time_taken = Vector3.Distance(target_to_hit.position, transform.position) / spaceJumpSpeed;
        if (flash_sound != null) {
            flash_sound.Play();
        }
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
