﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update

  public static string sceneName = "";
  public static int sceneIndex = 0;
  public static bool canAdvanceScene = true;
  public static bool isBetweenTurns = true;
  public static bool isTurnInProgress = false;
  public static bool isGameBeingPlayed = false;
  public static bool isGameOver = false;
  public static bool isEndOfTurn = false;
  public static bool lastGuessWasCorrect = false;
  public static int player1Score = 0;
  public static int player2Score = 0;
  public static int turnNumber = 0;
  public static int maxTurns = 6;

  public static string correctSock1 = "Cute";
  public static string correctSock2 = "Sport";
  public static string correctSock3 = "Goose";
  public static string correctSock4 = "Googly";
  public static string correctSock5 = "Fuzzy";
  public static string correctSock6 = "Striped";

  private IEnumerator coroutine;

  public static int[] sockOrder;

  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    this.UpdateSceneNameAndIndex();

    sockOrder = generateSockOrder();
  }

  int[] generateSockOrder()
  {
    System.Random rng = new System.Random();
    int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    int n = numbers.Length;

    while (n > 1)
    {
      n--;
      int k = rng.Next(n + 1);
      int value = numbers[k];
      numbers[k] = numbers[n];
      numbers[n] = value;
    }

    foreach (int number in numbers)
    {
      Debug.Log(number);
    }

    return numbers;
  }

  void UpdateSceneNameAndIndex()
  {
    Scene scene = SceneManager.GetActiveScene();
    sceneName = scene.name;
    sceneIndex = scene.buildIndex;

    // Reset stuff on title for repeat plays
    if (sceneName == "Title")
    {
      isBetweenTurns = true;
      isEndOfTurn = false;
      isGameBeingPlayed = false;
      isGameOver = false;
      isTurnInProgress = false;
      lastGuessWasCorrect = false;
      player1Score = 0;
      player2Score = 0;
      turnNumber = 0;
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
      coroutine = Timer(0f); // TODO: Set to 5f when done
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
  }

  private IEnumerator Timer(float seconds)
  {
    Debug.Log(seconds);
    yield return new WaitForSeconds(seconds);
    canAdvanceScene = true;
    Debug.Log("Ready to advance to next scene");
  }
}
