using System;

/// <summary>
/// �Q�[���C�x���g��ێ�����N���X
/// </summary>
public class GameEvent
{
    public Action<SoundController.MusicNameList> OnDataLoad; // �K�v�f�[�^�̃��[�h�C�x���g
    public Action IsGameStart;                                 // �Q�[���X�^�[�g�C�x���g
}
