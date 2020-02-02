using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update

  public static string sceneName = "";
  public static int sceneIndex = 0;
  public static bool canAdvanceScene = true;
  public static bool isGameBeingPlayed = false;
  public static bool isGameOver = true;
  public static int player1Score = 0;
  public static int player2Score = 0;

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

    // Reset stuff on title for repeat plays
    if (sceneName == "Title")
    {
      isGameBeingPlayed = false;
      isGameOver = false;
      player1Score = 0;
      player2Score = 0;
    }
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

    if (scene.name == "Game")
    {
      isGameBeingPlayed = true;
    }
    else
    {
      isGameBeingPlayed = false;
    }

    // TODO: When the game ends, set isGameOver to true and advance to the final scene. Not sure where that goes yet.
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
