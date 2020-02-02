using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public class UIManager : MonoBehaviour {

    public Text energy, metal, stone, enemies_left;
    public Image repair_fill, charge_fill;
    public Transform HUD, use_death_star_msg;
    public StartPanel startPanel;
    public GameOverPanel gameOverPanel;

    public Transform deathStarVideoRawImage;
    public VideoPlayer videoPlayer, intro_player, spaceJump_videoplayer;

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
        HUD.gameObject.SetActive(false);
        startPanel.renderStarPanel();
        gameOverPanel.gameObject.SetActive(false);

    }

    public void showGameOverPanel() {
        use_death_star_msg.gameObject.SetActive(false);
        HUD.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(false);
        gameOverPanel.renderGameOverPanel();
    }

    public void startUIGame() {
        use_death_star_msg.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        repair_fill.fillAmount = 0;
        charge_fill.fillAmount = 0;
        updateEnemyCount(0);
        HUD.gameObject.SetActive(true);
    }

    public void playerDeathstarKillVideo(System.Action after_video) {
        deathStarVideoRawImage.gameObject.SetActive(true);
        videoPlayer.Play();
        StartCoroutine(GameManager.instance.doSomethingAfterDelay(23f, delegate () {
            deathStarVideoRawImage.gameObject.SetActive(false);
            after_video?.Invoke();
            showUseDeathStar(false);
        }));
    }

    public void playIntroVideo(System.Action after_video) {
        deathStarVideoRawImage.gameObject.SetActive(true);
        intro_player.Play();
        StartCoroutine(GameManager.instance.doSomethingAfterDelay(38f, delegate () {
            playIntroPart2(after_video);
        }));
    }

    public void playIntroPart2(System.Action after_videoIntro) {
        deathStarVideoRawImage.gameObject.SetActive(true);
        spaceJump_videoplayer.Play();
        StartCoroutine(GameManager.instance.doSomethingAfterDelay(10f, delegate () {
            deathStarVideoRawImage.gameObject.SetActive(false);
            after_videoIntro?.Invoke();
        }));
    }

    public void showUseDeathStar(bool state) { 
        use_death_star_msg.gameObject.SetActive(state);
    }

    public void updateRepairUI(float repair_amount) {
        repair_fill.DOFillAmount(repair_amount / 100f, 0.35f);
    }
    public void updateChargeUI(float charge_amount) {
        charge_fill.DOFillAmount(charge_amount / 100f, 0.35f);
    }

    public void updateEnemyCount(int current_enemy_count) {
        enemies_left.text = current_enemy_count + "<size=30>x</size>";
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
