using UnityEngine;

public class Camara : MonoBehaviour
{
    public float sensityivity = 1f;
    public CharacterController playerCharacterController;
    float cameraVerticalRotation = 0f;
    bool lockedCursor = true;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacterController = GetComponentInParent<CharacterController>();
        HideCursor();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        cameraVerticalRotation -= mouseY * sensityivity * 100 * Time.deltaTime;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        playerCharacterController.transform.Rotate(Vector3.up * mouseX);
    }

    public void HideCursor()
    {
        lockedCursor = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowCursor()
    {
        lockedCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
