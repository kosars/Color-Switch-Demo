using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerModel : MonoBehaviour
{
    [SerializeField] private ObjectSpawnerData _objectSpawnerData;
    private List<string> _objectNames;
    private void OnEnable() => RayCastController.OnRayHitEmpty += HandleRayHitEmpty;
    private void OnDisable() => RayCastController.OnRayHitEmpty -= HandleRayHitEmpty;

    private void Awake() =>_objectNames = _objectSpawnerData.GetData();
    private void HandleRayHitEmpty(Vector3 point)
    {
        if (_objectNames.Count < 0)
            return;
        int objectNumber = Random.Range(0, _objectNames.Count); //Get the index of random GeometryObject
        GameObject gameObject = (GameObject)Resources.Load("Prefabs/" + _objectNames[objectNumber], typeof(GameObject)); //Load Object From Resources
        SpawnObject(gameObject, point);
    }

    private void SpawnObject(GameObject gameObject, Vector3 point)
    {
        Quaternion quaternion = new Quaternion();
        Instantiate(gameObject, point, quaternion);
    }
}
