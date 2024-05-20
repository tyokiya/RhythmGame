using UnityEngine;

/// <summary>
/// レーンのコントローラークラス
/// </summary>
public class LaneController : MonoBehaviour
{
    // 各レーンのマテリアル
    [SerializeField] Material redLaneMaterial;
    [SerializeField] Material greenLaneMaterial;
    [SerializeField] Material blueLaneMaterial;

    // Update is called once per frame
    void Update()
    {
        // キー入力時に応じたレーンの発光
        LuminescenceLane();
    }

    /// <summary>
    /// レーンの種類の列挙型
    /// </summary>
    public enum LaneColor
    {
        Red = 0,
        Green = 1,
        Blue = 2,
    }

    // 各レーンの座標xの定数
    public const float RedLanePosX = -1.1f;
    public const float GreenLanePosX = 0f;
    public const float BlueLanePosX = 1.1f;

    /// <summary>
    /// レーンの発光
    /// </summary>
    void LuminescenceLane()
    {
        // 赤レーン
        if (Input.GetKey(KeyCode.F))
        {
            redLaneMaterial.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            redLaneMaterial.SetColor("_EmissionColor", Color.black);
        }
        // 緑レーン
        if (Input.GetKey(KeyCode.J))
        {
            greenLaneMaterial.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            greenLaneMaterial.SetColor("_EmissionColor", Color.black);
        }
        // 青レーン
        if (Input.GetKey(KeyCode.K))
        {
            blueLaneMaterial.SetColor("_EmissionColor", Color.blue);
        }
        else
        {
            blueLaneMaterial.SetColor("_EmissionColor", Color.black);
        }
    }
}
