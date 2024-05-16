using System;

/// <summary>
/// ゲームイベントを保持するクラス
/// </summary>
public class GameEvent
{
    public Action<SoundController.MusicNameList> OnGameStart; // ゲームスタートイベント
}
