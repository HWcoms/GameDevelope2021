using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMM : MonoBehaviour
{
    // public List<GameObject> Spot = new List<GameObject>();
    public Transform[] Spot;
    public GameObject[] Monster;

    public int monsterAmount = 12;
    public int monster_min, monster_max;

    public float spawnPosOffset = 0.5f;
    public float spawnYpos = 3.0f;
    public float spawnTime = 3;
    public float Timer;
    public List<GameObject> MonsterList = new List<GameObject>();

    [SerializeField] private List<int> spotvalue;
    private int Monstervalue = 0;
    private int remainMonster;
    private int monsterlevel = 0;

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
            Spot[i - 1] = temp[i];
    }
    void CreateM()
    { 
        //스폰지점 랜덤
        int spotcount = Random.Range(1, Spot.Length);
        for (int s = 0; s < spotcount; s++)
        {
            int val = Random.Range(0, Spot.Length);

            spotvalue.Add(val);

            print("spot value [" + s + "]= " + val);
        }

        monsterlevel = Random.Range(monster_min, monster_max);  //총 4마리
        print("스폰할 몬스터 개수: " + monsterlevel);

        monsterlevel /= spotcount;  //각각 2마리 (스폰 2개일때)

        for (int j = 0; j < spotcount; j++)
        {
            for (int k=0;k<monsterlevel; k++)
            {
                //스폰할 몬스터
                int Monstervalue = Random.Range(0, Monster.Length);

                remainMonster--;

                Vector3 randomPos = new Vector3(Random.Range(-spawnPosOffset, spawnPosOffset), spawnYpos, Random.Range(-spawnPosOffset, spawnPosOffset));

                var currentMonster = Instantiate(Monster[Monstervalue], Spot[spotvalue[j]].transform.position + randomPos, Spot[spotvalue[j]].transform.rotation);
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

        spotvalue.Clear();
    }
}

