using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public GameObject main_cam;
    public GameObject sub_cam;
    public GameObject subCameraPos;

    public float lookAtSpeed = 2.0f;
    public float moveSpeed = 1.5f;

    [SerializeField] private bool isSubCam = false;
    [SerializeField] private bool isButtonReady = true;

    void Start()
    {
        isButtonReady = true;
        sub_cam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CameraChange") && isButtonReady)
        {
            StopAllCoroutines();
            StartCoroutine(CameraChange());
            isSubCam = !isSubCam;
        }

        if(isSubCam && isButtonReady)
        {
            sub_cam.transform.position = subCameraPos.transform.position;
        }
    }

    IEnumerator CameraChange()
    {
        main_cam.SetActive(false);
        sub_cam.SetActive(true);

        isButtonReady = false;

        if (isSubCam)
        {
            sub_cam.transform.position = subCameraPos.transform.position;

            while ((Vector3.Distance(sub_cam.transform.position, main_cam.transform.position) > 0.1f) || Quaternion.Angle(sub_cam.transform.rotation, main_cam.transform.rotation) > 0.3f)
            {
                yield return null;
                //print("changing position");

                sub_cam.transform.position = Vector3.Lerp(sub_cam.transform.position, main_cam.transform.position, moveSpeed * Time.deltaTime);
                sub_cam.transform.rotation = Quaternion.Slerp(sub_cam.transform.rotation, main_cam.transform.rotation, lookAtSpeed * Time.deltaTime);
            }

            sub_cam.transform.position = main_cam.transform.position;
            sub_cam.transform.rotation = main_cam.transform.rotation;

            main_cam.SetActive(true);
            sub_cam.SetActive(false);
        }
        else if (!isSubCam)
        {

            sub_cam.transform.position = main_cam.transform.position;

            while ((Vector3.Distance(sub_cam.transform.position, subCameraPos.transform.position) > 0.02f) || Quaternion.Angle(sub_cam.transform.rotation, subCameraPos.transform.rotation) > 0.1f)
            {
                yield return null;
                //print("changing position");

                sub_cam.transform.position = Vector3.Lerp(sub_cam.transform.position, subCameraPos.transform.position, moveSpeed * Time.deltaTime);
                sub_cam.transform.rotation = Quaternion.Slerp(sub_cam.transform.rotation, subCameraPos.transform.rotation, lookAtSpeed * Time.deltaTime);
            }

            sub_cam.transform.position = subCameraPos.transform.position;
            sub_cam.transform.rotation = subCameraPos.transform.rotation;
        }

        isButtonReady = true;
    }
}
