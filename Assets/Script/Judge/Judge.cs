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
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Red))//押されたボタンはレーンの番号とあっているか？
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Red))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.J) && !isGreenLaneHold)
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Green))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2);
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && !isBlueLaneHold)
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Blue))
            {
                TapJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Blue))
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
        if (Input.GetKey(KeyCode.J) && !isRedLaneHold)
        {
            Debug.Log("赤ホールド中");
        }
        if (Input.GetKey(KeyCode.J) && !isGreenLaneHold)
        {
            Debug.Log("緑ホールド中");
        }
        if (Input.GetKey(KeyCode.J) && !isBlueLaneHold)
        {
            Debug.Log("青ホールド中");
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
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneController.LaneColor.Red);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneController.LaneColor.Red);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Red))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneController.LaneColor.Red);
            }

            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false); // ホールドフラグを下す
        }
        if (Input.GetKeyUp(KeyCode.J) && isGreenLaneHold) // 緑レーン
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneController.LaneColor.Green);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneController.LaneColor.Green);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Green))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneController.LaneColor.Green);
            }

            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false); 
        }
        if (Input.GetKeyUp(KeyCode.K) && isBlueLaneHold) // 青レーン
        {
            if (notesGanerator.GetLaneColor(0) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(0) - gameStartTIme), 0, LaneController.LaneColor.Blue);
            }
            else if (notesGanerator.GetLaneColor(1) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(1) - gameStartTIme), 1, LaneController.LaneColor.Blue);
            }
            else if (notesGanerator.GetLaneColor(2) == ((int)LaneController.LaneColor.Blue))
            {
                UpJudgement(GetABS(Time.time - notesGanerator.GetNotesTieme(2) - gameStartTIme), 2, LaneController.LaneColor.Blue);
            }

            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false); 
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
            // Debug.Log("Miss");

            // ミスノーツの種類判定
            // ロングノーツの場合のみ判定
            if(notesGanerator.GetNotesType(0) == ((int)NoteGenerator.NotesType.LongNote))
            {
                // すでにホールド中かホールド前の始点ノーツかで判定を変える
                switch(notesGanerator.GetLaneColor(0))
                {
                    case 0:
                        if(isRedLaneHold)
                        {
                            // すでにホールド中だった場合はフラグを下ろす
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false);
                            // 中間ノーツデータ削除
                            notesGanerator.DeleteMiddleNoteData(LaneController.LaneColor.Red);
                        }
                        else
                        {
                            // ホールド中でなければフラグを立てる
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), true);
                        }
                        break;
                    case 1:
                        if (isGreenLaneHold)
                        {
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false);
                            notesGanerator.DeleteMiddleNoteData(LaneController.LaneColor.Green);
                        }
                        else if(!isGreenLaneHold)
                        {
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), true);
                        }
                        break;
                    case 2:
                        if (isBlueLaneHold)
                        {
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), false);
                            notesGanerator.DeleteMiddleNoteData(LaneController.LaneColor.Blue);
                        }
                        else
                        {
                            SetMiddleNotesFlg(notesGanerator.GetLaneColor(0), true);
                        }
                        break;
                    default:
                        break;
                }
            }

            notesGanerator.DeleteNormalNoteData(0); // ノーツデータの削除命令
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
            soundController.PlySE(SoundController.SEList.tapSE);                                      // タップ音再生
            // ロングノーツの時はホールドフラグを立てる
            if(notesGanerator.GetNotesType(indexNum) == ((int)NoteGenerator.NotesType.LongNote))
            {
                SetMiddleNotesFlg(notesGanerator.GetLaneColor(indexNum),true);  　　　　　　　　　　　　　// ロングノーツの入力中フラグを立てる
            }
            notesGanerator.DeleteNormalNoteData(indexNum);                                                  // ノーツデータ削除命令
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Great);            
            soundController.PlySE(SoundController.SEList.tapSE);
            if (notesGanerator.GetNotesType(indexNum) == ((int)NoteGenerator.NotesType.LongNote))
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
            if (notesGanerator.GetNotesType(indexNum) == ((int)NoteGenerator.NotesType.LongNote))
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
    void UpJudgement(float timeLag, int indexNum, LaneController.LaneColor laneColor)
    {
        if (timeLag <= PerfectTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.1秒以下だったら
        {
            // Debug.Log("Perfect");
            // 判定結果の表示命令
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Perfect); 
            notesGanerator.DeleteNormalNoteData(indexNum);       // ノーマルノーツの削除命令
            notesGanerator.DeleteMiddleNoteData(laneColor);      // ミドルノーツ削除命令
            soundController.PlySE(SoundController.SEList.tapSE); // サウンド再生
        }
        else if (timeLag <= GreatTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.15秒以下だったら
        {
            // Debug.Log("Great");
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Great);
            notesGanerator.DeleteNormalNoteData(indexNum);
            notesGanerator.DeleteMiddleNoteData(laneColor);
            soundController.PlySE(SoundController.SEList.tapSE);
        }
        else if (timeLag <= BadTime)//本来ノーツをたたくべき時間と実際にノーツをたたいた時間の誤差が0.2秒以下だったら
        {
            // Debug.Log("Bad");
            uiController.DisplayJudge(notesGanerator.GetLaneColor(indexNum), (int)JudgeNumber.Bad);
            notesGanerator.DeleteNormalNoteData(indexNum);
            notesGanerator.DeleteMiddleNoteData(laneColor);
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
    /// <param name="laneColor">レーン数</param>
    /// <param name="setFlg">セットするフラグ</param>
    void SetMiddleNotesFlg(int laneColor,bool setFlg)
    {
        //Debug.Log("ロングフラグ切り替え");
        switch (laneColor)
        {
            case ((int)LaneController.LaneColor.Red):
                isRedLaneHold = setFlg; // ホールドフラグをセット
                break;
            case ((int)LaneController.LaneColor.Green):
                isGreenLaneHold = setFlg;
                break;
            case ((int)LaneController.LaneColor.Blue):
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
