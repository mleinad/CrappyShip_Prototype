using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{

    public LayerMask layerMask;
    public float defaultLength = 50;
    public int numberOfReflections = 2;

    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 direction;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ReflectLaser();
    }


    private void NormalLaser()
    {
        lineRenderer.SetPosition(0,transform.position);

        if(Physics.Raycast(transform.position, transform.forward,out hit, defaultLength, layerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }

    }

    private void ReflectLaser()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0,transform.position);

        float remainLength = defaultLength;

        for (int i = 0; i < numberOfReflections; i++)
        {   
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainLength, layerMask))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction,hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength));
            }


        }

    }

    
}
