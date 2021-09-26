using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandFunctions : MonoBehaviour
{
    public static DebugCommandFunctions instance;

    [SerializeField] private EnemyHealth[] load_EnemyHealth;

    public void InvokeRun(string function, float delay)
    {
        Invoke(function, delay);
    }

    public static void run(string command)
    {
        if(instance)
            instance.InvokeRun(command, 0.0f);
    }

    public void heal()
    {
        print("healed all");
        load_EnemyHealth = GameObject.FindObjectsOfType<EnemyHealth>();
        
        foreach(EnemyHealth enemy in load_EnemyHealth)
        {
            if (enemy.getDead()) enemy.setDead(false);

            enemy.changeHp(99999);
        }
    }

    public void kill()
    {
        print("killed all");
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
