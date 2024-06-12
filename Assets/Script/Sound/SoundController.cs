using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource; // 楽曲再生用audioSource
    [SerializeField] AudioSource seAudioSource;    // SE再生用audioSource    
    [SerializeField] List<AudioClip> seList;       // SEのAudioClipリスト
    [SerializeField] float musicVolume = 0.5f;
    [SerializeField] float seVolume = 1.0f;
    AudioClip musicAudioClip; // 再生する楽曲のaudioClip

    // 再生する楽曲の名前リスト
    public enum MusicNameList
    {
        オーバーライド,
        ロングノーツテスター,
    }

    public enum SEList
    {
        tapSE = 0,
    }

    void Start()
    {
        musicAudioSource.volume = musicVolume;
        seAudioSource.volume = seVolume;
    }

    /// <summary>
    /// 再生する楽曲の読み込み
    /// </summary>
    /// <param name="musicName">流す曲名</param>
    public void LoadMusic(MusicNameList musicName)
    {
        // 再生する楽曲の取得
        musicAudioClip = (AudioClip)Resources.Load("Music/" + musicName);
    }

    /// <summary>
    /// 楽曲を再生する
    /// </summary>
    public void PlayMusic()
    {        
        // 楽曲の再生
        musicAudioSource.PlayOneShot(musicAudioClip);
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="plySE">再生するSE</param>
    public void PlySE(SEList plySE)
    {
        switch(plySE)
        {
            case SEList.tapSE:
                // SE再生
                seAudioSource.PlayOneShot(seList[((int)SEList.tapSE)]);
                break;
            default: 
                break;
        }
    }
}
