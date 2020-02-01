using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHelper {

    private static Vector3[] repair_req = {
        //x = metal, y = stone, z = energy
        new Vector3(10,10,10),
        new Vector3(40,40,40),
        new Vector3(60,60,60),
    };

    private static int[] friendly_guns = { 0 };

    public static int WAVE_NUMBER;
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
            req = 2;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 1;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 2;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 1;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 1;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 2;
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
            req = 5;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 6;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 6;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 6;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 7;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 7;
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
            req = 12;
        } else if (repair_amount > 55 && repair_amount <= 59) {
            req = 13;
        } else if (repair_amount > 65 && repair_amount <= 69) {
            req = 14;
        } else if (repair_amount > 75 && repair_amount <= 79) {
            req = 15;
        } else if (repair_amount > 85 && repair_amount <= 89) {
            req = 18;
        } else if (repair_amount > 95 && repair_amount <= 99) {
            req = 20;
        }
        return req;
    }

    public static bool canSpawnRepairCircle(float repair_amount, out List<Item> required_items) {
        required_items = new List<Item>();
        Vector3 amount = getRepairRequirements(repair_amount);
        Item total_metal = InventoryManager.instance.getInventoryItem(ItemType.METAL);
        Item total_energy = InventoryManager.instance.getInventoryItem(ItemType.ENERGY);
        Item total_stone = InventoryManager.instance.getInventoryItem(ItemType.STONE);
        int count = 0;
        if (amount.x <= total_metal.amount) { 
            count++;
            Item temp = total_metal;
            temp.amount = (int)amount.x;
            required_items.Add(temp);
        }
        if (amount.y <= total_stone.amount) { 
            count++;
            Item temp = total_stone;
            temp.amount = (int)amount.x;
            required_items.Add(temp);
        }
        if (amount.z <= total_energy.amount) { 
            count++;
            Item temp = total_energy;
            temp.amount = (int)amount.x;
            required_items.Add(temp);
        }
        if (count == 3)
            return true;
        else
            return false;
    }
}
