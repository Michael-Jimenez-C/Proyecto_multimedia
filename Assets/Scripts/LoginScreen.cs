using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginScreen : MonoBehaviour
{
    public TMP_InputField input;
    public TextMeshProUGUI error_message;
    public int nextScene;

    public void login_send(){
        if (input.text.Length>4){
            Debug.Log("asdasd");
            PlayerPrefs.SetString("PlayerName",input.text);
            SceneManager.LoadScene(nextScene,LoadSceneMode.Single);
        }
    }

    public void login_validation(){
        if (input. text.Length>4){
            error_message.text="";
        }else{
            error_message.text="El nombre debe tener por lo menos 5 caracteres";
        }
    }
}
