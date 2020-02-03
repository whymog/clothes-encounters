using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
  public static string selectedSock = "";

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log(collider.name);

    selectedSock = collider.name;
  }
}
