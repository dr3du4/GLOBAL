using Player;
using UnityEngine;

public class ChamsterSteering : MonoBehaviour, SteeringScheme
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 direction) {
        throw new System.NotImplementedException();
    }

    public bool Interact() {
        Physics.CheckSphere(transform.position, 20, LayerMask.NameToLayer("Interactable"));
        return false;
    }

    public void Jump() {
        throw new System.NotImplementedException();
    }
}
