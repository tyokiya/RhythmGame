using System;
using UnityEngine;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] SoundManager.MusicNameList music; // プレイする曲
    [SerializeField] SoundManager  soundManager;       // サウンドマネージャー
    [SerializeField] NoteGenerator notesGenerator;     // ノーツジェネレーター
    [SerializeField] float         notesSpeed = 5.0f;  // ノーツ速度

    // イベントクラス宣言
    GameEvent gameEvent = new GameEvent();

    void Start()
    {
        // イベント追加
        AddEvent();
        // ノーツ速度設定
        SetNotesSpeed();
        // スタートイベント呼び出し
        gameEvent.OnGameStart(music);
    }

    /// <summary>
    /// ノーツ速度の設定
    /// </summary>
    void SetNotesSpeed()
    {
        notesGenerator.SetNotesSpeed(notesSpeed); // ジェネレーターのノーツスピード設定     
    }

    /// <summary>
    /// イベントの追加
    /// </summary>
    void AddEvent()
    {
        // ゲームスタートイベント
        gameEvent.OnGameStart += soundManager.PlayMusic;
        gameEvent.OnGameStart += notesGenerator.LoadMusicalScoreData;
    }

    /// <summary>
    /// イベント削除
    /// </summary>
    void DestroyEvent()
    {
        // ゲームスタートイベント
        gameEvent.OnGameStart -= soundManager.PlayMusic;
        gameEvent.OnGameStart -= notesGenerator.LoadMusicalScoreData;
    }
}
