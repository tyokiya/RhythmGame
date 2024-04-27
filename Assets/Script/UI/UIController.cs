using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// UIのコントローラークラス
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField] List<GameObject> JugeText; // 判定結果を表示するオブジェクトのリスト
    [SerializeField] Transform CreatJudgeTextTransform; // 生成した判定テキストを入れるtransform
    // 定数
    const float JugeTextEulerX = 30.0f;
    const float JugeTextDestroyDelay = 1.0f; // 判定テキストの削除待機時間

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
            createObj = Instantiate(JugeText[judge], new Vector3(LaneController.RedLanePosX,1.1f,0),Quaternion.Euler(JugeTextEulerX, 0,0),CreatJudgeTextTransform);
            Debug.Log("判定生成");
        }
        else if (laneNum == (int)LaneController.LaneColor.Green) // 緑レーン
        {
            createObj = Instantiate(JugeText[judge], new Vector3(LaneController.GreenLanePosX, 1.1f, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), CreatJudgeTextTransform);
        }
        else                                                     // 青レーン
        {
            createObj = Instantiate(JugeText[judge], new Vector3(LaneController.BlueLanePosX, 1.1f, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), CreatJudgeTextTransform);
        }

        // JugeTextDeleteDelay秒待機後オブジェクト削除
        await UniTask.Delay(TimeSpan.FromSeconds(JugeTextDestroyDelay));
        Destroy(createObj);

    }
}
