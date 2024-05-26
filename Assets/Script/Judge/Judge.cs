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
    [SerializeField] SoundController soundController;
    float gameStartTIme; // ゲーム開始時の時間
    bool isGameStart = false;   // ゲーム開始フラグ

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
        if (isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.F))//〇キーが押されたとき
            {
                /*
                本来ノーツをたたく時間と実際にたたいた時間がどれくらいずれているかを求め、その絶対値をJudgement関数に送る            
                たたくノーツが常にList内の一番初めにあるため参照するindexは0、3レーンあるため上から0.1.2
                */
                if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Red))//押されたボタンはレーンの番号とあっているか？
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
                }
                else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Red))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
                }
                else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Red))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Green))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
                }
                else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Green))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
                }
                else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Green))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Blue))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
                }
                else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Blue))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
                }
                else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Blue))
                {
                    Judgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
                }
            }

            if (Time.time > notesGanerator.GetNotesTieme(0) + gameStartTIme + 0.2f)//本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
            {
                // 判定結果の表示命令
                uiController.DisplayJudge(notesGanerator.GetLaneNum(0), (int)JudgeNumber.Miss);
                notesGanerator.DeleteNoteData(0); // ノーツデータの削除命令
                                                  // Debug.Log("Miss");
                                                  //ミス
            }
        }
    }
    void Judgement(float timeLag, int indexNum)
    {
        if (timeLag <= 0.10f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            // Debug.Log("Perfect");
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Perfect);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);
        }
        else if (timeLag <= 0.15f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Great);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);
        }
        else if (timeLag <= 0.20f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            // Debug.Log("Bad");
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Bad);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);
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

    public void SetGameStart()
    {
        gameStartTIme = Time.time; // ゲーム開発時間をセット
        isGameStart = true;        // ゲーム開始フラグを立てる
    }
}
