using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 譜面データ(json)のクラス
/// </summary>
[Serializable]
class InputJsonData
{
    public string name;  // 曲名
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
 // ミドルノーツの必要データの一時保存領域クラス
/// </summary>
public class MiddleNotesTemporyData
{
    public bool generateFlg = false; // 生成フラグ
    public float startPosZ;          // 始点座標
    public float startTime;          // 始点の判定時間
}

/// <summary>
/// ノーツ生成クラス
/// </summary>
public class NoteGenerator : MonoBehaviour
{
    int notesNum;    // 総ノーツ数

    List<NoteData> middleNotesDataList_Red = new List<NoteData>(); 　// 生成したノーツデータリスト赤
    List<NoteData> middleNotesDataList_Green = new List<NoteData>(); // 生成したノーツデータリスト緑
    List<NoteData> middleNotesDataList_Blue = new List<NoteData>();  // 生成したノーツデータリスト青
    List<NoteData> normalNotesDataList = new List<NoteData>();       // 生成したノーツデータのリスト

    [SerializeField] List<GameObject> normalNoteObjectPrefab; // ノーマルノーツプレハブ
    [SerializeField] List<GameObject> longNoteObjectPrefab;   // ロングノーツプレハブ
    [SerializeField] Transform createNormalNotesTransform;    // 生成したノーマルノーツを入れるtransform
    [SerializeField] Transform createLongNotesTransform;      // 生成したロングノーツを入れる

    /// <summary>
    /// ノーツの種類の列挙型
    /// </summary>
    public enum NotesType
    {
        NormalNote = 1,
        LongNote = 2
    }

    float notesSpeed;
    // 定数
    const float frame = 60.0f; // fps
    const float notesPosY = 0.5f; // ノーツのy軸固定値

    // ミドルノーツの必要データの一時保存領域クラス
    MiddleNotesTemporyData redMiddleNotesTemporyData = new MiddleNotesTemporyData();   // 赤ノーツ
    MiddleNotesTemporyData greenMiddleNotesTemporyData = new MiddleNotesTemporyData(); // 緑ノーツ
    MiddleNotesTemporyData blueMiddleNotesTemporyData = new MiddleNotesTemporyData();  // 青ノーツ

    void OnEnable()
    {
        notesNum = 0; // 総ノーツ数初期化
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

        // 受け取ったデータ数だけノーツのデータのセットと生成
        for (int i = 0;i < inputJson.notes.Length;i++)
        {
            // ノーツの生成時間設定
            float distance = frame / (inputJson.BPM * inputJson.notes[i].LPB);
            float beatSec  = distance * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            // ノーツ情報をデータクラスに追加
            NoteData newData = gameObject.AddComponent<NoteData>();
            newData.SetNotesData(time, inputJson.notes[i].block, inputJson.notes[i].type);

            // ノーツ生成
            CreateNote(newData);

            // 生成したノーツデータをリストに追加
            normalNotesDataList.Add(newData);
        }
        // 生成したノーツにスピード設定
        SetSpeed();        
    }

