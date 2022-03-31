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

    private float wallAlpha = 0.0f;
    public float fadeSpeed = 15.0f;

    //public GameObject[] wallObjs;
    public List<GameObject> wallObjs;

    [SerializeField]private GameObject questObj;

    TimeOver timeOverScript;    //is mission done;

    private void OnEnable()
    {
        if (wallObjs.Count == 0)
        {
            print("load walls obj");
            getWallObjs();
        }
        InitMark();
        InitObjs();
    }

    private void OnDisable()
    {
        markerScript.MarkEnd();
        if(questObj != null)
            questObj.SetActive(false);
    }

    void InitMark()
    {
        //get marker info
        missionMarker = GameObject.Find("MissionMarker");
        markerScript = missionMarker.GetComponent<MissionWaypoint>();
        markerScript.TargetTemp = this.transform;

        markerScript.MarkStart();
    }

    void InitObjs()
    {
        oncePlayed = false;

        missionObjList.Clear();

        childCount = this.transform.childCount;

        for (int i = 0; i < childCount; ++i)
        {
            missionObjList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject childs in missionObjList)
        {
            childs.SetActive(false);
        }

        if(questObj == null)
            questObj = this.transform.Find("Quest").gameObject;
        questObj.SetActive(false);

        timeOverScript = questObj.GetComponent<TimeOver>();
    }

    void getWallObjs()
    {
        wallObjs.Clear();

        Transform parentWall = transform.Find("Walls");
        wallObjs = AllChilds(parentWall.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        getWallObjs();
        InitMark();
        InitObjs();
    }

    // Update is called once per frame
    void Update()
    {
        //미션 클리어 조건
        if(timeOverScript.questFinished)
        {
            StartCoroutine(offDisplay(0.0f));
        }

        if(wallObjs.Count == 0 || !wallObjs[0].activeSelf) return;

        foreach(GameObject wallObj in wallObjs)
        {
            //print("rendered");

            changeAlpha(wallObj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !oncePlayed)
        {
            markerScript.MarkEnd();

            print("mission start");

            oncePlayed = true;

            foreach (GameObject childs in missionObjList)
            {
                childs.SetActive(true);
            }

            //alpha
            StartCoroutine(wallFadeIn(fadeSpeed));


            //quset obj
            questObj.SetActive(true);
            questObj.transform.parent = GameObject.Find("Canvas").gameObject.transform;

            RectTransform rectTr = questObj.GetComponent<RectTransform>();
            rectTr.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rectTr.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    void changeAlpha(GameObject obj)
    {
        Color col;

        MeshRenderer meshRenderer = null;
        Renderer particleSystemRenderer = null;

        try
        {
            meshRenderer = obj.GetComponent<MeshRenderer>();
            particleSystemRenderer = obj.GetComponent<ParticleSystem>().GetComponent<Renderer>();
        }
        catch
        {
        }

        if (meshRenderer == null && particleSystemRenderer == null) return;

        if(meshRenderer != null && meshRenderer.material.shader.name.Equals("HDRP/Unlit"))
        {
            col = meshRenderer.material.GetColor("_UnlitColor");
            
            //print(meshRenderer.material.name + ": " + meshRenderer.material.shader.name);
            col.a = wallAlpha;
            obj.GetComponent<MeshRenderer>().material.SetColor("_UnlitColor", col);
        }
        

        //print(meshRenderer.material.name);
        if (particleSystemRenderer != null && particleSystemRenderer.material.shader.name.Equals("Slash/Bird"))
        {
            col = particleSystemRenderer.material.GetColor("_TintColor");

            //print(particleSystemRenderer.material.name + ": " + particleSystemRenderer.material.shader.name);
            col.a = wallAlpha;

            particleSystemRenderer.material.SetColor("_TintColor", col);
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

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }
    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }
}
