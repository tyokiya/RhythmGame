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
    string songName; // 曲名
    
    List<int>   lanesNum  = new List<int>();   // 降ってくるレーンの番号
    List<int>   notesType = new List<int>();   // 降ってくるノーツの種類番号
    List<float> notesTime = new List<float>(); // ノーツが判定線と重なる時間
    List<GameObject> notesObject = new List<GameObject>(); //ノーツオブジェクト

    [SerializeField] const float NotesSpeed = 5.0f; // ノーツ速度
    [SerializeField] List<GameObject> normalNote;   // ノーマルノーツプレハブ
    [SerializeField] GameObject longNote;           // ロングノーツプレハブ

    /// <summary>
    /// ノーツの種類の列挙型
    /// </summary>
    enum NotesType
    { 
        NormalNote = 0, 
        LongNote = 1
    }


    const float frame = 60.0f;

    void OnEnable()
    {
        notesNum = 0;        // 総ノーツ数初期化
        songName = "オーバーライド - 重音テトSV[吉田夜世]"; // 曲名取得 現在は仮取得(のちに自動化)
        LoadMusicalScoreData(songName);      // 譜面データ読み込み
    }

    /// <summary>
    /// 譜面データの読み込み
    /// </summary>
    /// <param name="SongName">曲名</param>
    void LoadMusicalScoreData(string SongName)
    {
        // 曲名取得
        string inputSongName = Resources.Load<TextAsset>(songName).ToString();
        // 曲名からjsonファイル読み込み
        InputJsonData inputJson = JsonUtility.FromJson<InputJsonData>(inputSongName);

        notesNum = inputJson.notes.Length; // 総ノーツ数設定

        for (int i = 0;inputJson.notes.Length > 0;i++)
        {
            // ノーツの生成時間設定
            float distance = frame / (inputJson.BPM * inputJson.notes[i].LPB);
            float beatSec  = distance * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;
            // ノーツ情報をリストに追加
            notesTime.Add(time);
            lanesNum.Add(inputJson.notes[i].block);
            notesType.Add(inputJson.notes[i].type);

            // ノーツ生成
            float z = notesTime[i] * NotesSpeed;
            if (notesType[i] == (int)NotesType.NormalNote)
            {
                //notesObject.Add(Instantiate(normalNote, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
            }
            else if (notesType[i] == (int)NotesType.LongNote)
            {
                //notesObject.Add(Instantiate(normalNote, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
            }
        }
    }

    /// <summary>
    /// ノーツ生成
    /// </summary>
    /// <param name="notesData">ノーツデータ</param>
    void CreateNote(Notes notesData,float time)
    {
        // 生成座標計算
        float z = time * NotesSpeed;
        switch (notesData.block)
        {
            case 0:
                //notesObject.Add(Instantiate(normalNote, new Vector3(-1.1f, 0.47f, z), Quaternion.identity));
                break;
        }
    }
}
