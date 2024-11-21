using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeInterperter : MonoBehaviour, Iinterperter
{
    List<string> response = new List<string>();
    private string correctPassword = "earth";
    [SerializeField] private Animator doorAnimator;

    public List<string> Interpert(string input)
    {
        response.Clear();

        string[] args = input.Split();

        if (args[0] == "help")
        {
            response.Add("help");
            return response;
        }
        if (args[0] == "password") 
        {
            if (args.Length > 1 && args[1] == correctPassword)
            {
                OpenDoor(); 
                response.Add("Access granted. Opening Door.");
            }
            else
            {
                response.Add("Incorrect password.");
            }
            return response;
        }
        else
        {
            response.Add("command not recognized");
            return response;
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Password is correct! Opening Door...");
        
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
        else
        {
            Debug.LogWarning("Animator not assigned to OfficeInterperter script!");
        }
    }
}
