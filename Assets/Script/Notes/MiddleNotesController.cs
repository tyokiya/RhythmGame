using UnityEngine;

public class MiddleNotesController : MonoBehaviour
{
    protected float notesSpeed;
    protected bool isGameStart = false;
    bool isHold; // �z�[���h���̃t���O

    void Update()
    {
        if (isGameStart)
        {
            UpdatePos(); // ���W�X�V
        }
        if (isHold)
        {
            ScaleUpdate(); // �T�C�Y�X�V
        }
    }

    void UpdatePos()
    {
        transform.position -= transform.forward * Time.deltaTime * notesSpeed;
    }

    void ScaleUpdate()
    {
        transform.localScale = new Vector3(1, 1, transform.localScale.z - (notesSpeed / 2)); // �X�P�[���ύX
        transform.position += transform.forward * notesSpeed / 2 * Time.deltaTime; // �T�C�Y�ύX���̍��W����
    }

    public void SetStartHold()
    {
        isHold = true;
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
        //Debug.Log("�X�s�[�h�ݒ�");
    }

    public void SetIsGameStart()
    {
        isGameStart = true;
        //Debug.Log("�X�^�[�g�t���O");
    }
}
