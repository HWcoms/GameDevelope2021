using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateM_Manager : MonoBehaviour
{
    public GameObject[] Spot = new GameObject[3];
    public GameObject[] Monster = new GameObject[3];
    public GameObject[] Monstermanager;
    //public GameObject Spotmanager;
    public int spotvalue = 0;
    public int Monstervalue = 0;
    public int Monstercount = 4;
    public int j = 0;
    public float Timer;

    void Start()
    {

    }
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0.0f)
        {
            CreateM();
            Timer = 7.0f;
        }
    }

    void CreateM()
    {
        int spotvalue = Random.Range(0, 3);
        int Monstervalue = Random.Range(0, 3);
        for (int i = 0; i < Monstercount + 1; i++)
        {
            Monstermanager[j] = Instantiate(Monster[Monstervalue], Spot[spotvalue].transform.position, Spot[spotvalue].transform.rotation);
            j++;
        }
    }

}
