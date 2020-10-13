using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ObjectSpawnerModel : MonoBehaviour
{
    [SerializeField] private ObjectSpawnerData _objectSpawnerData;
    private List<string> _objectNames;
    private string _bundlesPath;
    private Object _cachedObject;
    private void OnEnable() => RayCastController.OnRayHitEmpty += HandleRayHitEmpty;
    private void OnDisable() => RayCastController.OnRayHitEmpty -= HandleRayHitEmpty;

    private void Awake() 
    {
        _objectNames = _objectSpawnerData.GetData();
        _bundlesPath = _objectSpawnerData.BundlesPath;
    } 
    private void HandleRayHitEmpty(Vector3 point)
    {
        if (_objectNames.Count < 0)
            return;
        int objectNumber = Random.Range(0, _objectNames.Count); //Get the index of random GeometryObject
        //GameObject gameObject = (GameObject)Resources.Load("Prefabs/" + _objectNames[objectNumber], typeof(GameObject)); //Load Object From Resources
        Observable.FromCoroutine(() => GetPrimitiveBundle(_bundlesPath, objectNumber))
            .DoOnCompleted(() => SpawnObject(_cachedObject as GameObject, point))
            .Subscribe();
    }

    private void SpawnObject(GameObject gameObject, Vector3 point)
    {
        Quaternion quaternion = new Quaternion();
        Instantiate(gameObject, point, quaternion);
    }
    
    private IEnumerator GetPrimitiveBundle(string path, int index)
    {
        while (!Caching.ready)
            yield return null;
        string name = _objectNames[index];
        string assetBundlePath = Application.dataPath + path + name;
        var assetBundleRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);
        yield return assetBundleRequest;
        var assetBundle = assetBundleRequest.assetBundle;
        var prefabRequest = assetBundle.LoadAssetAsync(name);
        yield return prefabRequest;
        _cachedObject = prefabRequest.asset;
        assetBundle.Unload(false);
    }
}
