using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item {
    public ItemType item_type;
    public int amount;
    public Sprite Icon;
    public GameObject prefab;
}

public enum ItemType { 
    METAL,
    STONE,
    ENERGY
}

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    public List<Item> inventory;

    private void Start() {
        Debug.Log("Initializing Inventory : ");
        
        int metal = PlayerPrefs.GetInt("METAL", 0);
        int stone = PlayerPrefs.GetInt("STONE", 0);
        int energy = PlayerPrefs.GetInt("ENERGY", 0);

        if (inventory != null) {
            foreach (var item in inventory) {
                if (item.item_type == ItemType.METAL)
                    item.amount = metal;
                else if (item.item_type == ItemType.STONE)
                    item.amount = stone;
                else if (item.item_type == ItemType.ENERGY)
                    item.amount = energy;
            }
        }
        UpdateUI();
        GameEvents.OnEventAction += HandleInventoryEvents;
    }

    private void OnDestroy() {
        GameEvents.OnEventAction -= HandleInventoryEvents;
    }

    private void HandleInventoryEvents(EVENT_TYPE type, System.Object data = null) {
        if (type == EVENT_TYPE.REPAIR_COMPLETED && data != null) {
            modifyInventory(data as List<Item>,true);
        }
    }

    private void OnApplicationQuit() {
        SaveInventory();
    }

    public void modifyInventory(List<Item> items, bool remove = false) {
        for (int i = 0; i < items.Count; i++) {
            for (int j = 0; j < inventory.Count; j++) {
                if (items[i].item_type == inventory[j].item_type) {
                    if(remove)
                        inventory[j].amount -= items[i].amount;
                    else
                        inventory[j].amount += items[i].amount;
                }
            }
        }
        UpdateUI();
    }
    public void modifyInventory(Item item, bool remove = false) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].item_type == item.item_type) {
                if(remove)
                    inventory[i].amount -= item.amount;
                else
                    inventory[i].amount += item.amount;
            }
        }
        UpdateUI();
    }

    public Item getInventoryItem(ItemType itemType) {
        foreach (Item item in inventory) {
            if (item.item_type == itemType)
                return item;
        }
        return null;
    }

    public bool hasEnough(Item item) {
        if (getInventoryItem(item.item_type).amount >= item.amount)
            return true;
        return false;
    }

    private void UpdateUI() {
        //foreach (var item in inventory) {
        //    Debug.Log("Itm : " + item.item_type + " amount : " + item.amount);
        //}
        UIManager.instance.UpdateItemCount(inventory);
    }

    private void SaveInventory() {
        foreach (var item in inventory) {
            if (item.item_type == ItemType.ENERGY) {
                PlayerPrefs.SetInt("ENERGY", item.amount);
            } else if (item.item_type == ItemType.METAL) {
                PlayerPrefs.SetInt("METAL", item.amount);
            } else if (item.item_type == ItemType.STONE) {
                PlayerPrefs.SetInt("STONE", item.amount);
            }
        }
    }

}
