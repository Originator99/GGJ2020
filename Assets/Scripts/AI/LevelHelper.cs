using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHelper {

    private static Vector3[] repair_req = {
        new Vector3(10,10,10),
        new Vector3(40,40,40)
    };

    private static int[] friendly_guns = { 0 };
    public static int getGunIDRandom() {
        return friendly_guns[Random.Range(0, friendly_guns.Length)];
    }

    public static Vector3 getRepairRequirements(float repair_amount) {
        int req = 0;
        if (repair_amount >25 &&  repair_amount <= 29) {
            req = 0;
        }else if (repair_amount > 35 && repair_amount <= 39) {
            req = 1;
        } else if (repair_amount > 45 && repair_amount <= 49) {
            req = 1;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 1;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 1;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 1;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 1;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 1;
        }
        return repair_req[req];
    }

    public static int getMinEnemySpawn(float repair_amount) {
        int req = 4;
        if (repair_amount > 25 && repair_amount <= 29) {
            req = 4;
        } else if (repair_amount > 35 && repair_amount <= 39) {
            req = 5;
        } else if (repair_amount > 45 && repair_amount <= 49) {
            req = 6;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 7;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 7;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 8;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 9;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 10;
        }
        return req;
    }

    public static int getMaxEnemySpawn(float repair_amount) {
        int req = 10;
        if (repair_amount > 25 && repair_amount <= 29) {
            req = 10;
        } else if (repair_amount > 35 && repair_amount <= 39) {
            req = 11;
        } else if (repair_amount > 45 && repair_amount <= 49) {
            req = 15;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 18;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 20;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 22;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 24;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 26;
        }
        return req;
    }
}
