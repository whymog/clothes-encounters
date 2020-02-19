using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private Quaternion newRotation;
    private string rotateDirection;
    private Component[] socks;

    // Start is called before the first frame update
    void Start()
    {
        socks = gameObject.GetComponentsInChildren<Transform>();

        float radius = 3f;

        for (int i = 0; i <= 13; i++)
        {
            float angle = i * Mathf.PI * 2f / 13;
            socks[i].transform.localPosition = new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0f);
            socks[i].transform.localRotation = Quaternion.Euler(0, 0, -(float)i * 360f / 13f);
        }

        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        newRotation = transform.localRotation;
        rotateDirection = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateDirection == "")
        {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxisRaw("Horizontal dpad") > 0)
            {
                // rotate left
                newRotation = transform.localRotation;
                newRotation *= Quaternion.Euler(0, 0, 360f / 13f);
                rotateDirection = "clockwise";
                StartCoroutine(RotateWheel(60));
            }
            else if (Input.GetAxis("Horizontal") < 0 || Input.GetAxisRaw("Horizontal dpad") < 0)
            {
                //rotate right
                newRotation = transform.localRotation;
                newRotation *= Quaternion.Euler(0, 0, -360f / 13f);
                rotateDirection = "counterclockwise";
                StartCoroutine(RotateWheel(60));
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                Debug.Log("space!");

                string sockName = GameManager.sockOrder[GameManager.turnNumber - 1];

                if (Selector.selectedSock == sockName)
                {
                    GameManager.lastGuessWasCorrect = true;
                    GameManager.player1Score++;
                    GameManager.player2Score++;
                }
                else
                {
                    GameManager.lastGuessWasCorrect = false;
                    if (GameManager.turnNumber % 2 == 0)
                    {
                        GameManager.player2Score--;
                    }
                    else
                    {
                        GameManager.player1Score--;
                    }
                }

                GameManager.isEndOfTurn = true;
            }
        }
    }

    IEnumerator RotateWheel(int frames)
    {
        int currentFrame = 0;

        while (currentFrame < frames)
        {
            if (rotateDirection == "clockwise")
            {
                transform.localRotation *= Quaternion.Euler(0, 0, 360f / (13f * (float)frames));
            }
            else if (rotateDirection == "counterclockwise")
            {
                transform.localRotation *= Quaternion.Euler(0, 0, -360f / (13f * (float)frames));
            }

            currentFrame++;
            yield return new WaitForEndOfFrame();
        }

        rotateDirection = "";
    }
}
