using UnityEngine;

/// <summary>
/// �m�[�c�f�[�^�N���X
/// </summary>
public class NoteData : MonoBehaviour
{
    public int laneNum;   // �~���Ă��郌�[���̔ԍ�
    public int typeNum;      // �~���Ă���m�[�c�̎�ޔԍ�
    public float time;    // �m�[�c��������Əd�Ȃ鎞��
    public GameObject noteObject; // ���������I�u�W�F�N�g������

    /// <summary>
    /// �m�[�c�f�[�^�̃Z�b�g
    /// </summary>
    public void SetNotesData(float notesTime, int laneNum, int notesType)
    {
        this.time = notesTime;
        this.laneNum = laneNum;
        this.typeNum = notesType;
    }

    /// <summary>
    /// ���������m�[�c�v���n�u������
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
