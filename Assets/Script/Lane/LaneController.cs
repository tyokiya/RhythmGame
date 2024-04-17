using UnityEngine;

/// <summary>
/// ���[���̃R���g���[���[�N���X
/// </summary>
public class LaneController : MonoBehaviour
{
    // �e���[���̃}�e���A��
    [SerializeField] Material redLaneMaterial;
    [SerializeField] Material greenLaneMaterial;
    [SerializeField] Material blueLaneMaterial;

    // Update is called once per frame
    void Update()
    {
        // �L�[���͎��ɉ��������[���̔���
        LuminescenceLane();
    }

    /// <summary>
    /// ���[���̔���
    /// </summary>
    void LuminescenceLane()
    {
        // �ԃ��[��
        if (Input.GetKey(KeyCode.F))
        {
            redLaneMaterial.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            redLaneMaterial.SetColor("_EmissionColor", Color.black);
        }
        // �΃��[��
        if (Input.GetKey(KeyCode.J))
        {
            greenLaneMaterial.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            greenLaneMaterial.SetColor("_EmissionColor", Color.black);
        }
        // ���[��
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