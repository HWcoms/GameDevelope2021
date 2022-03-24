using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateM_Manager : MonoBehaviour
{
    // public List<GameObject> Spot = new List<GameObject>();
    public Transform[] Spot;
    public GameObject[] Monster;


    public int monsterAmount = 12;
    private int remainMonster;
    public int monsterPerTimer = 3;

    public float spawnPosOffset = 0.5f;
    public float spawnYpos = 3.0f;
    public float spawnTime = 3;
    public float Timer;
    public List<GameObject> MonsterList = new List<GameObject>();

    private int spotvalue = 0;
    private int Monstervalue = 0;

    [SerializeField] private bool spawnDone;

    void Start()
    {
        spawnDone = false;

        MonsterList.Clear();
        Timer = spawnTime;

        getSpot();
        remainMonster = monsterAmount;
    }
    void Update()
    {
        if (spawnDone) return;

        Timer -= Time.deltaTime;
        if (Timer < 0.0f)
        {
            CreateM();
            Timer = spawnTime;
        }
    }

    void getSpot()
    {
        Transform[] temp = transform.Find("SpawnPos").GetComponentsInChildren<Transform>();
        Spot = new Transform[transform.Find("SpawnPos").childCount];

        for (int i = 1; i < temp.Length; i++)
            Spot[i-1] = temp[i];
    }
    void CreateM()
    {
        int spotvalue = Random.Range(0, Spot.Length);
        for (int i = 0; i < monsterPerTimer; i++)
        {
            remainMonster--;

            Vector3 randomPos = new Vector3(Random.Range(-spawnPosOffset, spawnPosOffset), spawnYpos , Random.Range(-spawnPosOffset, spawnPosOffset));

            int Monstervalue = Random.Range(0, Monster.Length);
            var currentMonster =Instantiate(Monster[Monstervalue], Spot[spotvalue].transform.position + randomPos, Spot[spotvalue].transform.rotation);
            currentMonster.SetActive(true);
            MonsterList.Add(currentMonster);

            if (remainMonster <= 0)
            {
                spawnDone = true;
                remainMonster = 0;
                break;
            }    
        }
    }

}
