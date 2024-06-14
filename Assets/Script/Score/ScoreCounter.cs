using UnityEngine;
using static NoteGenerator;
using static Judge;

public class ScoreCountor : MonoBehaviour
{
    int score;
    int combo;

    const int PerfectScore = 1000;
    const int GreatScore   = 100;
    const int BadScore     = 50;

    public void Initialized()
    {
        // スコアコンボ初期化
        score = 0;
        combo = 0;
    }

    /// <summary>
    /// 受けとったノーツの種類と判定結果からスコアを更新
    /// </summary>
    /// <param name="notesType"></param>
    /// <param name="judge"></param>
    public void UpdateScore(NotesType notesType, JudgeNumber judge = 0)
    {
        // ノーツの種類から処理を分ける
        if(notesType == NotesType.NormalNote)
        {
            // 判定に応じてスコア加算
            switch (judge)
            {
                case JudgeNumber.Perfect:
                    break;
            }
        }
    }
}
