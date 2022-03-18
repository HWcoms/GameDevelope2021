using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCollisionEvent : MonoBehaviour
{
    public List<GameObject> missionObjList = new List<GameObject>();

    int childCount;

    [SerializeField] bool oncePlayed = false;

    [SerializeField] private GameObject missionMarker;
    private MissionWaypoint markerScript;
    /*
    public bool isKey = false;
    public bool isCondition = false;
    */

    public float delayTime = 3.0f;

    private float wallAlpha = 0.0f;
    public float fadeSpeed = 15.0f;

    public GameObject[] wallObjs;


    // Start is called before the first frame update
    void Start()
    {
        //get marker info
        missionMarker = GameObject.Find("MissionMarker");
        markerScript = missionMarker.GetComponent<MissionWaypoint>();

        markerScript.isMissionStart = true;

        oncePlayed = false;

        childCount = this.transform.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            missionObjList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject childs in missionObjList)
        {
            childs.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in wallObjs)
        {
            /*
            Color32 col = obj.material.GetColor("_Color");
            col.a = wallAlpha;
            obj.material.SetColor("_Color", col);*/
        }

        //미션 클리어 조건
        if(Input.GetKeyDown(KeyCode.R))
               StartCoroutine(offDisplay(0.0f));
     

        if(wallObjs.Length == 0 || !wallObjs[0].activeSelf) return;

        foreach(GameObject wallObj in wallObjs)
        {
            print("rendered");
            //print(wallObjs[0].name);

            Color col = wallObj.GetComponent<MeshRenderer>().material.GetColor("_UnlitColor");
            col.a = wallAlpha;
            //print(col);
            wallObj.GetComponent<MeshRenderer>().material.SetColor("_UnlitColor", col);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !oncePlayed)
        {
            print("mission start");

            oncePlayed = true;

            foreach (GameObject childs in missionObjList)
            {
                childs.SetActive(true);
            }

            //alpha
            StartCoroutine(wallFadeIn(fadeSpeed));
        }
    }

    IEnumerator offDisplay(float delay)
    {
        //StartCoroutine(wallFadeOut());
        //yield return new WaitForSeconds(delay);

        yield return StartCoroutine(wallFadeOut(fadeSpeed));

        foreach (GameObject childs in missionObjList)
        {
            childs.SetActive(false);
        }
    }

    IEnumerator wallFadeIn(float speed = 15.0f, float delay = 0.05f)
    {
        while(wallAlpha < 1.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha += 0.01f * speed;
        }
        wallAlpha = 1;
    }

    IEnumerator wallFadeOut(float speed = 15.0f, float delay = 0.05f)
    {
        while(wallAlpha >= 0.0f)
        {
            //print(wallAlpha);

            yield return new WaitForSeconds(delay);
            wallAlpha -= 0.01f * speed;
        }
        wallAlpha = 0;
    }
}
