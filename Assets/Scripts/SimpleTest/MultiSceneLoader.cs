using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneLoader : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public float playerSpawnDelay = 1.0f;
    private GameObject player;

    public string characterSceneName;
    //public string mapSceneName;
    public PhysicsScene physicsScene;

    public bool isLoaded = false;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    //map names
    public enum SceneNameEnum {TestMapLoad, mapTest};
    [SerializeField] private SceneNameEnum sceneNames;

    // Start is called before the first frame update
    void Start()
    {
        //playerSpawnPoint = GameObject.Find("Spawn_Point").transform;
        player = GameObject.FindWithTag("Player").gameObject;

        if (!string.IsNullOrEmpty(sceneNames.ToString()))
        {
            if (SceneManager.sceneCount > 0)
            {
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene _scene = SceneManager.GetSceneAt(i);
                    if (_scene.name.Equals(sceneNames.ToString()))
                    {
                        isLoaded = true;
                    }
                }
            }

            if (!isLoaded)
            {
                LoadScene(sceneNames.ToString(), 1);

                isLoaded = true;
            }
        }

        //spawn player
        StartCoroutine(WaitToSpawn(playerSpawnDelay));
    }

    void LoadScene(string sceneName)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName));
    }
    public void LoadScene(string sceneName, int mode)  //mode 0: Load, 1: Additive
    {
        if (mode == 0)
            SceneManager.LoadScene(sceneName);
        else
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    private IEnumerator WaitToSpawn(float delay)
    {
        player.transform.position = new Vector3(100.0f,100.0f,100.0f);
        player.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(delay);

        player.transform.position = playerSpawnPoint.position;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }

    public List<string> GetSceneName()
    {
        Scene[] scene = SceneManager.GetAllScenes();

        List<string> sceneNames = new List<string>();

        foreach (Scene names in scene)
        {
            sceneNames.Add(names.name);

            //print(names.name + " Scene in list");
        }

        return sceneNames;
    }
}
