﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip musicAudioClip; // 再生する楽曲のaudioClip

    // 再生する楽曲の名前リスト
    public enum MusicNameList
    {
        オーバーライド,
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // audioSorce取得
    }

    /// <summary>
    /// 楽曲を再生する
    /// </summary>
    /// <param name="musicName">流す曲名</param>
    public void PlayMusic(MusicNameList musicName)
    {
        // 再生する楽曲の取得
        musicAudioClip = (AudioClip)Resources.Load("Music/" + musicName);
        // 楽曲の再生
        audioSource.clip = musicAudioClip;
        audioSource.Play();
    }
}
