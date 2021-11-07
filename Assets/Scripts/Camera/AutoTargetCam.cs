using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTargetCam : MonoBehaviour
{
    [SerializeField] private bool isTargeting = false;

    [SerializeField] private bool isCamFollowTarget = false;
    [SerializeField] private float smoothLookSpeed = 5.0f;

    private GameObject monster;
    private GameObject player;

    //public GameObject targetingPrefab;
    [SerializeField] private bool isEffectOn = true;
    public Vector3 targetEffectPosOffset;
    public GameObject targetEffect;

    public Vector3 destDir; //Destination Direction

    /*
    public Vector3 currentDir;   //Cam to Player Direction;
    public float rotAngleY;
    public float rotAngleZ;
    */

    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Boss");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(2))
        {
            setIsTargeting(!isTargeting);
        }

        if(isTargeting && isCamFollowTarget)
        {
            //currentDir = transform.position - player.transform.position;    //player to cam
            destDir = monster.transform.position - player.transform.position;   //monster to player

            //currentDir.Normalize();
            destDir.Normalize();

            /*
            //get angle between current CamDir, Desired Dir
            rotAngleY = Vector3.Angle(new Vector3(0, currentDir.y, 0), new Vector3(0, destDir.y, 0));
            rotAngleZ = Vector3.Angle(new Vector3(0, 0, currentDir.z), new Vector3(0, 0, destDir.z));
            */

            Transform pivot;
            pivot = this.transform.parent;

            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.LookRotation(destDir), smoothLookSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        //Targeting Visual Effect
        Vector3 targetEffectPos = monster.transform.position + targetEffectPosOffset;

        if (isTargeting && isEffectOn)
        {
            //currentTargetPrefab = Instantiate(targetingPrefab, targetEffectPos, Quaternion.identity);
            targetEffect.SetActive(true);
            targetEffect.transform.position = targetEffectPos;
        }
        else {
            /*
            if (targetEffect != null)
                Destroy(targetEffect);
            */
            targetEffect.SetActive(false);
        }
    }

    public bool getIsTargeting()
    {
        return isTargeting;
    }

    public void setIsTargeting(bool flag)
    {
        isTargeting = flag;
    }

    public bool getIsCamFollowTarget()
    {
        return isCamFollowTarget;
    }

    public void setIsEffect(bool flag)
    {
        isEffectOn = flag;
    }
}
