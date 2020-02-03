using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
  private Component[] socks;
  // Start is called before the first frame update
  void Start()
  {
    socks = gameObject.GetComponentsInChildren<Transform>();

    float radius = 3f;

    for (int i = 0; i <= 9; i++)
    {
      float angle = i * Mathf.PI * 2f / 9;
      socks[i].transform.localPosition = new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0f);
      socks[i].transform.localRotation = Quaternion.Euler(0, 0, -(float)i * 360f / 9f);
    }

    gameObject.transform.localPosition = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      // rotate left
      transform.localRotation *= Quaternion.Euler(0, 0, 360f / 9f);
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      //rotate right
      transform.localRotation *= Quaternion.Euler(0, 0, -360f / 9f);
    }
    else if (Input.GetKeyDown(KeyCode.Space))
    {
      // check collision
      // if correct, set lastGuessWasCorrect to true
      // otherwise set to false
    }
  }
}
