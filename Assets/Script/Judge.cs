using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 判定クラス
/// </summary>
public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] UIController UIController;  // UIコントローラー
    [SerializeField] NoteGenerator notesManager; // ノーツマネージャー
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))//〇キーが押されたとき
        {
            if (notesManager.GetLaneNum(0) == ((int)LaneController.Lane.Red))//押されたボタンはレーンの番号とあっているか？
            {
                /*
                本来ノーツをたたく時間と実際にたたいた時間がどれくらいずれているかを求め、
                その絶対値をJudgement関数に送る
                たたくノーツが常にList内の一番初めにあるため参照するindexは0
                */
                Judgement(GetABS(Time.time - notesManager.GetNotesTie(0)));                
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (notesManager.GetLaneNum(0) == ((int)LaneController.Lane.Green))
            {
                Judgement(GetABS(Time.time - notesManager.GetNotesTie(0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (notesManager.GetLaneNum(0) == ((int)LaneController.Lane.Blue))
            {
                Judgement(GetABS(Time.time - notesManager.GetNotesTie(0)));
            }
        }

        if (Time.time > notesManager.GetNotesTie(0) + 0.2f)//本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        {
            
            notesManager.DeleteNoteData(0); // ノーツデータの削除命令
            Debug.Log("Miss");
            //ミス
        }
    }
    void Judgement(float timeLag)
    {
        if (timeLag <= 0.10f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            Debug.Log("Perfect");
            
            notesManager.DeleteNoteData(0);
        }
        else
        {
            if (timeLag <= 0.15f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
            {
                Debug.Log("Great");
                
                notesManager.DeleteNoteData(0);
            }
            else
            {
                if (timeLag <= 0.20f)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
                {
                    Debug.Log("Bad");
                    
                    notesManager.DeleteNoteData(0);
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
