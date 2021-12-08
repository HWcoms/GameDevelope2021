using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoTarget : MonoBehaviour
{
    Transform target;

    AutoTargetCam autoTargetCamScript;
    [SerializeField] private bool isTargetAllowed = true;
    [SerializeField] private bool isTarGettingOn;

    bool isLookAtTarget = false;
    public float lookAtSpeed = 5.0f;

    private UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl CACscript;


    [SerializeField] List<Transform> detectedTargets = new List<Transform>();

    public float detectRadius = 7.0f;
    Vector3 playerCenter;

    public LayerMask layerMask;

    [SerializeField] int targetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        autoTargetCamScript = Camera.main.GetComponent<AutoTargetCam>();

        CACscript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl>();

        targetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerCenter = transform.position + new Vector3(0, 1.0f, 0);

        if (!isTargetAllowed)
        {
            isTarGettingOn = false;
            return;
        }

        isTarGettingOn = autoTargetCamScript.getIsTargeting();

        if (!isTarGettingOn) return;

        detectTargets();

        //target = Camera.main.GetComponent<AutoTargetCam>().monster.transform;

        //StartCoroutine(DetectionCoroutine(1.0f));
       
        if (target != null)
        {
            if (isLookAtTarget)
                TargetAttack();
        }
        //print(Input.GetAxis("Mouse ScrollWheel"));

        //scroll target
        if (Input.GetAxis("Mouse ScrollWheel") > 0.05f)
        {
            targetIndex++;

            if (targetIndex > detectedTargets.Count - 1)
                targetIndex = 0;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.05f) // backwards
        {
            targetIndex--;

            if (targetIndex < 0)
                targetIndex = detectedTargets.Count - 1;
        }
    }

    private void LateUpdate()
    {
        
    }

    public void TargetAttack()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 dir = target.position - this.transform.position;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), lookAtSpeed * Time.deltaTime);

        if (Quaternion.Angle(Quaternion.LookRotation(dir), transform.rotation) < 15.0f)
            isLookAtTarget = false;
    }

    void setIsLookAtTarget(int flag)
    {
        bool fl = (flag == 0 ? false : true);

        if (fl)
            isLookAtTarget = true;
        else
            isLookAtTarget = false;
    }

    void setTarget(GameObject targetObj)
    {
        if (targetObj == null)
            target = null;
        else
            target = targetObj.transform;

        Camera.main.GetComponent<AutoTargetCam>().monster = targetObj;
    }

    void detectTargets()
    {
        Collider[] objs = Physics.OverlapSphere(playerCenter + Vector3.up, detectRadius, layerMask);

        if (objs.Length == 0)
        {
            clearTargets();
            autoTargetCamScript.setIsTargeting(false);
            setTarget(null);
            return;
        }

        if (objs.Length == detectedTargets.Count)
        {
            //print("same");
            setTarget(detectedTargets[targetIndex].gameObject);
            return;
        }

        clearTargets();

        foreach (Collider col in objs)
        {
            detectedTargets.Add(col.transform);
        }

        if (detectedTargets.Count > 0)
        {
            if (targetIndex > detectedTargets.Count - 1)
                targetIndex = detectedTargets.Count - 1;

            setTarget(detectedTargets[targetIndex].gameObject);
        }
    }

    void clearTargets()
    {
        detectedTargets.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCenter, detectRadius);
    }

    IEnumerator DetectionCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        detectTargets();
    }
}
