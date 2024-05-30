using UnityEngine;

public class MiddleNotesController : MonoBehaviour
{
    protected float notesSpeed;
    protected bool isGameStart = false;
    bool isHold; // ホールド中のフラグ

    void Update()
    {
        if (isGameStart)
        {
            UpdatePos(); // 座標更新
        }
        if (isHold)
        {
            ScaleUpdate(); // サイズ更新
        }
    }

    void UpdatePos()
    {
        transform.position -= transform.forward * Time.deltaTime * notesSpeed;
    }

    void ScaleUpdate()
    {
        transform.localScale = new Vector3(1, 1, transform.localScale.z - (notesSpeed / 2)); // スケール変更
        transform.position += transform.forward * notesSpeed / 2 * Time.deltaTime; // サイズ変更分の座標調整
    }

    public void SetStartHold()
    {
        isHold = true;
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
}
