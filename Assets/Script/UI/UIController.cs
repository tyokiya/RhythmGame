using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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

    /// <summary>
    /// 判定結果の表示
    /// </summary>
    /// <param name="laneNum">表示するレーン</param>
    /// <param name="judge">判定結果</param>
    public void DisplayJudge(int laneNum,int judge)
    {
        // レーンに応じてテキスト生成
        if (laneNum == (int)LaneController.LaneColor.Red)        // 赤レーン
        { 
            Instantiate(JugeText[judge], new Vector3(LaneController.RedLanePosX,1.1f,0),Quaternion.Euler(JugeTextEulerX, 0,0),CreatJudgeTextTransform);
        }
        else if (laneNum == (int)LaneController.LaneColor.Green) // 緑レーン
        {
            Instantiate(JugeText[judge], new Vector3(LaneController.GreenLanePosX, 0, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), CreatJudgeTextTransform);
        }
        else                                                     // 青レーン
        {
            Instantiate(JugeText[judge], new Vector3(LaneController.BlueLanePosX, 0, 0), Quaternion.Euler(JugeTextEulerX, 0, 0), CreatJudgeTextTransform);
        }        
    }
}
