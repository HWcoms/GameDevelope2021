using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemManager : MonoBehaviour {

    public GameObject[] eventList;
    [SerializeField] private int currentEventIndex = 0;

    [SerializeField] bool isEventRunable;

    private bool isNext = false;
    bool noEvent = false;

    // Use this for initialization
    void Start () {
        isNext = false;
        noEvent = false;
        isEventRunable = true;

        OffAllEvent();
        CheckNextEvent();
    }
	
	// Update is called once per frame
	void Update () {
        if (noEvent) { print("no event"); return; }

        if(isEventRunable)
            RunEvent();

        if (isNext)
            NextEvent();    //last check
    }

    void NextEvent()
    {
        if(CheckNextEvent())
        {
            currentEventIndex++;
        }
        isNext = false;
        isEventRunable = true;
    }

    bool CheckNextEvent()
    {
        int eventSize = eventList.Length - 1;

        if (eventSize <= currentEventIndex)
        {
            noEvent = true;
            currentEventIndex = eventSize;

            return false;
        }
        else
        {
            return true;
        }
    }

    void OffAllEvent()
    {
        if (eventList.Length == 0) return;

        foreach(GameObject eventObj in eventList)
        {
            eventObj.SetActive(false);
        }
    }

    void RunEvent()
    {
        isEventRunable = false;

        if (eventList[currentEventIndex] == null)
        {
            print("current event is null! skipping to next event");
            return;
        }

        eventList[currentEventIndex].SetActive(true);
    }

    public void EndEvent()
    {
        int index = currentEventIndex;
        //index = Mathf.Max(index, 0);

        eventList[index].SetActive(false);
        isNext = true;
    }
}
