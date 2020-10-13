using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GeometryObjectData", menuName = "GameData/Geometry Object Data", order = 51)]
public class GeometryObjectData : ScriptableObject
{
    [SerializeField] private List<ClickColorData> _clickData;

    public List<ClickColorData> GetClickData() { return _clickData; }
}
