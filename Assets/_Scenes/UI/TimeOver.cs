using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeOver : MonoBehaviour
{
    [SerializeField] GameObject TimeOverText;
    //[SerializeField] GameObject PlayerDieText;
    [SerializeField] GameObject QuestClearText;
    [SerializeField] float maxTime = 10f;
    [SerializeField] private Quest que;

    //public GameObject player;
    float timeLeft;
    [SerializeField] Image Timebar;

    bool Quest_S_F = false;

    bool QuestDone;


    // Start is called before the first frame update
    void Start()
    {
        TimeOverText.SetActive(false);
        QuestClearText.SetActive(false);
        Timebar = GameObject.Find("TimeBar").GetComponent<Image>();
        timeLeft = maxTime;
        Timebar.enabled = false; 
        que = GetComponent<Quest>();
        //que = GetComponentInChildren<Quest>();

        QuestDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestDone)
        {
            return;
        }

        if(que.Quest_number==1)
        {
            Timebar.enabled = false;
            if (que.death_monster==que.Monster_Max)
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
        else 
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

                Time.timeScale = 0f;
            }
            else
            {
                //QuestClearText.SetActive(true);
                Time.timeScale = 0f;
            }

        }

        IEnumerator delayOff(float delay)
        {
            yield return new WaitForSeconds(delay);

            TimeOverText.SetActive(false);
            QuestClearText.SetActive(false);

            QuestDone = true;
        }

    }
}
