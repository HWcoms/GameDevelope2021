using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI QuestText;
    //[SerializeField] TextMeshProUGUI TimeBar;
    [SerializeField] public int Monster_Max;
    public GameObject[] sk = new GameObject[1];
    public int death_monster = 0;

    public int Quest_number = 1; //특정 지역에 따라 값을 다르게 할수 있도록함. 나중에는 0으로 초기화하여 변하도록 하여 처음에 퀘스트창 안뜨게함.

    //공백:4 = 1글자, 12글자 최대
    public int questTextLimit = 12;

    void Start()
    {
        QuestText = transform.Find("QuestText").GetComponent<TextMeshProUGUI>();

        QuestText.enabled = false;
        //임시
        sk[0] = GameObject.Find("D-Knight_Prefab");

        getTextSize();
    }

    // Update is called once per frame
    void Update()
    {
        

        // 죽은상태 대신 살아있는 해골수 구해서
        int death_state = 0;
        foreach (GameObject number in sk)
        {
            Monster_Max = sk.Length;
            if(number.GetComponent<EnemyHealth>().getDead())
            {
                death_state++;
            }
        }
        death_monster = death_state;

        if (Quest_number == 1)
        {
            QuestText.enabled = true;
            TextChange_MonsterAttack();
        }
        else
        {
            QuestText.enabled = true;
            TextChange_Defense();
        }
    }

    public void TextChange_MonsterAttack()
    {
        QuestText.GetComponent<TextMeshProUGUI>().text = "적을 모두 제거하기 " + death_monster.ToString() + " / " + Monster_Max.ToString();
    }

    public void TextChange_Defense()
    {
        QuestText.GetComponent<TextMeshProUGUI>().text = "시간동안 버티기";
    }

    void TextLimit()
    {
        TextMeshProUGUI text = QuestText.GetComponent<TextMeshProUGUI>();

        int sizeOftext;

        //if(text.text.Length > 12)
    }

    float getTextSize()
    {
        TextMeshProUGUI text = QuestText.GetComponent<TextMeshProUGUI>();

        float size = 0;

        for (int i=0; i< text.text.Length; i++)
        {
            if(text.text[i] == ' ')
            {
                print("space");
                size += 0.25f;
            }
            else
            {
                size += 1;
            }
        }

        return size;
    }
}
