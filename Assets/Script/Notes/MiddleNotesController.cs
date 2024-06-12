using UnityEngine;

public class MiddleNotesController : MonoBehaviour
{
    float notesSpeed;
    bool isGameStart = false;
    bool isHold;         // ホールド中のフラグ
    float holdStartTime; // ホールド開始時間
    float holdEndTime;   // ホールド終了時の時間
    float gameStartTIme; // ゲーム開始時間

    void Update()
    {
        if (isGameStart)
        {
            UpdatePos(); // 座標更新

            // ホールド開始フラグの判定
            if (holdStartTime + gameStartTIme < Time.time && !isHold)
            {
                isHold = true; // ホールドフラグを立てる
            }

            // ホールド終了フラグの判定
            if(holdEndTime + gameStartTIme < Time.time)
            {
                isHold = false; // ホールドフラグを下ろす
            }
        }

        if (isHold)
        {
            ScaleUpdate(); // サイズ更新
        }
    }

    void UpdatePos()
    {
        transform.position -= transform.forward * notesSpeed * Time.deltaTime;
    }

    /// <summary>
    /// サイズ変更更新
    /// </summary>
    void ScaleUpdate()
    {
        float middleNotesEndPosZ = (holdEndTime + gameStartTIme - Time.time) * notesSpeed; // 現在のミドルノーツの終点座標計算
        float middleNotesCenterPosZ = middleNotesEndPosZ / 2;     // 現在のミドルノーツの中心座標Zの計算
        transform.localScale = new Vector3(1, 1, middleNotesEndPosZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, middleNotesCenterPosZ);
    }

    public void SetMiddoleNotesTime(float setStartTime, float setEndTime)
    {
        holdStartTime = setStartTime;
        holdEndTime = setEndTime;
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
        //Debug.Log("スピード設定");
    }

    public void SetIsGameStart()
    {
        isGameStart = true;
        gameStartTIme = Time.time; // 開始時間設定
        //Debug.Log("スタートフラグ");
    }
}
