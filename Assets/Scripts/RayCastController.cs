using System;
using UnityEngine;


public class RayCastController : MonoBehaviour
{
    public static event Action<Vector3> OnRayHitEmpty;

    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                GeometryObjectModel geometryObjectModel = hit.transform.gameObject.GetComponent<GeometryObjectModel>();
                //TryGetComponent isn't supports by unity 2018.3 :(((

                if (geometryObjectModel != null)
                {
                    geometryObjectModel.HandleRayHit();
                }
                else
                {
                    OnRayHitEmpty?.Invoke(hit.point);
                }
                   
            }
        }
    }
}
