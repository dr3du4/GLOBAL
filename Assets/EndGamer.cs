using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamer : MonoBehaviour
{
    public void EndGame() {
        SceneManager.LoadScene("end_video");
    }
}
