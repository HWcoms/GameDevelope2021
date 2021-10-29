using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_length : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public float ShortAttack;
    public float LongAttack;
    public float Block;
    

    private Animator anim;
    private AnimationClip clip;

    public AnimationClip[] clips;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Error: Did not find anim!");
        }
        else
        {
            //Debug.Log("Got anim");
        }

        UpdateAnimClipTimes();
    }
    public void UpdateAnimClipTimes()
    {
        clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "S_Attack":
                    ShortAttack = clip.length;
                    break;
                case "L_Attack":
                    LongAttack = clip.length;
                    break;
                case "Blcok":
                    Block = clip.length;
                    break;

            }
        }
    }
}
