using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 判定クラス
/// </summary>
public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] UIController uiController;    // UIコントローラー
    [SerializeField] NoteGenerator notesGanerator; //ノーツジェネレーター

    // 判定の番号の列挙型
    enum JudgeNumber
    {
        Perfect = 0,
        Great   = 1,
        Bad     = 2,
        Miss    = 3,
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))//〇キーが押されたとき
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Red))//押されたボタンはレーンの番号とあっているか？
            {
                /*
                本来ノーツをたたく時間と実際にたたいた時間がどれくらいずれているかを求め、
                その絶対値をJudgement関数に送る
                たたくノーツが常にList内の一番初めにあるため参照するindexは0
                */
                Judgement(GetABS(Time.time - notesGanerator.GetNotesTie(0)));                
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Green))
            {
                Judgement(GetABS(Time.time - notesGanerator.GetNotesTie(0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Blue))
            {
                Judgement(GetABS(Time.time - notesGanerator.GetNotesTie(0)));
            }
        }

        if (Time.time > notesGanerator.GetNotesTie(0) + 0.2f)//本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        {
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(0),(int)JudgeNumber.Miss);
            notesGanerator.DeleteNoteData(0); // ノーツデータの削除命令
            Debug.Log("Miss");
            //ミス
        }
    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            Debug.Log("Perfect");
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(0), (int)JudgeNumber.Perfect);
            notesGanerator.DeleteNoteData(0);
        }
        else
        {
            if (timeLag <= 0.15f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
            {
                Debug.Log("Great");
                uiController.DisplayJudge(notesGanerator.GetLaneNum(0), (int)JudgeNumber.Great);
                notesGanerator.DeleteNoteData(0);
            }
            else
            {
                if (timeLag <= 0.20f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
                {
                    Debug.Log("Bad");
                    uiController.DisplayJudge(notesGanerator.GetLaneNum(0), (int)JudgeNumber.Bad);
                    notesGanerator.DeleteNoteData(0);
                }
            }
        }
    }
    float GetABS(float num)//引数の絶対値を返す関数
    {
        if (num >= 0)
        {
            return num;
        }
        else
        {
            return -num;
        }
    }
}
