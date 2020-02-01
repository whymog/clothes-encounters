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
      if (GameManager.canAdvanceScene == true && (GameManager.sceneName == "Title" || GameManager.sceneName == "Tutorial"))
      {
        SceneManager.LoadScene(GameManager.sceneIndex + 1);
      }
    }
  }
}
