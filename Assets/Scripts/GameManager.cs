using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update

  public static string sceneName = "";
  public static int sceneIndex = 0;

  void Start()
  {
    DontDestroyOnLoad(this.gameObject);

    this.UpdateSceneNameAndIndex();
  }

  void UpdateSceneNameAndIndex()
  {
    Scene scene = SceneManager.GetActiveScene();
    sceneName = scene.name;
    sceneIndex = scene.buildIndex;
  }

  // called first
  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    UpdateSceneNameAndIndex();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
