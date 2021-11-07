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

    // Start is called before the first frame update
    void Start()
    {
        autoTargetCamScript = Camera.main.GetComponent<AutoTargetCam>();
        

        CACscript = GetComponent<UnityStandardAssets.Characters.ThirdPerson.CharacterActionControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTargetAllowed)
        {
            isTarGettingOn = false;
            return;
        }

        isTarGettingOn = autoTargetCamScript.getIsTargeting();

        if (!isTarGettingOn) return;

        target = Camera.main.GetComponent<AutoTargetCam>().monster.transform;

        if (target != null)
        {
            if (isLookAtTarget)
                TargetAttack();
        }

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
}
