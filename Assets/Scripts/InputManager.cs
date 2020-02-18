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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Pressed Space");
            if (GameManager.canAdvanceScene == true && (GameManager.sceneName != "Game" || GameManager.sceneName == "Game" && GameManager.isGameOver == true))
            {
                if (GameManager.sceneName == "Results")
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(GameManager.sceneIndex + 1);
                }
            }
            else if (GameManager.sceneName == "Game")
            {
                // Handle game input here; hacky, I know, but whatever
                if (!GameManager.isBetweenTurns && !GameManager.isEndOfTurn)
                {
                    // Turn is in progress - space makes a selection
                    // GameManager.isEndOfTurn = true;
                }
                else if (GameManager.isEndOfTurn)
                {
                    // Start new turn
                    GameManager.turnNumber++;
                    GameManager.isBetweenTurns = true;
                    GameManager.isEndOfTurn = false;
                }
            }
        }
    }
}
