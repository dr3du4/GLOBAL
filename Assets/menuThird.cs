using UnityEngine;
using UnityEngine.SceneManagement;

public class menuThird : menuSecond
{
    public void StartGame()
    {
        Debug.Log("Quitsadasdasdasdasd Game");
        SceneManager.LoadScene(sceneName);
    }
}
