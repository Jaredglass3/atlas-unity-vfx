using UnityEngine;
using UnityEngine.VFX;

public class FireballController : MonoBehaviour
{
    public VisualEffect fireballVFX;
    public string positionProperty = "FireballPosition";
    public string velocityProperty = "FireballVelocity";
    
    private Vector3 previousMouseWorldPos;

    void Start()
    {
        // Initialize previousMouseWorldPos
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set this to the distance from the camera
        previousMouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    void Update()
    {
        // Get the current mouse position in screen space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set this to the distance from the camera
        Vector3 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the velocity
        Vector3 mouseVelocity = (currentMouseWorldPos - previousMouseWorldPos) / Time.deltaTime;

        // Set the position and velocity properties in the VFX Graph
        fireballVFX.SetVector3(positionProperty, currentMouseWorldPos);
        fireballVFX.SetVector3(velocityProperty, -mouseVelocity);

        // Update previousMouseWorldPos for the next frame
        previousMouseWorldPos = currentMouseWorldPos;
    }
}
