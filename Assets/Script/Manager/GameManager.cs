using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static SoundController;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] MusicNameList    music;              // プレイする曲
    [SerializeField] SoundController  soundController; 　 // サウンドマネージャー
    [SerializeField] UIManager        uiManager;          // UIマネージャー
    [SerializeField] Judge            judgeController; 　 // 判定
    [SerializeField] NoteGenerator    notesGenerator;     // ノーツジェネレーター
    [SerializeField] ScoreCountor     scoreCountor;       // スコアコントローラー
    [SerializeField] float            notesSpeed = 5.0f;  // ノーツ速度

    [Header("テストモード設定")]
    [SerializeField] bool isTestMode = false;
    [SerializeField] float musicStartTime_TestMode;

    // イベントクラス宣言
    GameEvent gameEvent = new GameEvent();

    // フラグ
    bool isGameStart; // ゲーム開始フラグ
    
    // 定数
    const float GameStartDelay = 3f;

    async void Start()
    {        
        // ゲーム開始フラグを下ろす
        isGameStart = false;
        // イベント追加
        AddEvent();
        // ノーツ速度設定
        notesGenerator.SetNotesSpeed(notesSpeed);
        // データロードベント呼び出し
        gameEvent.OnDataLoad(music);
        // 初期化イベント呼び出し
        gameEvent.Initialize();

        // GameStartDelay秒待機後ゲーム開始フラグ
        await UniTask.Delay(TimeSpan.FromSeconds(GameStartDelay));

        Debug.Log("ゲーム開始");
        gameEvent.IsGameStart(); // ゲーム開始イベント呼出し
        isGameStart = true;      // ゲーム開始フラグを立てる

        // テストモードのフラグが立っていたらテスト用のデータを設定
        if (isTestMode)
        {
            Debug.Log("テストモード中");
            soundController.SetTestMode(musicStartTime_TestMode); // 楽曲の再生時間設定
            judgeController.SetTestMode(musicStartTime_TestMode); // 楽曲の再生時間に合わせて判定設定
            notesGenerator.SetTestMode(musicStartTime_TestMode);  // 楽曲再生時間に合わせてノーツの座標設定
        }
    }

    /// <summary>
    /// イベントの追加
    /// </summary>
    void AddEvent()
    {
        // データロードイベント
        gameEvent.OnDataLoad += soundController.LoadMusic;
        gameEvent.OnDataLoad += notesGenerator.LoadMusicalScoreData;
        // ゲームスタートイベント
        gameEvent.IsGameStart += soundController.PlayMusic;
        gameEvent.IsGameStart += notesGenerator.SetGameStart;
        gameEvent.IsGameStart += judgeController.SetGameStart;
        gameEvent.IsGameStart += uiManager.ActiveOffGameStartObject;
        // 初期化イベント
        gameEvent.Initialize += scoreCountor.Initialized;
        gameEvent.Initialize += uiManager.Initialize;
    }

    /// <summary>
    /// イベント削除
    /// </summary>
    /// TODO ゲーム終了処理の実装時に呼ぶ
    void DestroyEvent()
    {
        // ゲームスタートイベント
        gameEvent.OnDataLoad -= soundController.LoadMusic;
        gameEvent.OnDataLoad -= notesGenerator.LoadMusicalScoreData;
        // ゲームスタートイベント
        gameEvent.IsGameStart -= soundController.PlayMusic;
        gameEvent.IsGameStart -= notesGenerator.SetGameStart;
        gameEvent.IsGameStart -= judgeController.SetGameStart;
        gameEvent.IsGameStart -= uiManager.ActiveOffGameStartObject;
        // 初期化イベント
        gameEvent.Initialize -= scoreCountor.Initialized;
        gameEvent.Initialize -= uiManager.Initialize;
    }
}
