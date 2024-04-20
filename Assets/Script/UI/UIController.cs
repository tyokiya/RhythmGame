using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    /// <summary>
    /// 判定結果の表示
    /// </summary>
    /// <param name="displayLaneNum">表示するレーンの番号</param>
    /// <param name="judge">判定結果</param>
    public void JudgeDisplay(int displayLaneNum,int judge)
    {
        switch (judge)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
