using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ToAIMatch()
    {
        SceneManager.LoadScene("AIMatch");
    }
    public void ToHome()
    {
        SceneManager.LoadScene("GameStart");
    }
}
