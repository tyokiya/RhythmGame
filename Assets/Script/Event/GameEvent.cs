using System;

/// <summary>
/// ゲームイベントを保持するクラス
/// </summary>
public class GameEvent
{
    public Action<SoundManager.MusicNameList> OnGameStart; // ゲームスタートイベント
}
