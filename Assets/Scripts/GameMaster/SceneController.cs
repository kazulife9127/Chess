using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ToAIMatch()
    {
        SceneManager.LoadScene("AIMatchScene");
    }
    public void ToHome()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void ToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
