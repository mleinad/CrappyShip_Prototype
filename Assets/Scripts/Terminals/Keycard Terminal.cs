using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using MoodMe;
using UnityEngine;

public class KeycardTerminal : MonoBehaviour, IPuzzleComponent
{
    Interactable interactable;
    public GameObject face_camera;

    bool state = false;

    
    [SerializeField]
    CanvasGroup canvasGroup;


    public float targetTime = 1.0f;
    public float surprised__factor=0.0f;
    IPuzzleComponent keycard;
    public Camera camera;
    void Start()
    {
        EnableTerminal(false);
        interactable = GetComponent<Interactable>();

    }

    // Update is called once per frame
    void Update()
    {

        if(interactable.WasTriggered())
        {
            camera.enabled = true;
            Player.Instance.LockMovement(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            EnableTerminal(true);
        }


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnableTerminal(false);
            camera.enabled = false;
            Player.Instance.LockMovement(false);
                 Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            interactable.SetTrigger(false);
        }


    }

    void EnableTerminal(bool state)
    {
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    public bool CheckCompletion() => state;

    public void ResetPuzzle()
    {
        state = false;
    }



    IEnumerator ScanFace()
    {
        face_camera.SetActive(true); 

        surprised__factor = EmotionsManager.Emotions.surprised;
        if(surprised__factor>6.0f)
        {

        }
        yield return 2f;
        face_camera.SetActive(false);
    }

}
