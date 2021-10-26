using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSceneLoader : MonoBehaviour
{
    private Transform playerSpawnPoint;
    public float playerSpawnDelay = 1.0f;

    public string physicsSceneName;
    public float physicsSceneTimeScale = 1;
    public PhysicsScene physicsScene;

    public bool isLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = GameObject.Find("Spawn_Point").transform;

        Scene scene;
        LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);

        if (SceneManager.sceneCount > 0)
        {
            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene _scene = SceneManager.GetSceneAt(i);
                if (_scene.name.Equals(physicsSceneName))
                {
                    print(_scene.name);
                    physicsScene = _scene.GetPhysicsScene();
                    isLoaded = true;
                }
            }
        }

        if (!isLoaded)
        {
            scene = SceneManager.LoadScene(physicsSceneName, param);
            physicsScene = scene.GetPhysicsScene();

            isLoaded = true;
        }

        //spawn player
        StartCoroutine(WaitToSpawn(playerSpawnDelay));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(physicsScene != null)
        {
            physicsScene.Simulate(Time.fixedDeltaTime * physicsSceneTimeScale);
        }
    }

    private IEnumerator WaitToSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawnPoint.position;
    }
}
