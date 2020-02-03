using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
  private bool isBetweenTurns = false;
  private bool isTurnInProgress = false;
  private bool isEndSuccess = false;
  private bool isEndFailure = false;

  public AudioSource audio;
  public AudioClip guessing;
  public AudioClip success;
  public AudioClip failure;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.isBetweenTurns && !isBetweenTurns)
    {
      Debug.Log("GameMusicManager: Between turns");
      isBetweenTurns = true;
      audio.Stop();
    }
    else if (GameManager.isTurnInProgress && !isTurnInProgress)
    {
      Debug.Log("GameMusicManager: Playing turn audio");

      isBetweenTurns = false;
      isEndFailure = false;
      isEndSuccess = false;
      isTurnInProgress = true;
      audio.clip = guessing;
      audio.Play(0);
    }
    else if (GameManager.isEndOfTurn && !isEndSuccess && !isEndFailure)
    {
      Debug.Log("GameMusicManager: End of turn");

      isTurnInProgress = false;
      audio.Stop();

      if (GameManager.lastGuessWasCorrect)
      {
        Debug.Log("GameMusicManager: Correct");

        isEndSuccess = true;
        // Play success song
        audio.clip = success;
        audio.Play(0);
      }
      else
      {
        Debug.Log("GameMusicManager: Incorrect");

        isEndFailure = true;
        // Play failure song
        audio.clip = failure;
        audio.Play(0);
      }
    }
  }
}
