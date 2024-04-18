using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ʃf�[�^(json)�̃N���X
/// </summary>
[Serializable]
class InputJsonData
{
    public string name;     // �Ȗ�
    public int maxBlock; // �ő吔
    public int BPM;
    public int offset;
    public Notes[] notes;
}

/// <summary>
/// �m�[�c�N���X
/// </summary>
[Serializable]
public class Notes
{
    public int type;  // ���
    public int num;   // �m�[�c�G�f�B�^�[��ł̏c���̈ʒu
    public int block; // ���[���̈ʒu
    public int LPB;   // 1�������������ăm�[�c��u����
}

/// <summary>
/// �m�[�c�����N���X
/// </summary>
public class NoteGenerator : MonoBehaviour
{
    int notesNum;    // ���m�[�c��
    string songName; // �Ȗ�
    
    List<int>   lanesNum  = new List<int>();   // �~���Ă��郌�[���̔ԍ�
    List<int>   notesType = new List<int>();   // �~���Ă���m�[�c�̎�ޔԍ�
    List<float> notesTime = new List<float>(); // �m�[�c��������Əd�Ȃ鎞��
    List<GameObject> notesObject = new List<GameObject>(); //�m�[�c�I�u�W�F�N�g

    [SerializeField] const float NotesSpeed = 5.0f;  // �m�[�c���x
    [SerializeField] List<GameObject> noteObject;    // �m�[�c�v���n�u
    [SerializeField] Transform createNotesTransform; // ���������m�[�c������transform

    /// <summary>
    /// �m�[�c�̎�ނ̗񋓌^
    /// </summary>
    enum NotesType
    { 
        NormalNote = 1, 
        LongNote = 2
    }


    const float frame = 60.0f;

    void OnEnable()
    {
        notesNum = 0;        // ���m�[�c��������
        songName = "�I�[�o�[���C�h - �d���e�gSV[�g�c�鐢]"; // �Ȗ��擾 ���݂͉��擾(�̂��Ɏ�����)
        LoadMusicalScoreData(songName);      // ���ʃf�[�^�ǂݍ���
    }

    /// <summary>
    /// ���ʃf�[�^�̓ǂݍ���
    /// </summary>
    /// <param name="SongName">�Ȗ�</param>
    void LoadMusicalScoreData(string SongName)
    {
        // �Ȗ��擾
        string inputSongName = Resources.Load<TextAsset>(songName).ToString();
        // �Ȗ�����json�t�@�C���ǂݍ���
        InputJsonData inputJson = JsonUtility.FromJson<InputJsonData>(inputSongName);

        notesNum = inputJson.notes.Length; // ���m�[�c���ݒ�

        for (int i = 0;i < inputJson.notes.Length;i++)
        {
            // �m�[�c�̐������Ԑݒ�
            float distance = frame / (inputJson.BPM * inputJson.notes[i].LPB);
            float beatSec  = distance * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;
            // �m�[�c�������X�g�ɒǉ�
            notesTime.Add(time);
            lanesNum.Add(inputJson.notes[i].block);
            notesType.Add(inputJson.notes[i].type);

            // �m�[�c����
            CreateNote(inputJson.notes[i], time);
        }
    }

    /// <summary>
    /// �m�[�c����
    /// </summary>
    /// <param name="notesData">�m�[�c�f�[�^</param>
    void CreateNote(Notes notesData,float time)
    {
        // �������W�v�Z
        float z = time * NotesSpeed;
        Debug.Log("�m�[�c�^�C�v "+ notesData.type);
        // ���[���ɍ��킹�����W
        switch (notesData.block)
        {
            case 0:
                if(notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(-1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if(notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(-1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
            case 1:
                if (notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(0, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if (notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(0, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
            case 2:
                if (notesData.type == (int)NotesType.NormalNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.NormalNote - 1], new Vector3(1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                else if (notesData.type == (int)NotesType.LongNote)
                {
                    notesObject.Add(Instantiate(noteObject[(int)NotesType.LongNote - 1], new Vector3(1.1f, 0.47f, z), Quaternion.identity, createNotesTransform));
                }
                break;
        }
    }
}
