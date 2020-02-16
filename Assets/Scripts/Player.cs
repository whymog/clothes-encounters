﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

  public int playerNumber;
  private bool isBetweenTurns;
  private bool isEndOfTurn;
  private bool isGameOver;

  private IEnumerator timerCoroutine;

  public int turnLength;

  public GameObject Timer;
  public GameObject Wheel;
  public GameObject Selector;
  public GameObject Clue;
  public GameObject ClueText1;
  public GameObject ClueText2;
  public GameObject TurnTimer;
  public GameObject ResultText;
  public GameObject ResultTextDescription;

  // TODO: Remove once this is managed by GameManager
  public Sprite SockBeer;
  public Sprite SockBeerShadow;
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
    isBetweenTurns = false;
    isEndOfTurn = false;
    isGameOver = false;

    if (turnLength <= 0)
    {
      turnLength = 10;
    }

    Clue.SetActive(false);
    Wheel.SetActive(false);
    Selector.SetActive(false);
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
      Wheel.SetActive(false);
      Selector.SetActive(false);

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

        string sockName = GameManager.sockOrder[GameManager.turnNumber - 1];

        if (sockName == "SockBeer")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockBeerShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "tall boys for your horses";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "sudsy";
        }
        else if (sockName == "SockCute")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockCuteShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "keeps you alive";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "dainty";
        }
        else if (sockName == "SockSport")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockSportShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "gatorade";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "headband";
        }
        else if (sockName == "SockGoose")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockGooseShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "bread";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "anger";
        }
        else if (sockName == "SockGoogly")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockGooglyShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "ICU";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "wink";
        }
        else if (sockName == "SockFuzzy")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockFuzzyShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "touch it";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "an aesthetic choice";
        }
        else if (sockName == "SockStriped")
        {
          Clue.GetComponent<SpriteRenderer>().sprite = SockStripedShadow;
          ClueText1.GetComponent<TextMeshProUGUI>().text = "delineation";
          ClueText2.GetComponent<TextMeshProUGUI>().text = "contrast";
        }
      }
      else
      {
        // The player is choosing
        // Show wheel
        Wheel.SetActive(true);
        Selector.SetActive(true);

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

      Clue.SetActive(true);
      Wheel.SetActive(false);
      Selector.SetActive(false);

      string sockName = GameManager.sockOrder[GameManager.turnNumber - 1];

      if (sockName == "SockBeer")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockBeer;
      }
      else if (sockName == "SockCute")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockCute;
      }
      else if (sockName == "SockSport")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockSport;
      }
      else if (sockName == "SockGoose")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockGoose;
      }
      else if (sockName == "SockGoogly")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockGoogly;
      }
      else if (sockName == "SockFuzzy")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockFuzzy;
      }
      else if (sockName == "SockStriped")
      {
        Clue.GetComponent<SpriteRenderer>().sprite = SockStriped;
      }

      if (GameManager.lastGuessWasCorrect)
      {
        ResultText.GetComponent<TextMeshProUGUI>().text = "Correct!";


        if (sockName == "SockBeer")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "We had a lot of crazy nights, huh?";
        }
        else if (sockName == "SockCute")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "It's from our first Valentine's Day together.";
        }
        else if (sockName == "SockSport")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "We made so many friends playing soccer in that local league. Those are good memories.";
        }
        else if (sockName == "SockGoose")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "Remember when you saved me from that super angry goose?";
        }
        else if (sockName == "SockGoogly")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "It's kinda trippy. Remember when we got too high and tried to do magic eye puzzles?";
        }
        else if (sockName == "SockFuzzy")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "It's fuzzy wuzzy!";
        }
        else if (sockName == "SockStriped")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "The lines remind me of all of the highways we used to drive down.";
        }
      }
      else
      {
        ResultText.GetComponent<TextMeshProUGUI>().text = "Incorrect...";

        if (sockName == "SockBeer")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "You drink too much for someone your age.";
        }
        else if (sockName == "SockCute")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "Our first Valentine's together seems so far away. Are either of us the same people?";
        }
        else if (sockName == "SockSport")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "I always felt like I was more into soccer than you. I wish I'd kept going with it.";
        }
        else if (sockName == "SockGoose")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "You don't even remember our goose adventures? It's like I barely know you.";
        }
        else if (sockName == "SockGoogly")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "It looks like your mom's eye. She was always judging me.";
        }
        else if (sockName == "SockFuzzy")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "I normally love fuzzy things, but right now I just want to shave this sock.";
        }
        else if (sockName == "SockStriped")
        {
          ResultTextDescription.GetComponent<TextMeshProUGUI>().text = "The lines remind me of how long I was trapped in this relationship with you.";
        }
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
