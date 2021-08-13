using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    public bool useRotation = true;
    public bool isCursorLock = false;

    public float MouseXSensitivity = 6f;
    public float MouseYSensitivity = 6f;
    public bool isInvertedY = false;

    protected Transform _pivot;
    protected Vector3 _LocalRotation;

    public Transform target;
    private float lookAtOffset = -1.25f;  //offset for LookAt height
    private Vector3 TempTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    private Vector3 startedPos;     //position of camera at start game


    // Start is called before the first frame update
    void Start()
    {
        startedPos = transform.localPosition;
        this._pivot = this.transform.parent;

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

        TempTarget = target.position;
        TempTarget -= Vector3.up * lookAtOffset;

        Vector3 smoothedPosition = Vector3.Lerp(_pivot.transform.position, TempTarget, smoothSpeed);

        _pivot.transform.position = smoothedPosition;
    }
    
    void LateUpdate()
    {
        //mouse rotation
        int invMouseY;

        if (isInvertedY) invMouseY = -1;
        else invMouseY = 1;

        if (useRotation)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y") * invMouseY;

            _LocalRotation.x += mouseX * MouseXSensitivity;
            _LocalRotation.y -= mouseY * MouseYSensitivity;

            //Clamp the Y rotation
            _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, -95f, 62f);


            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            _pivot.rotation = Quaternion.Lerp(_pivot.rotation, QT, /*Time.deltaTime **/ smoothSpeed);
            
           
        }

    }
    
    void Update()
    {
        //racast
        Debug.DrawLine(this.transform.position, TempTarget, Color.cyan);

        transform.localPosition = Vector3.Lerp(transform.localPosition, startedPos, smoothSpeed);

        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(TempTarget, this.transform.position, out hit) && hit.transform.tag != "Player")
        {
            Debug.DrawRay(hit.point, Vector3.left, Color.red);
            
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(hit.point.x, hit.point.y, hit.point.z),
                1f);
            //transform.position = hit.point;
        }
        else
        {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, startedPos, 1f);
            
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, TempTarget);
    }*/
}