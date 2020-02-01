using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHelper {

    private static int[] friendly_guns = { 0 };
    public static int getGunIDRandom() {
        return friendly_guns[Random.Range(0, friendly_guns.Length)];
    }
}
