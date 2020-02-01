using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    public Text energy, metal, stone;
    public StartPanel startPanel;
    public GameOverPanel gameOverPanel;

    public static UIManager instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    private void Start() {
        showStartPanel();
    }

    public void showStartPanel() {
        startPanel.renderStarPanel();
        gameOverPanel.gameObject.SetActive(false);

    }

    public void showGameOverPanel() {
        startPanel.gameObject.SetActive(false);
        gameOverPanel.renderGameOverPanel();
    }

    public void startUIGame() {
        startPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
    }

    public void updateRepairUI(float repair_amount) { 
    
    }
    public void updateChargeUI(float charge_amount) { 
    
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
