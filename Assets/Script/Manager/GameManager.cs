using UnityEngine;
using static SoundController;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] MusicNameList music;              // プレイする曲
    [SerializeField] SoundController  soundController; // サウンドマネージャー
    [SerializeField] Judge            judgeController; // 判定
    [SerializeField] NoteGenerator notesGenerator;     // ノーツジェネレーター
    [SerializeField] float         notesSpeed = 5.0f;  // ノーツ速度

    // イベントクラス宣言
    GameEvent gameEvent = new GameEvent();

    // フラグ
    bool isGameStart; // ゲーム開始フラグ

    void Start()
    {
        // フレーム固定
        Application.targetFrameRate = 120;
        // ゲーム開始フラグを下ろす
        isGameStart = false;
        // イベント追加
        AddEvent();
        // ノーツ速度設定
        SetNotesSpeed();
        // データロードベント呼び出し
        gameEvent.OnDataLoad(music);
    }

    void Update()
    {
        if(!isGameStart && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ゲーム開始");
            gameEvent.IsGameStart(); // ゲーム開始イベント呼出し
            isGameStart = true;      // ゲーム開始フラグを立てる
        }
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
        // データロードイベント
        gameEvent.OnDataLoad += soundController.LoadMusic;
        gameEvent.OnDataLoad += notesGenerator.LoadMusicalScoreData;
        // ゲームスタートイベント
        gameEvent.IsGameStart += soundController.PlayMusic;
        gameEvent.IsGameStart += notesGenerator.SetGameStart;
        gameEvent.IsGameStart += judgeController.SetGameStart;
    }

    /// <summary>
    /// イベント削除
    /// </summary>
    void DestroyEvent()
    {
        // ゲームスタートイベント
        gameEvent.OnDataLoad -= soundController.LoadMusic;
        gameEvent.OnDataLoad -= notesGenerator.LoadMusicalScoreData;
        // ゲームスタートイベント
        gameEvent.IsGameStart -= soundController.PlayMusic;
        gameEvent.IsGameStart -= notesGenerator.SetGameStart;
        gameEvent.IsGameStart -= judgeController.SetGameStart;
    }
}
