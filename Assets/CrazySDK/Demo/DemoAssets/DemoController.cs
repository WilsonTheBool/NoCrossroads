using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoController : MonoBehaviour
{
    public void ChangeDemoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainDemo()
    {
        SceneManager.LoadScene("MainDemo");
    }
}
