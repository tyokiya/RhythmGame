using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using TMPro;

/// <summary>
/// UIのコントローラークラス
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject>      jugeText;                // 判定結果を表示するオブジェクトのリスト
    [SerializeField] Transform             creatJudgeTextTransform; // 生成した判定テキストを入れるtransform
    [SerializeField] List<TextMeshProUGUI> judgeCountTextList;      // 判定数のテキストリスト
    [SerializeField] TextMeshProUGUI       scoreText;               // スコアテキスト
    [SerializeField] ScoreCountor          scoreController;         // スコアコントローラー
    [SerializeField] GameObject            readyLogo;               // ゲームスタート前のロゴ

    // 定数
    const float JugeTextEulerX = 30.0f;
    const float JugeTextDestroyDelay = 1.0f; // 判定テキストの削除待機時間

    // 判定番号
    enum JudgeNum
    {
        Perfect = 0,
        Greate,
        Bad,
        Miss
    }

    void Update()
    {        
        DrawJudgeCount(); // 判定数描画
        DrawScore();      // スコア描画
    }

    /// <summary>
    /// 判定結果の表示
    /// </summary>
    /// <param name="laneNum">表示するレーン</param>
    /// <param name="judge">判定結果</param>
    public async void DisplayJudge(int laneNum,int judge)
    {
        GameObject createObj;

        // レーンに応じてテキスト生成
        if (laneNum == (int)LaneController.LaneColor.Red)        // 赤レーン
        { 
            createObj = Instantiate(jugeText[judge], new Vector3(LaneController.RedLanePosX,1.1f,0),Quaternion.Euler(JugeTextEulerX, 0,0),creatJudgeTextTransform);
        }
        else if (laneNum == (int)LaneController.LaneColor.Green) // 緑レーン
        {
            createObj = Instantiate(jugeText[judge], new Vector3(LaneController.GreenLanePosX, 1.1f, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), creatJudgeTextTransform);
        }
        else                                                     // 青レーン
        {
            createObj = Instantiate(jugeText[judge], new Vector3(LaneController.BlueLanePosX, 1.1f, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), creatJudgeTextTransform);
        }

        // JugeTextDeleteDelay秒待機後オブジェクト削除
        await UniTask.Delay(TimeSpan.FromSeconds(JugeTextDestroyDelay));
        Destroy(createObj);
    }

    void DrawJudgeCount()
    {
        // パーフェクト数
        judgeCountTextList[((int)JudgeNum.Perfect)].text = scoreController.GetPerfectNum().ToString();
        // グレート
        judgeCountTextList[((int)JudgeNum.Greate)].text = scoreController.GetGreatNum().ToString();
        // バッド
        judgeCountTextList[((int)JudgeNum.Bad)].text = scoreController.GetBadNum().ToString();
        // ミス
        judgeCountTextList[((int)JudgeNum.Miss)].text = scoreController.GetMissNum().ToString();
    }

    void DrawScore()
    {
        scoreText.text = scoreController.GetScore().ToString();
    }

    /// <summary>
    /// ゲームスタート時に不要なオブジェクトの非アクティブ化
    /// </summary>
    public void ActiveOffGameStartObject()
    {
        SetActiveRedyLogo(false); // ゲームスタート前のロゴ
    }

    /// <summary>
    /// ゲームスタート前のロゴのアクティブ切り替え
    /// </summary>
    /// <param name="setFlg">セットするフラグ</param>
    void SetActiveRedyLogo(bool setFlg)
    {
        readyLogo.SetActive(setFlg);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        readyLogo.SetActive(true); // ゲームスタート前のロゴアクティブ化

    }
}