    /// <summary>
    /// ノーツ生成
    /// </summary>
    /// <param name="notesData">ノーツデータ</param>
    void CreateNote(NoteData notesData)
    {
        // 生成座標計算
        float CreateObjectPosZ = notesData.time * notesSpeed ;
        //Debug.Log("ノーツタイプ "+ normalNotesDataList.typeNum);
        // レーンに合わせた座標
        switch (notesData.laneNum)
        {
            case 0:
                // ノーマルノーツ生成座標計算
                Vector3 createRedNormalNotesPos = new Vector3(LaneController.RedLanePosX, notesPosY, CreateObjectPosZ);
                if (notesData.typeNum == (int)NotesType.NormalNote)
                {
                    // ノーツの生成とデータのセット
                    notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createRedNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                }
                else if(notesData.typeNum == (int)NotesType.LongNote)
                {
                    if (redMiddleNotesTemporyData.generateFlg == false)
                    {
                        //Debug.Log("ロングノーツフラグ(赤)");
                        // ミドルノーツの生成に必要なデータの保存
                        redMiddleNotesTemporyData.generateFlg = true;
                        redMiddleNotesTemporyData.startPosZ = CreateObjectPosZ;
                        redMiddleNotesTemporyData.startTime = notesData.time;
                        // 始点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createRedNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                    }
                    else if (redMiddleNotesTemporyData.generateFlg == true)
                    {
                        //Debug.Log("ロングノーツ生成(赤)");
                        // 終点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createRedNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                        
                        // 中間ノーツの生成座標Z計算
                        float middleNotesCreatePosZ = redMiddleNotesTemporyData.startPosZ + ((CreateObjectPosZ - redMiddleNotesTemporyData.startPosZ) / 2);
                        // 中間ノーツの生成座標計算
                        Vector3 createMiddleNotesPos = new Vector3(LaneController.RedLanePosX, notesPosY, middleNotesCreatePosZ);
                        // 中間ノーツデータクラスの生成と初期化処理
                        NoteData middleNoteData = gameObject.AddComponent<NoteData>();
                        middleNoteData.SetNotesData(notesData.time, notesData.laneNum, ((int)NotesType.LongNote));
                        // 中間ノーツの生成
                        GameObject createMiddleNotes = Instantiate(longNoteObjectPrefab[notesData.laneNum], createMiddleNotesPos, Quaternion.identity, createLongNotesTransform);
                        // スケール変更
                        createMiddleNotes.transform.localScale = new Vector3(1, 1, (CreateObjectPosZ - redMiddleNotesTemporyData.startPosZ));
                        // 中間ノーツのホールド時間を設定
                        createMiddleNotes.GetComponent<MiddleNotesController>().SetMiddoleNotesTime(redMiddleNotesTemporyData.startTime, notesData.time);
                        // 生成したノーツをデータクラスに追加
                        middleNoteData.SetNotesObject(createMiddleNotes);
                        // 中間ノーツをレーンに合わせたデータリストに追加
                        AddMiddoleNoteData(middleNoteData);

                        // 一時データの初期化
                        redMiddleNotesTemporyData.generateFlg = false;
                        redMiddleNotesTemporyData.startPosZ = 0;
                        redMiddleNotesTemporyData.startTime = 0;
                    }
                }
                break;
            case 1:
                // ノーマルノーツ生成座標計算
                Vector3 createGreenNormalNotesPos = new Vector3(LaneController.GreenLanePosX, notesPosY, CreateObjectPosZ);
                if (notesData.typeNum == (int)NotesType.NormalNote)
                {
                    notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createGreenNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                }
                else if (notesData.typeNum == (int)NotesType.LongNote)
                {
                    if(greenMiddleNotesTemporyData.generateFlg == false)
                    {
                        //Debug.Log("ロングノーツフラグ(緑)");
                        greenMiddleNotesTemporyData.generateFlg = true;
                        greenMiddleNotesTemporyData.startPosZ = CreateObjectPosZ;
                        greenMiddleNotesTemporyData.startTime = notesData.time;
                        // 始点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createGreenNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                    }
                    else if(greenMiddleNotesTemporyData.generateFlg == true)
                    {
                        //Debug.Log("ロングノーツ生成(緑)");
                        // 終点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createGreenNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                        
                        // 中間用の生成座標Z計算
                        float middleNotesCreatePosZ = greenMiddleNotesTemporyData.startPosZ + ((CreateObjectPosZ - greenMiddleNotesTemporyData.startPosZ) / 2);
                        // 中間ノーツの生成座標計算
                        Vector3 createMiddleNotesPos = new Vector3(LaneController.GreenLanePosX, notesPosY, middleNotesCreatePosZ);
                        // 中間ノーツデータクラスの生成と初期化処理
                        NoteData middleNoteData = gameObject.AddComponent<NoteData>();
                        middleNoteData.SetNotesData(notesData.time, notesData.laneNum, ((int)NotesType.LongNote));
                        // 中間ノーツの生成
                        GameObject createMiddleNotes = Instantiate(longNoteObjectPrefab[notesData.laneNum], createMiddleNotesPos, Quaternion.identity, createLongNotesTransform);
                        // スケール変更
                        createMiddleNotes.transform.localScale = new Vector3(1, 1, (CreateObjectPosZ - greenMiddleNotesTemporyData.startPosZ));
                        // 中間ノーツのホールド終了時間を設定
                        createMiddleNotes.GetComponent<MiddleNotesController>().SetMiddoleNotesTime(greenMiddleNotesTemporyData.startTime, notesData.time);
                        // 生成したノーツをデータクラスに追加
                        middleNoteData.SetNotesObject(createMiddleNotes);
                        // 中間ノーツをレーンに合わせたデータリストに追加
                        AddMiddoleNoteData(middleNoteData);

                        // 一時データの初期化
                        greenMiddleNotesTemporyData.generateFlg = false;
                        greenMiddleNotesTemporyData.startPosZ = 0;
                        greenMiddleNotesTemporyData.startTime = 0;
                    }
                }
                break;
            case 2:
                // ノーマルノーツ生成座標計算
                Vector3 createBlueNormalNotesPos = new Vector3(LaneController.BlueLanePosX, notesPosY, CreateObjectPosZ);
                if (notesData.typeNum == (int)NotesType.NormalNote)
                {
                    notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createBlueNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                }
                else if (notesData.typeNum == (int)NotesType.LongNote)
                {
                    if (blueMiddleNotesTemporyData.generateFlg == false)
                    {
                        //Debug.Log("ロングノーツフラグ(青)");
                        blueMiddleNotesTemporyData.generateFlg = true;
                        blueMiddleNotesTemporyData.startPosZ = CreateObjectPosZ;
                        blueMiddleNotesTemporyData.startTime = notesData.time;
                        // 始点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createBlueNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                    }
                    else if (blueMiddleNotesTemporyData.generateFlg == true)
                    {
                        //Debug.Log("ロングノーツ生成(青)");
                        // 終点ノーツ生成
                        notesData.SetNotesObject(Instantiate(normalNoteObjectPrefab[notesData.laneNum], createBlueNormalNotesPos, Quaternion.identity, createNormalNotesTransform));
                        
                        // 中間用の生成座標Z計算
                        float middleNotesCreatePosZ = blueMiddleNotesTemporyData.startPosZ + ((CreateObjectPosZ - blueMiddleNotesTemporyData.startPosZ) / 2);
                        // 中間ノーツの生成座標計算
                        Vector3 createMiddleNotesPos = new Vector3(LaneController.BlueLanePosX, notesPosY, middleNotesCreatePosZ);
                        // 中間ノーツデータクラスの生成と初期化処理
                        NoteData middleNoteData = gameObject.AddComponent<NoteData>();
                        middleNoteData.SetNotesData(notesData.time, notesData.laneNum, ((int)NotesType.LongNote));
                        // 中間ノーツの生成
                        GameObject createMiddleNotes = Instantiate(longNoteObjectPrefab[notesData.laneNum], createMiddleNotesPos, Quaternion.identity, createLongNotesTransform);
                        // スケール変更
                        createMiddleNotes.transform.localScale = new Vector3(1, 1, (CreateObjectPosZ - blueMiddleNotesTemporyData.startPosZ));
                        // 中間ノーツのホールド終了時間を設定
                        createMiddleNotes.GetComponent<MiddleNotesController>().SetMiddoleNotesTime(blueMiddleNotesTemporyData.startTime, notesData.time);
                        // 生成したノーツをデータクラスに追加
                        middleNoteData.SetNotesObject(createMiddleNotes);
                        // 中間ノーツをレーンに合わせたデータリストに追加
                        AddMiddoleNoteData(middleNoteData);

                        // 一時データの初期化
                        blueMiddleNotesTemporyData.generateFlg = false;
                        blueMiddleNotesTemporyData.startPosZ = 0;
                        blueMiddleNotesTemporyData.startTime = 0;
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// レーン番号を変えす
    /// </summary>
    /// <param name="notesIndex">返すlanesNumのインデックス</param>
    public int GetLaneColor(int notesIndex)
    {
        return normalNotesDataList[notesIndex].laneNum;
    }

    /// <summary>
    /// ノーツの生成時間を返す
    /// </summary>
    /// <param name="notesIndex">返すnoteTimeのインデックス</param>
    /// <returns></returns>
    public float GetNotesTieme(int notesIndex)
    {
        return normalNotesDataList[notesIndex].time;
    }

    public int GetNotesType(int notesIndex)
    {
        return normalNotesDataList[notesIndex].typeNum;
    }

    /// <summary>
    /// ノーツのスピード設定
    /// </summary>
    void SetSpeed()
    {
        for (int i = 0; i < normalNotesDataList.Count; i++)
        {
            normalNotesDataList[i].noteObject.GetComponent<NotesController>().SetNotesSpeed(notesSpeed);
        }
        for (int i = 0; i < middleNotesDataList_Red.Count; i++)
        {
            middleNotesDataList_Red[i].noteObject.GetComponent<MiddleNotesController>().SetNotesSpeed(notesSpeed);
        }
        for (int i = 0; i < middleNotesDataList_Green.Count; i++)
        {
            middleNotesDataList_Green[i].noteObject.GetComponent<MiddleNotesController>().SetNotesSpeed(notesSpeed);
        }
        for (int i = 0; i < middleNotesDataList_Blue.Count; i++)
        {
            middleNotesDataList_Blue[i].noteObject.GetComponent<MiddleNotesController>().SetNotesSpeed(notesSpeed);
        }
    }

    /// <summary>
    /// ノーツデータにゲームスタートフラグをセット
    /// </summary>
    public void SetGameStart()
    {
        // 通常ノーツ
        for(int i = 0;i < normalNotesDataList.Count; i++)
        {
            normalNotesDataList[i].noteObject.GetComponent<NotesController>().SetIsGameStart();
        } 
        // 中間ノーツ
        for(int i = 0;i < middleNotesDataList_Red.Count; i++)
        {
            middleNotesDataList_Red[i].noteObject.GetComponent<MiddleNotesController>().SetIsGameStart();
        }
        for(int i=0;i<middleNotesDataList_Green.Count; i++)
        {
            middleNotesDataList_Green[i].noteObject.GetComponent<MiddleNotesController>().SetIsGameStart();
        }
        for (int i=0; i < middleNotesDataList_Blue.Count; i++)
        {
            middleNotesDataList_Blue[i].noteObject.GetComponent<MiddleNotesController>().SetIsGameStart();
        }
    }

    /// <summary>
    /// ミドルノーツのデータ追加
    /// </summary>
    void AddMiddoleNoteData(NoteData setData)
    {
        // レーンに合わせたリストに追加
        if (setData.laneNum == ((int)LaneController.LaneColor.Red)) middleNotesDataList_Red.Add(setData);
        else if (setData.laneNum == ((int)(LaneController.LaneColor.Green))) middleNotesDataList_Green.Add(setData);
        else if (setData.laneNum == ((int)LaneController.LaneColor.Blue)) middleNotesDataList_Blue.Add(setData);
    }

    /// <summary>
    /// ミドルノーツのデータ削除
    /// </summary>
    /// <param name="laneColor">削除するミドルノーツのレーンカラー</param>
    public void DeleteMiddleNoteData(LaneController.LaneColor laneColor)
    {
        switch (laneColor)
        {
            case LaneController.LaneColor.Red:
                middleNotesDataList_Red[0].DeleteNotes();
                middleNotesDataList_Red.RemoveAt(0);
                break;
            case LaneController.LaneColor.Green:
                middleNotesDataList_Green[0].DeleteNotes();
                middleNotesDataList_Green.RemoveAt(0);
                break;
            case LaneController.LaneColor.Blue:
                middleNotesDataList_Blue[0].DeleteNotes();
                middleNotesDataList_Blue.RemoveAt(0);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ノーマルノーツデータの削除
    /// </summary>
    /// <param name="index">削除するデータのインデックス</param>
    public void DeleteNormalNoteData(int index)
    {
        normalNotesDataList[index].DeleteNotes();
        normalNotesDataList.RemoveAt(index);        
    }
}
