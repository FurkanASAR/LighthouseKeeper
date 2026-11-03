using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform PlayerBody;
    [SerializeField] private float sensitivity = 100f;

    private bool lockMouseOn = false;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            lockMouseOn = !lockMouseOn;
        }


        if (lockMouseOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //PlayerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
