using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    public Text energy, metal, stone;

    public static UIManager instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void UpdateItemCount(List<Item> items) {
        foreach (Item item in items) {
            if (item.item_type == ItemType.ENERGY) {
                energy.text = item.amount.ToString();
            } else if (item.item_type == ItemType.METAL) {
                metal.text = item.amount.ToString();
            } else if (item.item_type == ItemType.STONE) {
                stone.text = item.amount.ToString();
            }
        }
    }
}
