using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SoundManager.MusicNameList music; // �v���C�����
    [SerializeField] SoundManager soundManager;
    [SerializeField] NoteGenerator notesGenerator;
    [SerializeField] const float NotesSpeed = 5;   // �m�[�c���x
    [SerializeField] List<GameObject> notesObject; // �m�[�c�̃v���n�u

    Action<SoundManager.MusicNameList> OnGameStart; // �Q�[���X�^�[�g�C�x���g

    void Start()
    {
        // �C�x���g�ǉ�
        AddEvent();

        // �m�[�c���x�ݒ�
        SetNotesSpeed();

        // �X�^�[�g�C�x���g�Ăяo��
        OnGameStart(music);
    }

    /// <summary>
    /// �m�[�c���x�̐ݒ�
    /// </summary>
    void SetNotesSpeed()
    {
        notesGenerator.SetNotesSpeed(NotesSpeed); // �W�F�l���[�^�[�̃m�[�c�X�s�[�h�ݒ�
        // �m�[�c�R���g���[���[�̑��x�ݒ�
        for (int i = 0; i < notesObject.Count; i++)
        {
          
        }
    }

    /// <summary>
    /// �C�x���g�̒ǉ�
    /// </summary>
    void AddEvent()
    {
        // �Q�[���X�^�[�g�C�x���g
        OnGameStart += soundManager.PlayMusic;
        OnGameStart += notesGenerator.LoadMusicalScoreData;
    }

    /// <summary>
    /// �C�x���g�폜
    /// </summary>
    void DestroyEvent()
    {
        // �Q�[���X�^�[�g�C�x���g
        OnGameStart -= soundManager.PlayMusic;
        OnGameStart -= notesGenerator.LoadMusicalScoreData;
    }
}