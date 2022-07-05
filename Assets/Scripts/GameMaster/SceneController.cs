using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ToAIMatch()
    {
        SceneManager.LoadScene("SingleGameScene");
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
