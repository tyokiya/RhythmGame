using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レーンのコントローラークラス
/// </summary>
public class LaneController : MonoBehaviour
{
    [SerializeField] GameObject redLane;
    [SerializeField] GameObject greenLane;
    [SerializeField] GameObject blueLane;

    Renderer redLaneRenderer;

    void Start()
    {
        redLaneRenderer = redLane.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            //redLaneRenderer.material.
        }
    }


}
