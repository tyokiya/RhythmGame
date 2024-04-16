using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ノーツのコントローラークラス
/// </summary>
public class NotesController : MonoBehaviour
{
    [SerializeField] const int NotesSpeed = 5; // ノーツの速度定数

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NotesSpeed; 
    }
}
