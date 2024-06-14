using UnityEngine;
using static NoteGenerator;
using static Judge;

public class ScoreCountor : MonoBehaviour
{
    int score;
    int combo;

    const int PerfectScore = 1000;
    const int GreatScore   = 100;
    const int BadScore     = 50;

    public void Initialized()
    {
        // �X�R�A�R���{������
        score = 0;
        combo = 0;
    }

    /// <summary>
    /// �󂯂Ƃ����m�[�c�̎�ނƔ��茋�ʂ���X�R�A���X�V
    /// </summary>
    /// <param name="notesType"></param>
    /// <param name="judge"></param>
    public void UpdateScore(NotesType notesType, JudgeNumber judge = 0)
    {
        // �m�[�c�̎�ނ��珈���𕪂���
        if(notesType == NotesType.NormalNote)
        {
            // ����ɉ����ăX�R�A���Z
            switch (judge)
            {
                case JudgeNumber.Perfect:
                    break;
            }
        }
    }
}
