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
  public GameObject ClueText1;
  public GameObject ClueText2;
  public GameObject TurnTimer;

  // Start is called before the first frame update
  void Start()
  {
    isPlayerInputAllowed = false;
    Clue.SetActive(false);
    ClueText1.GetComponent<TextMeshProUGUI>().text = "";
    ClueText2.GetComponent<TextMeshProUGUI>().text = "";
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
      // If player is giving hints, show this stuff
      if (playerNumber == 1 && GameManager.turnNumber % 2 == 0)
      {
        isBetweenTurns = false;
        Clue.SetActive(true);
        // Stop showing interstitial screen and allow round interaction
        ClueText1.GetComponent<TextMeshProUGUI>().text = "Text 1";
        ClueText2.GetComponent<TextMeshProUGUI>().text = "Text 2";

        // Start timer
        timerCoroutine = TakeTurn(30);
        StartCoroutine(timerCoroutine);
      }
    }
    // TODO: Kill Game Turn coroutine (StopCoroutine()) if player guesses
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

  IEnumerator TakeTurn(int seconds)
  {
    while (seconds > 0)
    {
      TurnTimer.GetComponent<TextMeshProUGUI>().text = seconds.ToString();
      yield return new WaitForSeconds(1f);
      seconds--;
    }
    // Update text to 0
    TurnTimer.GetComponent<TextMeshProUGUI>().text = seconds.ToString();

    // End turn
    // TODO: Create end of turn method
    Debug.Log("Turn over");
  }
}
