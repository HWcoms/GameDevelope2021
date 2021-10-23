using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSceneLoader : MonoBehaviour
{
    public string physicsSceneName;
    public float physicsSceneTimeScale = 1;
    public PhysicsScene physicsScene;

    // Start is called before the first frame update
    void Start()
    {
        LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
        Scene scene = SceneManager.LoadScene(physicsSceneName, param);

        physicsScene = scene.GetPhysicsScene();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(physicsScene != null)
        {
            physicsScene.Simulate(Time.fixedDeltaTime * physicsSceneTimeScale);
        }
    }
}
