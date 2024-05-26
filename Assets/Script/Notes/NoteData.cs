using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �m�[�c�f�[�^�N���X
/// </summary>
public class NoteData : MonoBehaviour
{
    public int laneNum;   // �~���Ă��郌�[���̔ԍ�
    public int type;      // �~���Ă���m�[�c�̎�ޔԍ�
    public float time;    // �m�[�c��������Əd�Ȃ鎞��
    public GameObject noteObject; // ���������I�u�W�F�N�g������

    /// <summary>
    /// �m�[�c�f�[�^�̃Z�b�g
    /// </summary>
    public void SetNotesData(float notesTime, int laneNum, int notesType)
    {
        this.time = notesTime;
        this.laneNum = laneNum;
        this.type = notesType;
    }

    /// <summary>
    /// ���������m�[�c������
    /// </summary>
    public void setNotesObject(GameObject createObject)
    {
        noteObject = createObject;
    }
}
