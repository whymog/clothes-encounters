using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAndTutorialAudioSource : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    DontDestroyOnLoad(gameObject);
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.sceneIndex > 2)
    {
      Destroy(gameObject);
    }
  }
}
