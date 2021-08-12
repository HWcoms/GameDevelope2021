using UnityEngine;

public class TpsFollowCam : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
    }
}