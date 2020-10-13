
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "GameData/Game Data", order = 51)]
public class GameData : ScriptableObject
{
    [SerializeField] private int _observableTime = 1;

    public int ObservableTime { get => _observableTime; }
}
