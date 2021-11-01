using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    [SerializeField] private bool useRotation = true;
    [SerializeField] private bool isCursorLock = true;

    [SerializeField] private float MouseXSensitivity = 6f;
    [SerializeField] private float MouseYSensitivity = 6f;
    [SerializeField] private bool isInvertedY = false;

    [SerializeField] private float CameraDistance = 10f;
    [SerializeField] private float maxDistance = 10f;
    private float userSetDistance;
    [SerializeField] private float ScrollSensitivity = 2f;
    [SerializeField] private float ScrollDampening = 6f;
    
    private Transform _pivot;
    private Vector3 _LocalRotation;

    [SerializeField] private Transform target;
    private float lookAtOffset = -1.25f;  //offset for LookAt height
    private Vector3 TempTarget;

    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector3 startedPos;     //position of camera at start game

    public LayerMask ignoreCollide;

    // Start is called before the first frame update
    void Start()
    {
        startedPos = transform.localPosition;
        userSetDistance = CameraDistance;
        this._pivot = this.transform.parent;

        target = GameObject.FindGameObjectWithTag("Player").transform;

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

            if (Input.GetKey(KeyCode.X))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
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
        }

        if (Input.GetKey(KeyCode.X))
            useRotation = false;
        else
            useRotation = true;


        //zoom
        float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
        
        if(ScrollAmount != 0f)
        {   
            //make camera zoom faster the further away it is from the target
            ScrollAmount *= (CameraDistance * 0.3f);

            CameraDistance += ScrollAmount * -1f;

            //zoom clamp 1.5 meters ~ 100 meters from target
            CameraDistance = Mathf.Clamp(CameraDistance, 1.5f, maxDistance);

            userSetDistance = CameraDistance;
        }

        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _pivot.rotation = Quaternion.Lerp(_pivot.rotation, QT, /*Time.deltaTime **/ smoothSpeed);

        if (transform.localPosition.z != CameraDistance * -1f)
        {
            transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
    
    void Update()
    {
        //racast
        Debug.DrawLine(this.transform.position, TempTarget, Color.cyan);

        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(TempTarget, this.transform.position, out hit, ignoreCollide) /*(hit.transform.tag != "Player" || hit.transform.tag != "Monster")*/)
        {
            //print(hit.transform.gameObject.layer.ToString());
            Debug.DrawRay(hit.point, Vector3.left, Color.red);

            float distance = hit.distance;

            CameraDistance = Mathf.Lerp(CameraDistance, distance, ScrollDampening * 10f);
        }
        else
        {
            CameraDistance = Mathf.Lerp(CameraDistance, userSetDistance, Time.deltaTime * ScrollDampening);
        }
    }

}