using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] bool checkPlayer;
    GameObject player;
    CharacterHealth playerHPscript;
    MultiSceneLoader multiSceneLoader;

    [SerializeField] bool gOverOnce = false;

    public List<string> sceneNames;

    [SerializeField] GameObject gOverUI;

    // Start is called before the first frame update
    void Start()
    {
        checkPlayer = false;
        gOverOnce = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHPscript = player.GetComponent<CharacterHealth>();
        multiSceneLoader = GameObject.Find("SceneManager").GetComponent<MultiSceneLoader>();

        gOverUI = GameObject.Find("GameOverUI");
        gOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gOverOnce) return;

        if (!checkPlayer)
        {
            if (player != null)
            {
                //print(player + ": player found");
                checkPlayer = true;
            }

            return;
        }
        
        if(playerHPscript.getDead())
        {
            GameOver();

            gOverOnce = true;
        }
    }

    void GameOver()
    {
        if (gOverOnce) return;

        gOverUI.SetActive(true);

        Animator gOverUIAnim = gOverUI.GetComponentInChildren<Animator>();
        float uiAnimLength = gOverUIAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        print(gOverUIAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name + ": " + uiAnimLength + " sec");

        StartCoroutine(ResetScenesDelay(uiAnimLength));
    }

    void ResetScenes()
    {
        //print("main char dead");
        sceneNames = multiSceneLoader.GetSceneName();

        //Base scene Load
        multiSceneLoader.LoadScene(sceneNames[0], 0);
        print("Load" + "<color=#FFFF00> *GameManager* </color>" + "<color=#FFFF00>" + sceneNames[0] + "</color>");

        //Additive scenes Load
        for(int i=1; i < sceneNames.Count; i++)
        {
            multiSceneLoader.LoadScene(sceneNames[i], 1);
            print("Load" + "<color=#FFFF00> *GameManager* </color>" + "<color=#FFFF00>" + sceneNames[i] + "</color>");
        }
    }

    IEnumerator ResetScenesDelay(float delay)
    {
        print("1");

        yield return new WaitForSeconds(delay);

        ResetScenes();
    }
}
