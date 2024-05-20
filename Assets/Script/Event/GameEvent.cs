using System;

/// <summary>
/// ゲームイベントを保持するクラス
/// </summary>
public class GameEvent
{
    public Action<SoundController.MusicNameList> OnDataLoad; // 必要データのロードイベント
    public Action IsGameStart;                                 // ゲームスタートイベント
}
