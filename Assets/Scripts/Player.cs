using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

  public int playerNumber;
  private bool isBetweenTurns;
  private bool isEndOfTurn;
  private bool correctGuess;
  private bool isGameOver;

  private IEnumerator timerCoroutine;

  public int turnLength;

  public GameObject Timer;
  public GameObject Wheel;
  public GameObject Clue;
  public GameObject ClueText1;
  public GameObject ClueText2;
  public GameObject TurnTimer;
  public GameObject ResultText;
  public GameObject ResultTextDescription;

  public Sprite SockCute;
  public Sprite SockCuteShadow;
  public Sprite SockFuzzy;
  public Sprite SockFuzzyShadow;
  public Sprite SockGoogly;
  public Sprite SockGooglyShadow;
  public Sprite SockGoose;
  public Sprite SockGooseShadow;
  public Sprite SockPompom;
  public Sprite SockPompomShadow;
  public Sprite SockSport;
  public Sprite SockSportShadow;
  public Sprite SockStriped;
  public Sprite SockStripedShadow;

  // Start is called before the first frame update
  void Start()
  {
    correctGuess = false;
    isBetweenTurns = false;
    isEndOfTurn = false;
    isGameOver = false;

    if (turnLength <= 0)
    {
      turnLength = 10;
    }

    Clue.SetActive(false);
    ClueText1.GetComponent<TextMeshProUGUI>().text = "";
    ClueText2.GetComponent<TextMeshProUGUI>().text = "";
    ResultText.GetComponent<TextMeshProUGUI>().text = "";
    ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "";

    GameManager.turnNumber = 1;
  }

  // Update is called once per frame
  void Update()
  {
    // On odd rounds, player 1 searches and player 2 requests
    // On even rounds, player 2 searches and player 1 requests

    if (GameManager.isBetweenTurns && !isBetweenTurns)
    {
      Debug.Log("Is between turns");
      isEndOfTurn = false;
      isBetweenTurns = true;
      Clue.SetActive(false);

      ResultText.GetComponent<TextMeshProUGUI>().text = "";
      ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "";

      timerCoroutine = ShowInstructions();
      StartCoroutine(timerCoroutine);
    }
    else if (!GameManager.isBetweenTurns && isBetweenTurns)
    {
      isBetweenTurns = false;
      Debug.Log("Is turn");

      GameManager.isTurnInProgress = true;

      // If player is giving hints, show this stuff
      if ((playerNumber == 1 && GameManager.turnNumber % 2 == 0) || (playerNumber == 2 && GameManager.turnNumber % 2 != 0))
      {
        Clue.SetActive(true);
        // Stop showing interstitial screen and allow round interaction
        ClueText1.GetComponent<TextMeshProUGUI>().text = "Text 1";
        ClueText2.GetComponent<TextMeshProUGUI>().text = "Text 2";

        // Hardcoded this shit I am so sorry
        if (GameManager.turnNumber == 1)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockCuteShadow;
        }
        else if (GameManager.turnNumber == 2)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockSportShadow;
        }
        else if (GameManager.turnNumber == 3)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockGooseShadow;
        }
        else if (GameManager.turnNumber == 4)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockGooglyShadow;
        }
        else if (GameManager.turnNumber == 5)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockFuzzyShadow;
        }
        else if (GameManager.turnNumber == 6)
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockStripedShadow;
        }
      }
      else
      {
        // The player is choosing
        // Show wheel

        // When player makes choice, note it as correct or incorrect
        // Stop guess music

        // TODO: Kill Game Turn coroutine (StopCoroutine()) if player guesses
      }

      // Start turn timer
      timerCoroutine = TakeTurn(turnLength);
      StartCoroutine(timerCoroutine);
    }
    else if (GameManager.isEndOfTurn && !isEndOfTurn)
    {
      Debug.Log("Is end of turn");

      isEndOfTurn = true;
      StopCoroutine(timerCoroutine);

      GameManager.isTurnInProgress = false;

      // Update text to 0
      TurnTimer.GetComponent<TextMeshProUGUI>().text = "";

      // Show results + text
      ClueText1.GetComponent<TextMeshProUGUI>().text = "";
      ClueText2.GetComponent<TextMeshProUGUI>().text = "";

      if (GameManager.turnNumber == 1)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockCute;
      }
      else if (GameManager.turnNumber == 2)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockSport;
      }
      else if (GameManager.turnNumber == 3)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockGoose;
      }
      else if (GameManager.turnNumber == 4)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockGoogly;
      }
      else if (GameManager.turnNumber == 5)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockFuzzy;
      }
      else if (GameManager.turnNumber == 6)
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockStriped;
      }

      if (correctGuess)
      {
        ResultText.GetComponent<TextMeshProUGUI>().text = "Correct!";
        ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "You did it!";
      }
      else
      {
        ResultText.GetComponent<TextMeshProUGUI>().text = "Incorrect...";
        ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "You done goofed :(";
      }

      if (GameManager.turnNumber >= GameManager.maxTurns && !isGameOver)
      {
        isGameOver = true;
        GameManager.isGameOver = true;
      }
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

  IEnumerator TakeTurn(int seconds)
  {
    while (seconds > 0)
    {
      Debug.Log(seconds);
      TurnTimer.GetComponent<TextMeshProUGUI>().text = seconds.ToString();
      yield return new WaitForSeconds(1f);
      seconds--;
    }

    // End turn
    // TODO: Create end of turn method
    Debug.Log("Turn over");
    GameManager.isEndOfTurn = true;
  }
}
