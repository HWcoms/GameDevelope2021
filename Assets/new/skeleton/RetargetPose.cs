using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetargetPose : MonoBehaviour
{
    public List<Transform> originalBones;
    SkeletonDeath skeletonDeathScript;

    public List<Transform> copiedSkeletonTransform;

    bool isGetOG;
    [SerializeField] bool isOnce = false;

    [SerializeField] GameObject newParent;
    GameObject viewParent;
    bool isCreateParent = false;

    //public GameObject viewPrefab;

    //[SerializeField] bool isAnimator;

    public float explosion_radius = 3.0f;
    public float explosion_power = 300.0f;

    public GameObject exp_prefab;

    // Start is called before the first frame update
    void Start()
    {
        skeletonDeathScript = GetComponent<SkeletonDeath>();

        isGetOG = false;
        isOnce = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGetOG && originalBones != null)
        {
            //retarget transforms
            if(!isOnce)
            {
                isOnce = true;
                RetargetCurrentPose();
            }
        }

        //isAnimator = this.GetComponent<Animator>().enabled;
    }

    public void getOriginalBones()
    {
        originalBones = skeletonDeathScript.skeleton_bones;
        
        isGetOG = true;
    }
    void RetargetCurrentPose()
    {
        if(!isCreateParent)
        {
            newParent = new GameObject("retargeted skeleton");
            newParent.transform.position = transform.position;
            newParent.transform.rotation = transform.rotation;

            viewParent = new GameObject("retargeted views");
            viewParent.transform.position = transform.position;
            viewParent.transform.rotation = transform.rotation;
            isCreateParent = true;
        }
        
        foreach (Transform child in originalBones)
        {
            if (null == child)
                continue;


            Transform temp = new GameObject().transform;
            temp.position = child.position;
            temp.rotation = child.rotation;
            temp.name = child.name;
            
            child.position = temp.position;
            child.rotation = temp.rotation;

            temp.SetParent(newParent.transform);

            copiedSkeletonTransform.Add(temp);
        }

        CopyPasteBonePos();

        InverseAnimator();

        ExplosionSimulate();

        DisablePhysics();
    }

    void InverseAnimator()
    {
        this.GetComponent<Animator>().enabled = !this.GetComponent<Animator>().enabled;
    }

    void CopyPasteBonePos()
    {
        for(int i = 0; i< originalBones.Count; i++)
        {
            Transform ogBone = originalBones[i];
            Transform targetBone = copiedSkeletonTransform[i].transform;

            ogBone.SetParent(skeletonDeathScript.getRoot().transform);

            ogBone.GetComponent<Rigidbody>().isKinematic = false;
            ogBone.GetComponent<Collider>().enabled = true;
        }
    }

    

    void ExplosionSimulate()
    {
        if(exp_prefab)
        {
            GameObject expObj = Instantiate(exp_prefab, skeletonDeathScript.getRoot().transform.position, Quaternion.identity);

            BombSim bombscript = expObj.GetComponent<BombSim>();
            bombscript.setRadius(explosion_radius);
            bombscript.setPower(explosion_power);
            bombscript.enabled = true;
        }
    }

    void DisablePhysics()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Collider>().enabled = true;
    }
}
