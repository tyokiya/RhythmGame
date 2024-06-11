using UnityEngine;

public class MiddleNotesController : MonoBehaviour
{
    float notesSpeed;
    bool isGameStart = false;
    bool isHold;         // �z�[���h���̃t���O
    float holdStartTime; // �z�[���h�J�n����
    float holdEndTime;   // �z�[���h�I�����̎���
    float gameStartTIme; // �Q�[���J�n����

    void Update()
    {
        if (isGameStart)
        {
            UpdatePos(); // ���W�X�V

            // �z�[���h�J�n�t���O�̔���
            if (holdStartTime + gameStartTIme < Time.time && !isHold)
            {
                isHold = true; // �z�[���h�t���O�𗧂Ă�
            }

            // �z�[���h�I���t���O�̔���
            if(holdEndTime + gameStartTIme < Time.time)
            {
                isHold = false; // �z�[���h�t���O�����낷
            }
        }

        if (isHold)
        {
            ScaleUpdate(); // �T�C�Y�X�V
        }
    }

    void UpdatePos()
    {
        transform.position -= transform.forward * notesSpeed * Time.deltaTime;
    }

    /// <summary>
    /// �T�C�Y�ύX�X�V
    /// </summary>
    void ScaleUpdate()
    {
        float middleNotesEndPosZ = (holdEndTime + gameStartTIme - Time.time) * notesSpeed; // ���݂̃~�h���m�[�c�̏I�_���W�v�Z
        float middleNotesCenterPosZ = middleNotesEndPosZ / 2;     // ���݂̃~�h���m�[�c�̒��S���WZ�̌v�Z
        transform.localScale = new Vector3(1, 1, middleNotesEndPosZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, middleNotesCenterPosZ);
    }

    public void SetMiddoleNotesTime(float setStartTime, float setEndTime)
    {
        holdStartTime = setStartTime;
        holdEndTime = setEndTime;
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
        //Debug.Log("�X�s�[�h�ݒ�");
    }

    public void SetIsGameStart()
    {
        isGameStart = true;
        gameStartTIme = Time.time; // �J�n���Ԑݒ�
        //Debug.Log("�X�^�[�g�t���O");
    }
}
