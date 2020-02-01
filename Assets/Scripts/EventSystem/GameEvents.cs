public class GameEvents {
    public delegate void OnEvent(EVENT_TYPE type, System.Object data);
    public static event OnEvent OnEventAction;
    public static void RaiseGameEvent(EVENT_TYPE type, System.Object data = null) {
        if (OnEventAction != null) {
            OnEventAction(type, data);
        }
    }
}

public enum EVENT_TYPE {
    GAME_START,
    SPAWN_ENEMY,
    GAME_OVER
}
