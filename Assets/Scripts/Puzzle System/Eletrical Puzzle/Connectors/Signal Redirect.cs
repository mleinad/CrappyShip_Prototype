using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignalRedirect : MonoBehaviour, ISignalModifier
{
    [SerializeField]
    private int signal;
    
    bool is_over_base, is_docked;
    
    private float rayDistance = 10.0f;
    DragNDrop dragNDrop;
    ModuleBase base_t;    
    Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        dragNDrop = GetComponent<DragNDrop>();
    }
    void Update()
    {

        if(is_docked) rigidbody.isKinematic =true;

        if(dragNDrop.IsPickedUp())
        {
        is_docked =false;
        GetClosestBase();
        }

        DrawVectors();

        if(is_over_base && !dragNDrop.IsPickedUp())
        {

            if(!is_docked)
            {
                SnapToBase(base_t.transform);
                SnapRotation();
            }
        }
    }

    public void SetSignal(int value) => signal = value;

    void OnTriggerExit(Collider other)
    {
        signal = 0;
    }

    public int GetOutput()
    {
        return signal;
    }

    void GetClosestBase()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, rayDistance))
        {
            
         
            if(base_t!=null) base_t.SetComponent(null);
            
            ModuleBase moduleBase = hitInfo.collider.GetComponent<ModuleBase>();
            if(moduleBase==null)
            {
                is_over_base = false;
                return;
            }
            base_t = moduleBase;
            is_over_base = true;
            base_t.SetComponent(this);

        }

        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
    } 

    void SnapToBase(Transform base_transform)
    {
        transform.position = base_transform.GetChild(0).position;
        is_docked =true;
    }
    
    void SnapRotation()
    {

        List<Vector3> vectors = base_t.GetRotationAngles();
    
        Vector3 currentForward = Player.Instance.transform.forward; //ALTERNATIVE: transform.forward / relative to this object

        Vector3 closestDirection = vectors[0];
        float maxDot = -Mathf.Infinity;

        foreach (Vector3 direction in vectors)
        {
            float dot = Vector3.Dot(currentForward, direction);
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirection = direction;
            }
        }

        transform.rotation = Quaternion.LookRotation(closestDirection);
    }

    void DrawVectors(){

        float vectorLength = 2.0f;
        
        Vector3 front = transform.position + transform.forward * vectorLength;
        Vector3 back = transform.position - transform.forward * vectorLength;
        Vector3 right = transform.position + transform.right * vectorLength;
        Vector3 left = transform.position - transform.right * vectorLength;

        Debug.DrawLine(transform.position, front, Color.green);  // Front (green)
        Debug.DrawLine(transform.position, back, Color.red);     // Back (red)
        Debug.DrawLine(transform.position, right, Color.blue);   // Right (blue)
        Debug.DrawLine(transform.position, left, Color.yellow);  // Left (yellow)

    }

    public void SetSignal(Dictionary<IEletricalComponent, ColliderIO> adj_comp)
    {
        
        int maxSignal = 0;
        foreach (var component in adj_comp)
        {
            maxSignal = Mathf.Max(maxSignal, component.Key.GetSignal());
        }
        signal = maxSignal;
    }
}
