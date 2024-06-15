using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    int nowTortalFPS = 0; // ���݂̃g�[�^��fps�l(���t���[�����Z�����)

    void Start()
    {
        // �t���[���Œ�
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // �t���[�����Z
        nowTortalFPS++;

        // �I�[�o�[�t���[�΍�
        if(nowTortalFPS >= int.MaxValue)
        {
            nowTortalFPS = 0;
        }
    }

    /// <summary>
    /// ���݂̃g�[�^��fps�l�̃Q�b�^�[
    /// </summary>
    /// <returns></returns>
    public int GetNowTortalFPS()
    {
        return nowTortalFPS;
    }
}
