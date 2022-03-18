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
    public GameObject[] sk;
    public int death_monster = 0;

    public int Quest_number = 1; //특정 지역에 따라 값을 다르게 할수 있도록함. 나중에는 0으로 초기화하여 변하도록 하여 처음에 퀘스트창 안뜨게함.

    void Start()
    {
        QuestText.enabled = false;          
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
            TextChange_Defese();
        }
    }

    public void TextChange_MonsterAttack()
    {
        QuestText.GetComponent<TextMeshProUGUI>().text = " Attack QUEST " + "\n" + "\n\n" + death_monster.ToString() + " / " + Monster_Max.ToString();
    }

    public void TextChange_Defese()
    {
        QuestText.GetComponent<TextMeshProUGUI>().text = " Defense QUEST ";
    }
}
