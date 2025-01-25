using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Obiekt, za którym podąża kamera
    public float followSpeed = 5f; // Prędkość podążania za obiektem
    public float rotationSpeed = 100f; // Prędkość obrotu kamery
    public float zoomSpeed = 2f; // Prędkość przybliżania/oddalania kamery
    public float minZoom = 5f; // Minimalna odległość od celu
    public float maxZoom = 20f; // Maksymalna odległość od celu
    public float verticalRotationLimit = 80f; // Maksymalny kąt obrotu góra/dół

    private float currentZoom = 10f; // Aktualna odległość kamery od celu
    private float currentRotationY = 0f; // Aktualny kąt obrotu kamery w osi Y (lewo/prawo)
    private float currentRotationX = 0f; // Aktualny kąt obrotu kamery w osi X (góra/dół)

    void Update()
    {
        // Obsługa przybliżania/oddalania kamery
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Obsługa obrotu kamery na klawiszach Q i E
        if (Input.GetKey(KeyCode.Q))
        {
            currentRotationY -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentRotationY += rotationSpeed * Time.deltaTime;
        }

        // Obsługa obrotu kamery myszką góra/dół
        if (Input.GetMouseButton(2)) // Lewy przycisk myszy
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            currentRotationY += mouseX;
            currentRotationX -= mouseY;

            // Ograniczenie kąta obrotu góra/dół
            currentRotationX = Mathf.Clamp(currentRotationX, -verticalRotationLimit, verticalRotationLimit);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Pozycjonowanie kamery za obiektem
        Vector3 direction = new Vector3(0, 0, -currentZoom);
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 position = target.position + rotation * direction;

        transform.position = position;
        transform.LookAt(target);
    }
    
    public void SetTarget(Transform newTarget)
    {
        this.target = newTarget;
    } 
}
