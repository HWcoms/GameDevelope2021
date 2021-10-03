using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandFunctions : MonoBehaviour
{
    public static DebugCommandFunctions instance;

    //[SerializeField] private Object[] loaded_Script;
    [SerializeField] private List<Object> loaded_Script;

    static string des;

    public void InvokeRun(string function, float delay)
    {
        print("<color=#00FF00> *command* </color>" + "<color=#00FF00>" + function.ToUpper() +"</color>" + " : " + des);
        Invoke(function, delay);

        ClearLoadedList();
    }

    public static void run(string command, string description)
    {
        if (instance)
        {
            des = description;
            instance.InvokeRun(command, 0.0f);
        }
    }

    public void heal()
    {
        //loaded_Script = GameObject.FindObjectsOfType<EnemyHealth>();
        loaded_Script.AddRange(GameObject.FindObjectsOfType<EnemyHealth>());

        foreach (EnemyHealth enemyScript in loaded_Script)
        {
            if (enemyScript.getDead()) enemyScript.setDead(false);

            enemyScript.changeHp(99999);
        }
    }

    public void kill()
    {
        loaded_Script.AddRange(GameObject.FindObjectsOfType<EnemyHealth>());

        foreach (EnemyHealth enemyScript in loaded_Script)
        {
            enemyScript.changeHp(-99999);
        }
    }

    public void heal_player()
    {
        loaded_Script.AddRange(GameObject.FindObjectsOfType<CharacterHealth>());

        foreach (CharacterHealth playerScript in loaded_Script)
        {
            if (playerScript.getDead())
            {
                playerScript.setDead(false);
                playerScript.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().setMoveAble(true);   //set player moveAble = true
            }

            playerScript.changeHp(99999);
        }
    }

    public void kill_player()
    {
        loaded_Script.AddRange(GameObject.FindObjectsOfType<CharacterHealth>());

        foreach (CharacterHealth playerScript in loaded_Script)
        {
            playerScript.changeHp(-99999, 1);
        }
    }
    
    void Awake()
    {
        instance = this;

        loaded_Script = new List<Object>();
    }

    void ClearLoadedList()
    {
        loaded_Script.Clear();
    }

}
