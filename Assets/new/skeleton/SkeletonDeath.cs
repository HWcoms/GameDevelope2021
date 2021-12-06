using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeath : MonoBehaviour
{
    Animator anim;

    bool isOnce = false;
    [SerializeField] GameObject skeleton_bone_root;
    public List<Transform> skeleton_bones = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //skeleton_bone_root = this.transform.Find("Bip01").gameObject;

        foreach(Transform bones in skeleton_bones)
        {
            bones.GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<EnemyHealth>().getDead())
            StartCoroutine(DestroyEffect(5.0f));
    }

    IEnumerator DestroyEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if(!isOnce)
        {
            isOnce = true;

            GetComponent<RetargetPose>().getOriginalBones();
        }

        
    }

    void getSkeletonBones()
    {
        int children = skeleton_bone_root.transform.childCount;

        Transform[] allChildren = skeleton_bone_root.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren){
            if (null == child)
                continue;

            skeleton_bones.Add(child);
        }
    }

    public GameObject getRoot()
    {
        return skeleton_bone_root;
    }

}
