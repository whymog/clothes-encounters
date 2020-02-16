using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PIGSquadVideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.Play();
        StartCoroutine(SwitchScenes());
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("TGIHLogo");
    }
}
