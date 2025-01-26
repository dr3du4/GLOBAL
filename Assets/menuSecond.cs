using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSecond : MonoBehaviour
{
    public GameObject settingsPanel; // Panel ustawień
    public GameObject mainMenuPanel; // Panel głównego menu
    public string sceneName;

    void Start()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        // Zamknij grę (nie działa w edytorze Unity)
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        // Pokaż panel ustawień i ukryj menu główne
        if (settingsPanel != null && mainMenuPanel != null)
        {
            settingsPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        // Ukryj panel ustawień i pokaż menu główne
        if (settingsPanel != null && mainMenuPanel != null)
        {
            settingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
}
