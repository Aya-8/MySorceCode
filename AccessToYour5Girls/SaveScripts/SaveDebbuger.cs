using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class SaveDebbuger : MonoBehaviour, IController
{
    private INodeHistoryModel model;
    private IStatsModel sm;
    private int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        model = this.GetModel<INodeHistoryModel>();
        sm = this.GetModel<IStatsModel>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            model.MarkCompleted("d");
        }
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            model.MarkCompleted("a");
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            model.MarkCompleted("y");
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            model.MarkCompleted("0");
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            model.MarkCompleted("1");
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            model.MarkCompleted("_");
        }*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sm.AddAffinity("A",10);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sm.AddAffinity("A",-10);
        }
    }
    
    public IArchitecture GetArchitecture() => GameArchitecture.Interface;
}
