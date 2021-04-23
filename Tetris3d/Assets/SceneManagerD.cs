using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerD : MonoBehaviour
{
    // Start is called before the first frame update

    public void GameQuit()
    {
        Application.Quit();
    }
    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void CreditScene()
    {
        SceneManager.LoadScene(2);
    }
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
