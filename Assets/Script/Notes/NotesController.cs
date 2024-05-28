﻿using UnityEngine;

/// <summary>
/// ノーツのコントローラークラス
/// </summary>
public class NotesController : MonoBehaviour
{
    float notesSpeed;
    bool isGameStart = false;

    void Update()
    {
        if (isGameStart)
        {
            transform.position -= transform.forward * Time.deltaTime * notesSpeed;
        }        
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
        Debug.Log("スピード設定");
    }

    public void SetIsGameStart()
    {
        isGameStart = true;
        Debug.Log("スタートフラグ");
    }
}
