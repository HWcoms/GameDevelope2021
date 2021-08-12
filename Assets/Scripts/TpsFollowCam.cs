using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    public bool isCursorLock = false;

    public float MouseXSensitivity = 6f;
    public float MouseYSensitivity = 6f;

    public Transform target;
    private float lookAtOffset = -.75f;  //offset for LookAt height
    private Vector3 TempTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if (isCursorLock)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCursorLock)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        TempTarget = target.position;
        TempTarget -= transform.up * lookAtOffset;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        offset = Quaternion.AngleAxis(mouseX * MouseXSensitivity, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(mouseY * MouseYSensitivity, Vector3.left) * offset;
        transform.LookAt(TempTarget);
    }
}