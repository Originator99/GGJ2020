using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : MonoBehaviour {
    public Button start_button;
    public Button quit;

    private void Start() {
        start_button.onClick.RemoveAllListeners();
        start_button.onClick.AddListener(delegate () {
            if (GameManager.instance.play_intro) {
                UIManager.instance.playIntroVideo(delegate () {
                    GameEvents.RaiseGameEvent(EVENT_TYPE.GAME_START);
                });
            }
        });
        quit.onClick.RemoveAllListeners();
        quit.onClick.AddListener(delegate(){
            Application.Quit();
        });
    }

    public void renderStarPanel() {
        gameObject.SetActive(true);
    }
}
