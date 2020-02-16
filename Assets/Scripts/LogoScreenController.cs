using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScreenController : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(SwitchScenes());
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Title");
    }
}

