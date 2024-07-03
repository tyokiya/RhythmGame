using UnityEngine;
using static LaneController;

public class MiddleNotesController : MonoBehaviour
{
    [SerializeField] new MeshRenderer renderer;
    float notesSpeed;
    bool isGameStart = false;
    bool isHold;         // ホールド中のフラグ
    float holdStartTime; // ホールド開始時間
    float holdEndTime;   // ホールド終了時の時間
    float gameStartTIme; // ゲーム開始時間

    const float NoteModelActivePosZ = 40; // ノーツモデルのアクティブ化座標z

    void Start()
    {
        // ノーツモデルの非アクティブ化
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.z < NoteModelActivePosZ && !transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

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
        float middleNotesCenterPosZ = middleNotesEndPosZ / 2;                              // 現在のミドルノーツの中心座標Zの計算
        transform.localScale = new Vector3(1, 1, middleNotesEndPosZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, middleNotesCenterPosZ);
    }

    /// <summary>
    /// ホールド開始時間と終了時間のセット
    /// </summary>
    /// <param name="setStartTime"></param>
    /// <param name="setEndTime"></param>
    public void SetMiddoleNotesHoldTime(float setStartTime, float setEndTime)
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

    /// <summary>
    /// ノーツの発行処理セット
    /// </summary>
    /// <param name="flg">発光のフラグ</param>
    /// <param name="laneColor">そのノーツのレーンカラー</param>
    public void SetLuminescence(bool flg, LaneColor laneColor)
    {
        var material = renderer.material;
        // 発光処理
        if(flg)
        {
            switch(laneColor)
            {
                case LaneColor.Red:
                    material.SetColor("_EmissionColor", Color.red);
                    break; 
                case LaneColor.Green:
                    material.SetColor("_EmissionColor", Color.green);
                    break;
                case LaneColor.Blue:
                    material.SetColor("_EmissionColor", Color.blue);
                    break;
                default:
                    break;
            }            
        }
        else // 発光終了
        {
            material.SetColor("_EmissionColor", Color.black);
        }
        
    }

    /// <summary>
    /// テストモードで必要なデータの設定(テストモードで使用)
    /// </summary>
    /// <param name="startTime">楽曲再生開始時間</param>
    public void SetTestMode(float startTime)
    {
        // 座標調整
        Vector3 pos = this.transform.position; // ノーツオブジェクトのtransform取得
        pos.z -= startTime * notesSpeed;       // スタート開始時間分の移動計算
        this.transform.position = pos;         // 計算した座標を反映

        // ホールド開始時間と終了時間の調整
        holdStartTime -= startTime;
        holdEndTime -= startTime;
    }
}
