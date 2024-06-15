using UnityEngine;
using static NoteGenerator;
using static Judge;

public class ScoreCountor : MonoBehaviour
{
    [SerializeField] FrameController frameController; // フレームコントローラー
    int score;
    int combo;

    const int PerfectScore = 1000;
    const int GreatScore   = 100;
    const int BadScore     = 50;
    const int LongNotesAddScoreInterval = 30; // ロングノーツのスコア加算間隔

    public void Initialized()
    {
        // スコアコンボ初期化
        score = 0;
        combo = 0;
    }

    /// <summary>
    /// 受けとったノーツの種類と判定結果からスコアを更新
    /// </summary>
    /// <param name="notesType">判定したノーツの種類</param>
    /// <param name="judge">判定結果(ロングノーツの場合は省略可能)</param>
    public void UpdateScore(NotesType notesType, JudgeNumber judge = 0)
    {
        // ノーツの種類から処理を分ける
        if(notesType == NotesType.NormalNote)    // ノーマルノーツ
        {
            // 判定に応じてスコア加算
            switch (judge)
            {
                case JudgeNumber.Perfect:               // パーフェクト
                    score += PerfectScore; // スコア加算
                    combo++;               // コンボ加算
                    break;
                case JudgeNumber.Great:                 // グレート
                    score += GreatScore;
                    combo++;
                    break;
                case JudgeNumber.Bad:                   // バッド
                    score += BadScore;
                    combo++;
                    break;
                case JudgeNumber.Miss:                  // ミス
                    // 加算なし
                    combo = 0; // コンボリセット
                    break;
                default:
                    break; 
            }
        }
        else if(notesType == NotesType.LongNote) // ロングノーツ
        {
            // 0.5秒ごとにスコアとコンボ加算
            // 判定はすべてPerfect判定として扱われる
            if(frameController.GetNowTortalFPS() == LongNotesAddScoreInterval)
            {
                score += PerfectScore; // スコア加算
                combo++;               // コンボ加算
            }
        }
    }
}
