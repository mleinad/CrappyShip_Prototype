using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class FuseBox : MonoBehaviour, IPuzzleComponent, IEletricalComponent
{
    bool state= false;
    int signal;

    public int signal_needed;
    public bool CheckCompletion()=>state;

    public int GetSignal()
    {
       return signal;
    }

    public void ResetPuzzle()=> state = false;

    void Update()
    {
            if(signal> signal_needed) state = true;        
    }


    void OnTriggerEnter(Collider other)
    {
        IEletricalComponent electricalComponent;
        electricalComponent = other.GetComponent<IEletricalComponent>();
        signal = electricalComponent.GetSignal();
    }
}
