using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 譜面データ(json)のクラス
/// </summary>
[Serializable]
class InputJsonData
{
    public string name;     // 曲名
    public int maxBlock; // 最大数
    public int BPM;
    public int offset;
    public Notes[] notes;
}

/// <summary>
/// ノーツクラス
/// </summary>
[Serializable]
public class Notes
{
    public int type;  // 種類
    public int num;   // ノーツエディター上での縦線の位置
    public int block; // レーンの位置
    public int LPB;   // 1拍を何分割してノーツを置くか
}

/// <summary>
/// ノーツ生成クラス
/// </summary>
public class NoteGenerator : MonoBehaviour
{
    int notesNum;    // 総ノーツ数

    List<int> lanesNum = new List<int>();   // 降ってくるレーンの番号
    List<int> notesType = new List<int>();   // 降ってくるノーツの種類番号
    List<float> notesTime = new List<float>(); // ノーツが判定線と重なる時間
    List<GameObject> notesObject = new List<GameObject>(); //ノーツオブジェクト

    [SerializeField] List<GameObject> noteObject;    // ノーツプレハブ
    [SerializeField] Transform createNotesTransform; // 生成したノーツを入れるtransform

    /// <summary>
    /// ノーツの種類の列挙型
    /// </summary>
    enum NotesType
    {
        NormalNote = 1,
        LongNote = 2
    }

    float notesSpeed;
    const float frame = 60.0f;

    void OnEnable()
    {
        notesNum = 0;        // 総ノーツ数初期化
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
    }


    /// <summary>
    /// 譜面データの読み込み
    /// </summary>
    /// <param name="songName">曲名</param>
    public void LoadMusicalScoreData(SoundController.MusicNameList songName)
    {
        // 曲名取得
        string inputSongName = Resources.Load<TextAsset>(songName.ToString()).ToString();
        // 曲名からjsonファイル読み込み
        InputJsonData inputJson = JsonUtility.FromJson<InputJsonData>(inputSongName);

        notesNum = inputJson.notes.Length; // 総ノーツ数設定

        for (int i = 0;i < inputJson.notes.Length;i++)
        {
            // ノーツの生成時間設定
            float distance = frame / (inputJson.BPM * inputJson.notes[i].LPB);
            float beatSec  = distance * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            // ノーツ情報をリストに追加
            notesTime.Add(time);
            lanesNum.Add(inputJson.notes[i].block);
            notesType.Add(inputJson.notes[i].type);

            // ノーツ生成
            CreateNote(inputJson.notes[i], time);
            notesObject[i].GetComponent<NotesController>().SetNotesSpeed(notesSpeed);
        }
    }

    /// <summary>
    /// ノーツ生成
    /// </summary>
    /// <param name="notesData">ノーツデータ</param>
    void CreateNote(Notes notesData,float time)
    {
        // 生成座標計算
        float z = time * notesSpeed ;
        //Debug.Log("ノーツタイプ "+ notesData.type);
        // レーンに合わせた座標
        switch (notesData.block)
        {
            case 0:
                if(notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(-1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if(notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(-1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
            case 1:
                if (notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(0, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if (notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(0, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
            case 2:
                if (notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if (notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// レーン番号を変えす
    /// </summary>
    /// <param name="laneIndex">返すlanesNumのインデックス</param>
    public int GetLaneNum(int laneIndex)
    {
        return lanesNum[laneIndex];
    }

    /// <summary>
    /// ノーツの生成時間を返す
    /// </summary>
    /// <param name="Index">返すnoteTimeのインデックス</param>
    /// <returns></returns>
    public float GetNotesTie(int Index)
    {
        return notesTime[Index];
    }

    /// <summary>
    /// ノーツデータの削除
    /// </summary>
    /// <param name="index">削除するデータのインデックス</param>
    public void DeleteNoteData(int index)
    {
        notesTime.RemoveAt(index);
        notesType.RemoveAt(index);
        lanesNum.RemoveAt(index);
    }
}
