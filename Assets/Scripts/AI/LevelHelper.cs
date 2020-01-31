using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHelper {
    public static int getGunIDRandom() {
        return Random.Range(0, GunPool.instance.guns.Count);
    }
}
