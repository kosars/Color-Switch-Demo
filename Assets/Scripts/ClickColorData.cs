using UnityEngine;

[System.Serializable]
public class ClickColorData 
{
    [SerializeField] private string _objectType;
    [SerializeField] private int _minClickCount, _maxClickCount;
    [SerializeField] private Color _color;

    public string ObjectType { get => _objectType; }
    public int MinClickCount { get => _minClickCount; }
    public int MaxClickCount { get => _maxClickCount; }
    public Color Color { get => _color; }
}
