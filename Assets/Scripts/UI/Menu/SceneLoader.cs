using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;


public class SceneLoader : MonoBehaviour
{

   public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
