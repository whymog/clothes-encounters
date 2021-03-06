﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
  public static string selectedSock = "";

  public AudioSource audioSource;

  public AudioClip sockBeerAudio;
  public AudioClip sockBlackTieAudio;
  public AudioClip sockCuteAudio;
  public AudioClip sockFuzzyAudio;
  public AudioClip sockGooglyAudio;
  public AudioClip sockGooseAudio;
  public AudioClip sockMermaidAudio;
  public AudioClip sockMonsterAudio;
  public AudioClip sockPipeCleanerAudio;
  public AudioClip sockPompomAudio;
  public AudioClip sockSportAudio;
  public AudioClip sockStripedAudio;
  public AudioClip sockTallAudio;

  // Update is called once per frame
  void OnTriggerEnter2D(Collider2D collider)
  {
    Debug.Log(collider.name);

    selectedSock = collider.name;

    AudioClip clipToPlay = sockBeerAudio;

    if (collider.name == "SockBeer")
    {
      clipToPlay = sockBeerAudio;
    }
    else if (collider.name == "SockBlackTie")
    {
      clipToPlay = sockBlackTieAudio;
    }
    else if (collider.name == "SockCute")
    {
      clipToPlay = sockCuteAudio;
    }
    else if (collider.name == "SockFuzzy")
    {
      clipToPlay = sockFuzzyAudio;
    }
    else if (collider.name == "SockGoogly")
    {
      clipToPlay = sockGooglyAudio;
    }
    else if (collider.name == "SockGoose")
    {
      clipToPlay = sockGooseAudio;
    }
    else if (collider.name == "SockMermaid")
    {
      clipToPlay = sockMermaidAudio;
    }
    else if (collider.name == "SockMonster")
    {
      clipToPlay = sockMonsterAudio;
    }
    else if (collider.name == "SockPipeCleaner")
    {
      clipToPlay = sockPipeCleanerAudio;
    }
    else if (collider.name == "SockPompom")
    {
      clipToPlay = sockPompomAudio;
    }
    else if (collider.name == "SockSport")
    {
      clipToPlay = sockSportAudio;
    }
    else if (collider.name == "SockStriped")
    {
      clipToPlay = sockStripedAudio;
    }
    else if (collider.name == "SockTall")
    {
      clipToPlay = sockTallAudio;
    }

    audioSource.clip = clipToPlay;
    audioSource.Play(0);
  }
}
