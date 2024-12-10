using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMediaState : TutorialBaseState
{
    [SerializeField]
    GameObject mediaPanel;
    public override void EnterState(TutorialUIManager context)
    {
        mediaPanel.SetActive(true);
    }

    public override void UpdateState(TutorialUIManager context)
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            context.SwithState(context.offState);
        }
    }

    public override void ExitState(TutorialUIManager context)
    {
        mediaPanel.SetActive(false);
    }

    private void Awake()
    {
        mediaPanel.SetActive(false);
    }
}
