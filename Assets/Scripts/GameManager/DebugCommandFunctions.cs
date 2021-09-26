using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandFunctions : MonoBehaviour
{
    public static DebugCommandFunctions instance;

    [SerializeField] private EnemyHealth[] load_EnemyHealth;

    static string des;

    public void InvokeRun(string function, float delay)
    {
        print("<color=#00FF00> *command* </color>" + "<color=#00FF00>" + function.ToUpper() +"</color>" + " : " + des);
        Invoke(function, delay);
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
        load_EnemyHealth = GameObject.FindObjectsOfType<EnemyHealth>();
        
        foreach(EnemyHealth enemy in load_EnemyHealth)
        {
            if (enemy.getDead()) enemy.setDead(false);

            enemy.changeHp(99999);
        }
    }

    public void kill()
    {
        load_EnemyHealth = GameObject.FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth enemy in load_EnemyHealth)
        {
            enemy.changeHp(-99999);
        }
    }
    
    void Awake()
    {
        instance = this;
    }

}
