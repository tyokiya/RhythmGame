using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �m�[�c�̃R���g���[���[�N���X
/// </summary>
public class NotesController : MonoBehaviour
{
    [SerializeField] const float NotesSpeed = 5.0f; // �m�[�c�̑��x�萔

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NotesSpeed; 
    }
}
