using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SK_length : MonoBehaviour
{
    public float Born1;
    public float Skill1;
    public float Skill2;
    public float Skill3;
    public float Skill4;
    public float Skill5;
    public float Run;
    public float Dead;

    private Animator anim;
    private AnimationClip clip;

    public AnimationClip[] clips;
    // Start is called before the first frame update
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

    // Update is called once per frame
    public void UpdateAnimClipTimes()
    {
        clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Born1":
                    Born1 = clip.length;
                    break;
                case "Skill1":
                    Skill1 = clip.length;
                    break;
                case "Skill2":
                    Skill2 = clip.length;
                    break;
                case "Skill3":
                    Skill3 = clip.length;
                    break;
                case "Skill4":
                    Skill4 = clip.length;
                    break;
                case "Skill5":
                    Skill5 = clip.length;
                    break;
                case "Run":
                    Run = clip.length;
                    break;
                case "Dead":
                    Dead = clip.length;
                    break;

            }
        }
    }
}
