using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Quest : MonoBehaviour
{
    public enum QuestList
    {
        KillAll,
        TimeDefense
    }

    public QuestList questList;

    [SerializeField] TextMeshProUGUI QuestText;
    //[SerializeField] TextMeshProUGUI TimeBar;
    [SerializeField] public int Monster_Max;
    public List<GameObject> monsters;
    public int death_monster = 0;

    public int Quest_number = 1; //특정 지역에 따라 값을 다르게 할수 있도록함. 나중에는 0으로 초기화하여 변하도록 하여 처음에 퀘스트창 안뜨게함.

    //공백:4 = 1글자, 12글자 최대
    public int questTextLimit = 12;

    private Image textLine;

    void Start()
    {
        QuestText = transform.Find("QuestText").GetComponent<TextMeshProUGUI>();

        QuestText.enabled = false;
        //임시
        monsters = GameObject.Find("MonsterManager").GetComponent<CreateMM>().MonsterList;

        textLine = transform.Find("QuestTextLine").GetComponent<Image>();
        TextInfo();

        Monster_Max = GameObject.Find("MonsterManager").GetComponent<CreateMM>().monsterAmount;
    }

    // Update is called once per frame
    void Update()
    {
        TextRefresh();
    }

    void TextInfo()
    {
        TextRefresh();

        TextLimit();
    }

    void TextRefresh()
    {
        if (questList == QuestList.KillAll)
        {
            int death_state = 0;
            foreach (GameObject number in monsters)
            {
                if (number.GetComponent<EnemyHealth>().getDead())
                {
                    death_state++;
                }
            }
            death_monster = death_state;

            QuestText.enabled = true;
            TextChange_KillALL();
        }
        else if (questList == QuestList.TimeDefense)
        {
            QuestText.enabled = true;
            TextChange_TimeDefense();
        }
    }

    public void TextChange_KillALL()
    {
        
        QuestText.GetComponent<TextMeshProUGUI>().text = "모든 적 제거하기 " + death_monster.ToString() + "/" + Monster_Max.ToString();
    }

    public void TextChange_TimeDefense()
    {
        QuestText.GetComponent<TextMeshProUGUI>().text = "시간동안 버티기";
    }

    void TextLimit()
    {
        TextMeshProUGUI text = QuestText.GetComponent<TextMeshProUGUI>();

        float sizeOftext = getTextSize();
        //print(sizeOftext);

        if(sizeOftext > questTextLimit)
        {
            text.enableAutoSizing = true;

            sizeOftext = questTextLimit;
        }
        else
        {
            text.enableAutoSizing = false;
        }

        float percent = sizeOftext / questTextLimit;
        textLine.fillAmount = percent;
    }

    float getTextSize()
    {
        TextMeshProUGUI text = QuestText.GetComponent<TextMeshProUGUI>();

        float size = 0;

        for (int i=0; i< text.text.Length; i++)
        {
            if(text.text[i] == ' ' || text.text[i] == '/')
            {
                size += 0.25f;
            }
            else
            {
                size += 0.95f;
            }
        }

        return size;
    }
}
