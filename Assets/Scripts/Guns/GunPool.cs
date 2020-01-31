using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPool : MonoBehaviour {
    public static GunPool instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public List<GunSO> guns;

    public GunSO getGunSO(int id) { 
        foreach(var gun in guns) {
            if (gun._id == id)
                return gun;
        }
        return null;
    }
}
