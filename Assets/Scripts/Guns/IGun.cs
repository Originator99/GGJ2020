using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun  {
    void Shoot(bool is_enemy = false);

    void setGun(GunSO gunSO);
}
