using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform PlayerBody;
    [SerializeField] private float sensitivity = 100f;

    private float cameraPitch = 0f;
    private float playerYaw = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        

        cameraPitch -= mouseY;
        playerYaw += mouseX;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

        PlayerBody.Rotate(0f, mouseX, 0f);

        //PlayerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
