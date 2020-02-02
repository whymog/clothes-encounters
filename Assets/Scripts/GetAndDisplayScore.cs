using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetAndDisplayScore : MonoBehaviour
{
  public int playerNumber;
  private int score;

  // Start is called before the first frame update
  void Start()
  {
    if (playerNumber < 1) playerNumber = 1;

    score = playerNumber == 1 ? GameManager.player1Score : GameManager.player2Score;

    GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
