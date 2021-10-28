using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKnight_AnimCtrl_Script : MonoBehaviour
{
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Walk
        if (Input.GetKeyDown("2"))
        {
            if (myAnim.GetBool("isWalking"))
                myAnim.SetBool("isWalking", false);
            else
                myAnim.SetBool("isWalking", true);
        }

        //Run
        if (Input.GetKeyDown("3"))
        {
            if (myAnim.GetBool("isRunning"))
                myAnim.SetBool("isRunning", false);
            else
                myAnim.SetBool("isRunning", true);
        }

        //Short Attack
        if (Input.GetKeyDown("4"))
        {
            if (myAnim.GetBool("isShortAttacking"))
                myAnim.SetBool("isShortAttacking", false);
            else
                myAnim.SetBool("isShortAttacking", true);
        }
        if (Input.GetKeyUp("4"))
        {
            myAnim.SetBool("isShortAttacking", false);
        }

        //Long Attack
        if (Input.GetKeyDown("5"))
        {
            if (myAnim.GetBool("isLongAttacking"))
                myAnim.SetBool("isLongAttacking", false);
            else
                myAnim.SetBool("isLongAttacking", true);
        }
        if (Input.GetKeyUp("5"))
        {
            myAnim.SetBool("isLongAttacking", false);
        }

        //Death
        if (Input.GetKeyDown("6"))
        {
            if (myAnim.GetBool("isDying"))
                myAnim.SetBool("isDying", false);
            else
                myAnim.SetBool("isDying", true);
        }


        //Block
        if (Input.GetKeyDown("7"))
        {
            if (myAnim.GetBool("isBlocking"))
                myAnim.SetBool("isBlocking", false);
            else
                myAnim.SetBool("isBlocking", true);
        }
        if (Input.GetKeyUp("7"))
        {
            myAnim.SetBool("isBlocking", false);
        }
        //HitFront
        if (Input.GetKeyDown("8"))
        {
            if (myAnim.GetBool("isHit"))
                myAnim.SetBool("isHit", false);
            else
                myAnim.SetBool("isHit", true);
                myAnim.SetBool("isDying", true);
        }
        if (Input.GetKeyUp("8"))
        {
            myAnim.SetBool("isHit", false);
        }


    }
}
