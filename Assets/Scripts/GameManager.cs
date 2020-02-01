using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update

  public static string sceneName = "";
  public static int sceneIndex = 0;
  public static bool canAdvanceScene = true;

  private IEnumerator coroutine;

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

    if (scene.name == "Tutorial")
    {
      canAdvanceScene = false;
      coroutine = Timer(5.0f);
      StartCoroutine(coroutine);
    }
    else
    {
      canAdvanceScene = true;
    }
  }

  private IEnumerator Timer(float seconds)
  {
    Debug.Log(seconds);
    yield return new WaitForSeconds(seconds);
    canAdvanceScene = true;
    Debug.Log("Ready to advance to next scene");
  }

  // Update is called once per frame
  void Update()
  {

  }
}
