using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton GameManagerInstance;
    KeyBindings keyBindigs;
    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
            keyBindigs = GetComponent<KeyBindings>();
            DontDestroyOnLoad(gameObject);
            keyBindigs.forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
            keyBindigs.backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
            keyBindigs.left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
            keyBindigs.right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
            keyBindigs.jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
            keyBindigs.run = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("runKey", "LeftShift"));
            keyBindigs.use = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("useKey", "F"));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
