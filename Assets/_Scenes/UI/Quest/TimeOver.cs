using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeOver : MonoBehaviour
{
    public bool questFinished;
    [SerializeField] GameObject TimeOverText;
    //[SerializeField] GameObject PlayerDieText;
    [SerializeField] GameObject QuestClearText;
    [SerializeField] float maxTime = 10f;
    [SerializeField] private Quest quest;

    //public GameObject player;
    float timeLeft;
    [SerializeField] Image Timebar;

    bool questDone;


    // Start is called before the first frame update
    void Start()
    {
        TimeOverText = transform.Find("TImeOver").gameObject;
        QuestClearText = transform.Find("Clear Game").gameObject;

        TimeOverText.SetActive(false);
        QuestClearText.SetActive(false);
        Timebar = GameObject.Find("TimeBar").GetComponent<Image>();
        timeLeft = maxTime;
        Timebar.enabled = false; 
        quest = this.GetComponent<Quest>();
        //quest = GetComponentInChildren<Quest>();

        questDone = false;
        questFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (questDone)
        {
            return;
        }

        if(quest.questList == Quest.QuestList.KillAll)
        {
            Timebar.enabled = false;
            if (quest.death_monster==quest.Monster_Max)
            {
                QuestClearText.SetActive(true);
                //Destroy(QuestClearText,3.0f);
                StartCoroutine(delayOff(3.0f));
            }
            else
            {
                //Quest fail, player Die
            }
           
        }
        else if (quest.questList == Quest.QuestList.TimeDefense) 
        {
            Timebar.enabled = true;
            timeLeft -= Time.deltaTime; //play time check
            Timebar.fillAmount = timeLeft / maxTime;

            if(timeLeft<=0)
            {
                timeLeft = 0;
                TimeOverText.SetActive(true);

                //Destroy(TimeOverText, 3.0f);
                StartCoroutine(delayOff(3.0f));

                //Time.timeScale = 0f;
            }
            else
            {
                //QuestClearText.SetActive(true);
                //Time.timeScale = 0f;
            }

        }

        IEnumerator delayOff(float delay)
        {
            questDone = true;

            yield return new WaitForSeconds(delay);

            TimeOverText.SetActive(false);
            QuestClearText.SetActive(false);

            TimeOver();
        }

        void TimeOver()
        {
            NextMission();
        }

        void NextMission()
        {
            print("next mission");
            questFinished = true;
        }
    }
}
