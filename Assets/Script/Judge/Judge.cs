using UnityEngine;
using static LaneController;
using static NoteGenerator;

/// <summary>
/// 判定クラス
/// </summary>
public class Judge : MonoBehaviour
{
    //変数の宣言
    [SerializeField] UIController    uiController;    // UIコントローラー
    [SerializeField] NoteGenerator   notesGanerator;  // ノーツジェネレーター
    [SerializeField] SoundController soundController; // サウンドコントローラー
    [SerializeField] ScoreCountor    scoreCountor;    //スコアコントローラー

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
    public enum JudgeNumber
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
            TapUpdate();  // ノーマルノーツのタップ判定            
            HoldUpdate(); // ホールド判定            
            UpUpdate();   // ホールドを離した判定            
            MissUpdate(); // ミス判定
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
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Red))//押されたボタンはレーンの番号とあっているか？
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && !isGreenLaneHold)
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && !isBlueLaneHold)
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Blue))
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
        if (Input.GetKey(KeyCode.J) && isRedLaneHold) // 押せているとき
        {
            //Debug.Log("赤ホールド中");
            scoreCountor.UpdateScore(NotesType.LongNote);             // 判定結果の表示命令
            notesGanerator.CallLuminescence(true, LaneColor.Red); // 発光処理の呼び出し
        }
        else if (isRedLaneHold)                         // 押せてないとき
        {
            notesGanerator.CallLuminescence(false, LaneColor.Red); // 発光処理終了の呼び出し
        }

        if (Input.GetKey(KeyCode.J) && isGreenLaneHold)
        {
            //Debug.Log("緑ホールド中");
            notesGanerator.CallLuminescence(true, LaneColor.Green);
        }
        else if (isGreenLaneHold)
        {
            notesGanerator.CallLuminescence(false, LaneColor.Green);
        }

        if (Input.GetKey(KeyCode.J) && isBlueLaneHold)
        {
            //Debug.Log("青ホールド中");
            notesGanerator.CallLuminescence(true, LaneColor.Blue);
        }
        else if (isBlueLaneHold)
        {
            notesGanerator.CallLuminescence(false, LaneColor.Blue);
        }
    }

    /// <summary>
    /// 離し判定更新
    /// </summary>
    void UpUpdate()
    {
        if (Input.GetKeyUp(KeyCode.F) && isRedLaneHold) // 赤レーン
        {
            // 3レーン分判定
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneColor.Red);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneColor.Red);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneColor.Red);
            }
        }
        if (Input.GetKeyUp(KeyCode.J) && isGreenLaneHold) // 緑レーン
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneColor.Green);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneColor.Green);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneColor.Green);
            }
        }
        if (Input.GetKeyUp(KeyCode.K) && isBlueLaneHold) // 青レーン
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneColor.Blue);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneColor.Blue);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneColor.Blue);
            }
        }
    }

    /// <summary>
    /// ミス判定更新
    /// </summary>
    void MissUpdate()
    {
        // 本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        if (Time.time > notesGanerator.GetNotesTieme(0) + gameStartTIme + BadTime)
        {
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneColor(0), (int)JudgeNumber.Miss);
            // スコア更新呼び出し
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Miss);
            // Debug.Log("Miss");

            // ミスノーツの種類判定
            // ロングノーツの場合のみ判定
            if (notesGanerator.GetNotesType(0) == ((int)NotesType.LongNote))
            {
                // すでにホールド中かホールド前の始点ノーツかで判定を変える
                switch(notesGanerator.GetLaneColor(0))
                {
                    case 0:
                        if(isRedLaneHold)
                        {
                            // すでにホールド中だった場合はフラグを下ろす
                            SetMiddleNotesFlg(((int)LaneColor.Red), false);
                            // 中間ノーツデータ削除
                            notesGanerator.DeleteMiddleNoteData(LaneColor.Red);
                        }
                        else
                        {
                            // ホールド中でなければフラグを立てる
                            SetMiddleNotesFlg((int)LaneColor.Red, true);
                        }
                        break;
                    case 1:
                        if (isGreenLaneHold)
                        {
                            SetMiddleNotesFlg(((int)LaneColor.Green), false);
                            notesGanerator.DeleteMiddleNoteData(LaneColor.Green);
                        }
                        else if(!isGreenLaneHold)
                        {
                            SetMiddleNotesFlg(((int)LaneColor.Green), true);
                        }
                        break;
                    case 2:
                        if (isBlueLaneHold)
                        {
                            SetMiddleNotesFlg(((int)LaneColor.Blue), false);
                            notesGanerator.DeleteMiddleNoteData(LaneColor.Blue);
                        }
                        else
                        {
                            SetMiddleNotesFlg(((int)LaneColor.Blue), true);
                        }
                        break;
                    default:
                        break;
                }
            }

            notesGanerator.DeleteNormalNoteData(0); // ノーマルノーツデータの削除命令
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
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Perfect); // 判定結果の表示命令     
            soundController.PlySE(SoundController.SEList.tapSE);                                       　                // タップ音再生
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Perfect);                                // スコア更新呼び出し                                // スコア更新呼び出し
            // ロングノーツの時はホールドフラグを立てる
            if(notesGanerator.GetNotesType(indexNum) == ((int)NotesType.LongNote))
            {
                SetMiddleNotesFlg(notesGanerator.GetLaneColor(indexNum),true); // ロングノーツの入力中フラグを立てる
            }
            notesGanerator.DeleteNormalNoteData(indexNum);                                         // ノーツデータ削除命令
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Great);            
            soundController.PlySE(SoundController.SEList.tapSE);
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Great);
            if (notesGanerator.GetNotesType(indexNum) == ((int)NotesType.LongNote))
            {
                SetMiddleNotesFlg(notesGanerator.GetLaneColor(indexNum),true);
            }
            notesGanerator.DeleteNormalNoteData(indexNum);
        }
        else if (timeLag <= BadTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            // Debug.Log("Bad");
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Bad);           
            soundController.PlySE(SoundController.SEList.tapSE);
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Bad);
            if (notesGanerator.GetNotesType(indexNum) == ((int)NotesType.LongNote))
            {
                SetMiddleNotesFlg(notesGanerator.GetLaneColor(indexNum), true);
            }
            notesGanerator.DeleteNormalNoteData(indexNum);
        }
    }

    /// <summary>
    /// 離しの判定
    /// </summary>
    /// <param name="timeLag">実際のタップ時間との差</param>
    /// <param name="indexNum">参照する要素数</param>
    void UpJudgement(float timeLag, int indexNum, LaneColor laneColor)
    {
        if (timeLag <= PerfectTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            // Debug.Log("Perfect");
            uiController.DisplayJudge(((int)laneColor), (int)JudgeNumber.Perfect); // 判定結果の表示命令
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Perfect); // スコア更新呼び出し 
            notesGanerator.DeleteNormalNoteData(indexNum);                                // ノーマルノーツの削除命令
            notesGanerator.DeleteMiddleNoteData(laneColor);                                     // ミドルノーツ削除命令                               
            soundController.PlySE(SoundController.SEList.tapSE);                          // サウンド再生                                                                 
            SetMiddleNotesFlg(((int)laneColor), false);                         // フラグを下ろす
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(((int)laneColor), (int)JudgeNumber.Great);
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Great);
            notesGanerator.DeleteNormalNoteData(indexNum);
            notesGanerator.DeleteMiddleNoteData(laneColor);
            soundController.PlySE(SoundController.SEList.tapSE);
            SetMiddleNotesFlg(((int)laneColor), false);          
        }
        else if (timeLag <= BadTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            // Debug.Log("Bad");
            uiController.DisplayJudge(((int)laneColor), (int)JudgeNumber.Bad);
            scoreCountor.UpdateScore(NotesType.NormalNote, JudgeNumber.Bad);
            notesGanerator.DeleteNormalNoteData(indexNum);
            notesGanerator.DeleteMiddleNoteData(laneColor);
            soundController.PlySE(SoundController.SEList.tapSE);
            SetMiddleNotesFlg(((int)laneColor), false);
        }
    }

    /// <summary>
    /// 引数の絶対値を返す関数
    /// </summary>
    float GetABS(float num)
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
    /// ロングノーツフラグをセット
    /// </summary>
    /// <param name="laneColor">レーン数</param>
    /// <param name="setFlg">セットするフラグ</param>
    void SetMiddleNotesFlg(int laneColor,bool setFlg)
    {
        //Debug.Log("ロングフラグ切り替え");
        switch (laneColor)
        {
            case ((int)LaneColor.Red):
                isRedLaneHold = setFlg; // ホールドフラグをセット
                break;
            case ((int)LaneColor.Green):
                isGreenLaneHold = setFlg;
                break;
            case ((int)LaneColor.Blue):
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
