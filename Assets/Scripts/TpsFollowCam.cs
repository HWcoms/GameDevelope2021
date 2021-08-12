using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    public Transform target;
    public float lookAtOffset;  //offset for LookAt height

    public Vector3 TempTarget;


    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        TempTarget = target.position;
        TempTarget -= transform.up * lookAtOffset;

        transform.LookAt(TempTarget);
    }
}