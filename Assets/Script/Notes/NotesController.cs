using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �m�[�c�̃R���g���[���[�N���X
/// </summary>
public class NotesController : MonoBehaviour
{
    [SerializeField] const int NotesSpeed = 5; // �m�[�c�̑��x�萔

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NotesSpeed; 
    }
}
