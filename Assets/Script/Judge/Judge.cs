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
    float gameStartTIme;        // ゲーム開始時の時間
    bool isGameStart = false;   // ゲーム開始フラグ
    // 各レーンのホールドノーツ中のフラグ
    bool isRedLaneHold = false;
    bool isGreenLaneHold = false;
    bool isBlueLaneHold = false;

    // 判定の秒数定数
    const float PerfectTime = 0.1f;
    const float GreatTime = 0.15f;
    const float BadTime = 0.2f;
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
            // ノーマルノーツのタップ判定
            TapUpdate();

            // ホールド判定
            HoldUpdate();

            // ホールドを離した判定
            UpUpdate();

            // ミス判定
            MissUpdate();
        }
    }

    /// <summary>
    /// タップ判定更新
    /// </summary>
    void TapUpdate()
    {        
        if (Input.GetKeyDown(KeyCode.F) && !isRedLaneHold)//〇キーが押されたとき
        {
            /*
            本来ノーツをたたく時間と実際にたたいた時間がどれくらいずれているかを求め、その絶対値をJudgement関数に送る            
            たたくノーツが常にList内の一番初めにあるため参照するindexは0、3レーンあるため上から0.1.2
            */
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Red))//押されたボタンはレーンの番号とあっているか？
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && !isGreenLaneHold)
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && !isBlueLaneHold)
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
    }

    /// <summary>
    /// ホールド判定更新
    /// </summary>
    void HoldUpdate()
    {
        if (Input.GetKey(KeyCode.J) && !isGreenLaneHold)
        {
            Debug.Log("緑ホールド中");
        }
        if (Input.GetKey(KeyCode.J) && !isGreenLaneHold)
        {
            Debug.Log("緑ホールド中");
        }
        if (Input.GetKey(KeyCode.J) && !isGreenLaneHold)
        {
            Debug.Log("緑ホールド中");
        }
    }

    /// <summary>
    /// 離し判定更新
    /// </summary>
    void UpUpdate()
    {
        if (Input.GetKeyUp(KeyCode.J) && isRedLaneHold) // 赤レーン
        {
            // 判定
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }

            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false); // ホールドフラグを下す
        }
        if (Input.GetKeyUp(KeyCode.J) && isGreenLaneHold) // 緑レーン
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }

            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false); 
        }
        if (Input.GetKeyUp(KeyCode.J) && isBlueLaneHold) // 青レーン
        {
            if (notesGanerator.GetLaneNum(0) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneNum(1) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneNum(2) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }

            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false); 
        }
    }

    void MissUpdate()
    {
        // 本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        if (Time.time > notesGanerator.GetNotesTieme(0) + gameStartTIme + BadTime)
        {
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(0), (int)JudgeNumber.Miss);
            // Debug.Log("Miss");

            // ミスノーツの種類判定
            // ロングノーツの場合のみ判定
            if(notesGanerator.GetNotesType(0) == ((int)NoteGenerator.NotesType.LongNote))
            {
                // すでにホールド中かホールド前の始点ノーツかで判定を変える
                switch(notesGanerator.GetLaneNum(0))
                {
                    case 0:
                        if(isRedLaneHold)
                        {
                            // すでにホールド中だった場合はフラグを下ろす
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false);
                        }
                        else
                        {
                            // ホールド中でなければフラグを立てる
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), true);
                        }
                        break;
                    case 1:
                        if (isGreenLaneHold)
                        {
                            // すでにホールド中だった場合はフラグを下ろす
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false);
                            Debug.Log("成功");
                        }
                        else if(!isGreenLaneHold)
                        {
                            // ホールド中でなければフラグを立てる
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), true);
                        }
                        break;
                    case 2:
                        if (isRedLaneHold)
                        {
                            // すでにホールド中だった場合はフラグを下ろす
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), false);
                        }
                        else
                        {
                            // ホールド中でなければフラグを立てる
                            SetLongNotesFlg(notesGanerator.GetLaneNum(0), true);
                        }
                        break;
                    default:
                        break;
                }
            }

            notesGanerator.DeleteNoteData(0); // ノーツデータの削除命令
        }
    }

    /// <summary>
    /// タップの判定
    /// </summary>
    /// <param name="timeLag">実際のタップ時間との差</param>
    /// <param name="indexNum">参照する要素数</param>
    void TapJudgement(float timeLag, int indexNum)
    {
        if (timeLag <= PerfectTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            // Debug.Log("Perfect");
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Perfect);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);

            // ロングノーツの時はホールドフラグを立てる
            if(notesGanerator.GetNotesType(0) == ((int)NoteGenerator.NotesType.LongNote))
            {
                SetLongNotesFlg(notesGanerator.GetLaneNum(0),true);
            }
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Great);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);

            if (notesGanerator.GetNotesType(0) == ((int)NoteGenerator.NotesType.LongNote))
            {
                SetLongNotesFlg(notesGanerator.GetLaneNum(0),true);
            }
        }
        else if (timeLag <= BadTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            // Debug.Log("Bad");
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Bad);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);

            if (notesGanerator.GetNotesType(0) == ((int)NoteGenerator.NotesType.LongNote))
            {
                SetLongNotesFlg(notesGanerator.GetLaneNum(0), true);
            }
        }
    }

    /// <summary>
    /// 離しの判定
    /// </summary>
    /// <param name="timeLag">実際のタップ時間との差</param>
    /// <param name="indexNum">参照する要素数</param>
    void UpJudgement(float timeLag, int indexNum)
    {
        if (timeLag <= PerfectTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            // Debug.Log("Perfect");
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Perfect);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneNum(indexNum), (int)JudgeNumber.Great);
            notesGanerator.DeleteNoteData(indexNum);
            soundController.PlySE(SoundController.SEList.tapSE);
        }
        else if (timeLag <= BadTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
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

    /// <summary>
    /// ロングノーツフラグを立てる
    /// </summary>
    /// <param name="laneNum">レーン数</param>
    /// <param name="setFlg">セットするフラグ</param>
    void SetLongNotesFlg(int laneNum,bool setFlg)
    {
        Debug.Log("ロングフラグ切り替え");
        switch (laneNum)
        {
            case 0:
                isRedLaneHold = setFlg; // ホールドフラグを立てる
                break;

            case 1:
                isGreenLaneHold = setFlg;
                break;

            case 2:
                isBlueLaneHold = setFlg;
                break;
            default:
                break;
        }
    }

    public void SetGameStart()
    {
        gameStartTIme = Time.time; // ゲーム開発時間をセット
        isGameStart = true;        // ゲーム開始フラグを立てる
    }
}
