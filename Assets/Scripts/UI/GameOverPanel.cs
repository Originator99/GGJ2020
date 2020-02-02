using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public Button restart_button, quit;

    private void Start() {
        restart_button.onClick.RemoveAllListeners();
        restart_button.onClick.AddListener(delegate() {
            GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_START);
        });
        quit.onClick.RemoveAllListeners();
        quit.onClick.AddListener(delegate () {
            Application.Quit();
        });
    }

    public void renderGameOverPanel() {
        gameObject.SetActive(true);
    }
}
