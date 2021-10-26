using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiSceneLoader : MonoBehaviour
{
    private Transform playerSpawnPoint;
    public float playerSpawnDelay = 1.0f;
    private GameObject player;

    public string characterSceneName;
    public string mapSceneName;
    public PhysicsScene physicsScene;

    public bool isLoaded = false;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.Find("Spawn_Point").transform;
        player = GameObject.FindWithTag("Player").gameObject;

        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene _scene = SceneManager.GetSceneAt(i);
                if (_scene.name.Equals(mapSceneName))
                {
                    isLoaded = true;
                }
            }
        }

        if (!isLoaded)
        {
            LoadScene(mapSceneName, 1);

            isLoaded = true;
        }
        
        //spawn player
        StartCoroutine(WaitToSpawn(playerSpawnDelay));
    }

    void LoadScene(string sceneName)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName));
    }
    void LoadScene(string sceneName, int mode)  //mode 0: Load, 1: Additive
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
    }

    private IEnumerator WaitToSpawn(float delay)
    {
        player.SetActive(false);

        yield return new WaitForSeconds(delay);

        player.SetActive(true);
        player.transform.position = playerSpawnPoint.position;
    }
}
