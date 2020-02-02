using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("InputManager is awake.");
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      Debug.Log("Pressed Space");
      if (GameManager.canAdvanceScene == true && (GameManager.sceneName != "Game" || GameManager.sceneName == "Game" && GameManager.isGameOver == true))
      {
        if (GameManager.sceneName == "Results")
        {
          SceneManager.LoadScene(0);
        }
        else
        {
          SceneManager.LoadScene(GameManager.sceneIndex + 1);
        }
      }
      else if (GameManager.sceneName == "Game")
      {
        // Handle game input here; hacky, I know, but whatever
        if (Player.isPlayerInputAllowed)
        {
          // Ok, do it
        }
      }
    }
  }
}
