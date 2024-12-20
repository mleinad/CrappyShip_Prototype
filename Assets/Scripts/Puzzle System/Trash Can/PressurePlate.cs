using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour, IPuzzleComponent
{
    
    public int numberOfObjects = 2;
    bool state = false;
    
    public List<GameObject> objectsOnPlate = new List<GameObject>();

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlateObject"))
        {
            objectsOnPlate.Add(collision.gameObject);
            CheckActivation();

        }

    }
    
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("PlateObject"))
        {
            objectsOnPlate.Remove(collision.gameObject);
            CheckActivation();
            
        }

    }
    private void ActivatePlate()
    {
       // Debug.Log("Placa de Pressao Ativada.");
       state = true;
       EventManager.Instance.OnAiTrigger(this);
    }

    private void CheckActivation()
    {
        if(objectsOnPlate.Count == numberOfObjects)
        {
            ActivatePlate();
        }
        else
        {
            DeactivatePlate();
        }
    }



    private void DeactivatePlate()
    {
       // Debug.Log("Placa de Pressao Desativada.");
        state = false;
    }
    
    
    public int GetNumberOfObjects() => numberOfObjects;
    public int GetCurrentObjects()=> objectsOnPlate.Count;
    public bool CheckCompletion()=> state;

    public void ResetPuzzle()
    {
       state = false;
    }
}
