using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public void Show() {
        gameObject.SetActive(true);
    }
    
    public void Hide() {
        gameObject.SetActive(false);
    }
}
