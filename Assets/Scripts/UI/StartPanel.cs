using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : MonoBehaviour {
    public Button start_button;

    private void Start() {
        start_button.onClick.RemoveAllListeners();
        start_button.onClick.AddListener(delegate() {
            GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_START);
        });
    }

    public void renderStarPanel() {
        gameObject.SetActive(true);
    }
}
