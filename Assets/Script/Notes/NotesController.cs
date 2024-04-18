using UnityEngine;

/// <summary>
/// ノーツのコントローラークラス
/// </summary>
public class NotesController : MonoBehaviour
{
    float notesSpeed;

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * notesSpeed; 
    }

    public void SetNotesSpeed(float setNotesSpeed)
    {
        notesSpeed = setNotesSpeed;
    }
}
