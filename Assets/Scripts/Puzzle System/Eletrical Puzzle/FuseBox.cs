using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour, IPuzzleComponent, IEletricalComponent
{
   public bool state= false;
    public int signal;
    public int signal_needed;


    List<IEletricalComponent> adjecent_components;
    public List<string> adj_comp_names;

    public bool CheckCompletion()=>state;

    public int GetSignal()
    {
       return signal;
    }


     void Start()
    {    
        adjecent_components = new List<IEletricalComponent>();
    }

    public void ResetPuzzle()=> state = false;

    void Update()
    {
        if(signal >0) state = true; 
        else ResetPuzzle();         

        transform.GetChild(0).Rotate(0, 0, signal * Time.deltaTime); 

        
        CheckAdjacencies();
    }

 void OnTriggerEnter(Collider other)
    {   
        IEletricalComponent electricalComponent;
        electricalComponent = other.GetComponent<IEletricalComponent>();

        if(electricalComponent == null) return;

        if(!adjecent_components.Contains(electricalComponent)){
            adjecent_components.Add(electricalComponent);
        }

    }
    void OnTriggerExit(Collider other)
    {    
        IEletricalComponent eletricalComponent;
        eletricalComponent = other.GetComponent<IEletricalComponent>();

        if(eletricalComponent == null) return;

        if(adjecent_components.Contains(eletricalComponent)){
            adjecent_components.Remove(eletricalComponent);
        }
    }
    void CheckAdjacencies()
    {
        adj_comp_names = new List<string>();

        foreach(IEletricalComponent comp in adjecent_components){
            adj_comp_names.Add(comp.ToString());
        } 
    }

    public List<IEletricalComponent> GetAdjacencies()
    {
        throw new System.NotImplementedException();
    }

    public void SetSignal(int newSignal)
    {
        signal = newSignal;
        PropagateSignal();
    }

     public void PropagateSignal()
    {
        foreach (var component in adjecent_components)
        {
           if(component is not ModuleBase)
           {
                if (component.GetSignal() < signal)
                {
                    component.SetSignal(signal);
                }   
           }
        }
    }
}
