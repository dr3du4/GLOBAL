using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Przypisz VideoPlayer w Inspectorze
    public string nextSceneName;   // Nazwa sceny, do której przejdziesz po filmie

    void Start()
    {
        // Subskrybuj event zakończenia filmu
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Przejdź do następnej sceny
        SceneManager.LoadScene(nextSceneName);
    }
}