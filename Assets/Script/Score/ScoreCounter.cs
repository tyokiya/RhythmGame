using UnityEngine;
using static NoteGenerator;
using static Judge;

public class ScoreCountor : MonoBehaviour
{
    [SerializeField] FrameController frameController; // �t���[���R���g���[���[
    int score;
    int combo;

    const int PerfectScore = 1000;
    const int GreatScore   = 100;
    const int BadScore     = 50;
    const int LongNotesAddScoreInterval = 30; // �����O�m�[�c�̃X�R�A���Z�Ԋu

    public void Initialized()
    {
        // �X�R�A�R���{������
        score = 0;
        combo = 0;
    }

    /// <summary>
    /// �󂯂Ƃ����m�[�c�̎�ނƔ��茋�ʂ���X�R�A���X�V
    /// </summary>
    /// <param name="notesType">���肵���m�[�c�̎��</param>
    /// <param name="judge">���茋��(�����O�m�[�c�̏ꍇ�͏ȗ��\)</param>
    public void UpdateScore(NotesType notesType, JudgeNumber judge = 0)
    {
        // �m�[�c�̎�ނ��珈���𕪂���
        if(notesType == NotesType.NormalNote)    // �m�[�}���m�[�c
        {
            // ����ɉ����ăX�R�A���Z
            switch (judge)
            {
                case JudgeNumber.Perfect:               // �p�[�t�F�N�g
                    score += PerfectScore; // �X�R�A���Z
                    combo++;               // �R���{���Z
                    break;
                case JudgeNumber.Great:                 // �O���[�g
                    score += GreatScore;
                    combo++;
                    break;
                case JudgeNumber.Bad:                   // �o�b�h
                    score += BadScore;
                    combo++;
                    break;
                case JudgeNumber.Miss:                  // �~�X
                    // ���Z�Ȃ�
                    combo = 0; // �R���{���Z�b�g
                    break;
                default:
                    break; 
            }
        }
        else if(notesType == NotesType.LongNote) // �����O�m�[�c
        {
            // 0.5�b���ƂɃX�R�A�ƃR���{���Z
            // ����͂��ׂ�Perfect����Ƃ��Ĉ�����
            if(frameController.GetNowTortalFPS() == LongNotesAddScoreInterval)
            {
                score += PerfectScore; // �X�R�A���Z
                combo++;               // �R���{���Z
            }
        }
    }
}
