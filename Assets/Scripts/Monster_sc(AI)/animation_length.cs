﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_length : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public float attackTime_R;
    public float attackTime_L;
    public float damageTime;
    public float deathTime;
    public float idleTime;

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
                case "Attack_Right":
                    attackTime_R = clip.length;
                    break;
                case "Attack_Left":
                    attackTime_L = clip.length;
                    break;
                case "Idle":
                    idleTime = clip.length;
                    break;
            }
        }
    }
}