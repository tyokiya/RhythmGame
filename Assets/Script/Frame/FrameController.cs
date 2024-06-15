using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    int nowTortalFPS = 0; // 現在のトータルfps値(毎フレーム加算される)

    void Start()
    {
        // フレーム固定
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // フレーム加算
        nowTortalFPS++;

        // オーバーフロー対策
        if(nowTortalFPS >= int.MaxValue)
        {
            nowTortalFPS = 0;
        }
    }

    /// <summary>
    /// 現在のトータルfps値のゲッター
    /// </summary>
    /// <returns></returns>
    public int GetNowTortalFPS()
    {
        return nowTortalFPS;
    }
}
