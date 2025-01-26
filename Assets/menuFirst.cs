using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFirst : MonoBehaviour
{
    public GameObject settingsPanel; // Panel ustawień
    public GameObject mainMenuPanel; // Panel głównego menu
    public string sceneName;

    void Start()
    {
        // Upewnij się, że panel głównego menu jest aktywny na początku
        mainMenuPanel.SetActive(true);

        // Ukryj panel ustawień
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Wczytaj scenę gry (zmień "GameScene" na nazwę swojej sceny)
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {   Debug.Log("Quit");
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
