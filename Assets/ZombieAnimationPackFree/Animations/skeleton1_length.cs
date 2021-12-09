using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton1_length : MonoBehaviour
{
    public float Attack;
    public float Dead;
    public float Walk;
    public float Damage;
    public float Jump;


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
                case "Attack":
                    Attack = clip.length;
                    break;
                case "Dead":
                    Dead = clip.length;
                    break;
                case "Walk":
                    Walk = clip.length;
                    break;
                case "Damage":
                    Damage = clip.length;
                    break;
                case "JumpAttack":
                    Jump = clip.length;
                    break;


            }
        }
    }
}
