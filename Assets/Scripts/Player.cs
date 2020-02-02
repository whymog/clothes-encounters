using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

  public int playerNumber;
  private bool isBetweenTurns;
  public static bool isPlayerInputAllowed;

  private IEnumerator timerCoroutine;

  public GameObject Timer;
  public GameObject Wheel;
  public GameObject Clue;

  // Start is called before the first frame update
  void Start()
  {
    isPlayerInputAllowed = false;
  }

  // Update is called once per frame
  void Update()
  {
    // On odd rounds, player 1 searches and player 2 requests
    // On even rounds, player 2 searches and player 1 requests

    if (GameManager.isBetweenTurns && !isBetweenTurns)
    {
      isBetweenTurns = true;
      timerCoroutine = ShowInstructions();
      StartCoroutine(timerCoroutine);
      // Show instructions based on which turn it is
      if (playerNumber == 1 && GameManager.turnNumber % 2 != 0)
      {
        // Odd turn number - player 1 sees search instructions

      }
    }
    else if (!GameManager.isBetweenTurns && isBetweenTurns)
    {
      isBetweenTurns = false;
      // Stop showing interstitial screen and allow round interaction
    }
  }

  IEnumerator ShowInstructions()
  {
    // Show "Get Ready" for 2 seconds
    Timer.GetComponent<TextMeshProUGUI>().text = "Get Ready!";
    yield return new WaitForSeconds(2f);
    // Show timer at 3 seconds
    Timer.GetComponent<TextMeshProUGUI>().text = "3";
    yield return new WaitForSeconds(1f);
    // Show timer at 2 seconds
    Timer.GetComponent<TextMeshProUGUI>().text = "2";
    yield return new WaitForSeconds(1f);
    // Show timer at 1 second
    Timer.GetComponent<TextMeshProUGUI>().text = "1";
    yield return new WaitForSeconds(1f);
    Timer.GetComponent<TextMeshProUGUI>().text = "";
    GameManager.isBetweenTurns = false;
  }
}
