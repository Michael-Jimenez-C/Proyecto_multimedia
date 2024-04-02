using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyAssigner : MonoBehaviour
{
    KeyBindings keyBindings;
    Text buttonText;
    bool waitingForKey;
    Transform actions;
    Event keyEvent;
    KeyCode newKey;
    void Start()
    {
        actions = transform.GetChild(0).Find("Actions");
        waitingForKey = false;
        keyBindings = GameObject.Find("GameManager").GetComponent<KeyBindings>();

        for(int i = 0;i<actions.childCount;i++){
            if (actions.GetChild(i).name == "setForwardButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.forward.ToString();
            }
            else if (actions.GetChild(i).name == "setBackwardButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.backward.ToString();
            }
            else if (actions.GetChild(i).name == "setRightButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.right.ToString();
            }
            else if (actions.GetChild(i).name == "setLeftButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.left.ToString();
            }
            else if (actions.GetChild(i).name == "setRunButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.run.ToString();
            }
            else if (actions.GetChild(i).name == "setJumpButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.jump.ToString();
            }
            else if (actions.GetChild(i).name == "setUseButton"){
                actions.GetChild(i).GetComponentInChildren<Text>().text = keyBindings.use.ToString();
            }
        }

    }

    void OnGUI(){
        keyEvent = Event.current;
        if(keyEvent.isKey && waitingForKey){
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssigment(string keyName){
        if(!waitingForKey){
            StartCoroutine(AssignKey(keyName));
        }
    }
    public void SendText(Text text){
        buttonText = text;
    }

    IEnumerator WaitForKey(){
        while(!keyEvent.isKey){
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName){
        waitingForKey = true;
        yield return WaitForKey();
        switch(keyName)
        {
            case "forward":
                keyBindings.forward = newKey;
                buttonText.text = keyBindings.forward.ToString();
                PlayerPrefs.SetString("forwardKey", keyBindings.forward.ToString());
                break;
            case "backward":
                keyBindings.backward = newKey;
                buttonText.text = keyBindings.backward.ToString();
                PlayerPrefs.SetString("backwardKey", keyBindings.backward.ToString());
                break;
            case "right":
                keyBindings.right = newKey;
                buttonText.text = keyBindings.right.ToString();
                PlayerPrefs.SetString("rightKey", keyBindings.right.ToString());
                break;
            case "left":
                keyBindings.left = newKey;
                buttonText.text = keyBindings.left.ToString();
                PlayerPrefs.SetString("leftKey", keyBindings.left.ToString());
                break;
            case "jump":
                keyBindings.jump = newKey;
                buttonText.text = keyBindings.jump.ToString();
                PlayerPrefs.SetString("jumpKey", keyBindings.jump.ToString());
                break;
            case "run":
                keyBindings.run = newKey;
                buttonText.text = keyBindings.run.ToString();
                PlayerPrefs.SetString("runKey", keyBindings.run.ToString());
                break;
            case "use":
                keyBindings.use = newKey;
                buttonText.text = keyBindings.use.ToString();
                PlayerPrefs.SetString("useKey", keyBindings.use.ToString());
                break;
        }
        yield return null;
    }
}
