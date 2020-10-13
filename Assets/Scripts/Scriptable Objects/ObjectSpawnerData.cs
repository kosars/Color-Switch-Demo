using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Object Spawner Data", menuName = "GameData/Object Spawner Data", order = 51)]
public class ObjectSpawnerData : ScriptableObject
{
    [SerializeField] private string _fileName = @"Resources/GeometryObjects.json";

    public List<string> GetData()
    {
        List<string> data = new List<string>();
        List<ObjectSpawnerName> objectSpawnerNames = JsonLoader.ReturnDatabase<ObjectSpawnerName>(_fileName);
        if (objectSpawnerNames.Count < 0)
            return data;
        foreach(ObjectSpawnerName name in objectSpawnerNames)
        {
            data.Add(name.Name);
        }

        return data;
    }
}

public class ObjectSpawnerName
{
    public string Name { get; set; }
}