using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public Button restart_button;

    private void Start() {
        restart_button.onClick.RemoveAllListeners();
        restart_button.onClick.AddListener(delegate() {
            GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_START);
        });
    }

    public void renderGameOverPanel() {
        gameObject.SetActive(true);
    }
}
