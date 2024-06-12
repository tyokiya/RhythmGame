using UnityEngine;

/// <summary>
/// ノーツデータクラス
/// </summary>
public class NoteData : MonoBehaviour
{
    public int laneNum;   // 降ってくるレーンの番号
    public int typeNum;      // 降ってくるノーツの種類番号
    public float time;    // ノーツが判定線と重なる時間
    public GameObject noteObject; // 生成したオブジェクトを入れる

    /// <summary>
    /// ノーツデータのセット
    /// </summary>
    public void SetNotesData(float notesTime, int laneNum, int notesType)
    {
        this.time = notesTime;
        this.laneNum = laneNum;
        this.typeNum = notesType;
    }

    /// <summary>
    /// 生成したノーツプレハブを入れる
    /// </summary>
    public void SetNotesObject(GameObject createObject)
    {
        noteObject = createObject;
    }

    public void DeleteNotes()
    {
        Destroy(noteObject);
    }
}
