using UnityEngine;

/// <summary>
/// ノーツのコントローラークラス
/// </summary>
public class NotesController : MonoBehaviour
{
    float notesSpeed;
    bool isGameStart = false;

    void Update()
    {
        if (isGameStart)
        {
            UpdatePos(); // 座標更新
        }        
    }

    void UpdatePos()
    {
        transform.position -= transform.forward * Time.deltaTime * notesSpeed;
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
        //Debug.Log("スピード設定");
    }

    public void SetIsGameStart()
    {
        isGameStart = true;
        //Debug.Log("スタートフラグ");
    }

    /// <summary>
    /// テストモードで必要なデータの設定(テストモードで使用)
    /// </summary>
    /// <param name="startTime">楽曲再生開始時間</param>
    public void SetTestMode(float startTime)
    {
        Vector3 pos = this.transform.position; // ノーツオブジェクトのtransform取得
        pos.z -= startTime * notesSpeed;       // スタート開始時間分の移動計算
        this.transform.position = pos;         // 計算した座標を反映
    }
}
