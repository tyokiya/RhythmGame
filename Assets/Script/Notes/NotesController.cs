using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツのコントローラークラス
/// </summary>
public class NotesController : MonoBehaviour
{
    [SerializeField] const float NotesSpeed = 5.0f; // ノーツの速度定数

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NotesSpeed; 
    }
}
