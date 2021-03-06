﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GeometryObjectModel : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GeometryObjectData _geometryObjectData;
    [SerializeField] private Renderer _renderer;


    private int _clickCount = 0;
    private int _observableTime;
    private List<ClickColorData> _clickData;

    private void Awake() 
    {
        //initialization
        _observableTime = _gameData.ObservableTime;
        _clickData = _geometryObjectData.GetClickData();

        //change the color of our object every (_observableTime) seconds
        Observable.FromCoroutine(SetRandomColorByTime).Repeat().Subscribe();
    }

    private IEnumerator SetRandomColorByTime()
    {
        SetRandomColor();
        yield return new WaitForSeconds(_observableTime);
    }

    private void SetColor(Color color) =>
        _renderer.material.SetColor("_Color", color);

    private void SetRandomColor()
    {
        Color color = UnityEngine.Random.ColorHSV();
        SetColor(color);
    }

    public void HandleRayHit()
    {
        _clickCount++;
        if (_clickData == null)
            return;
        foreach (ClickColorData clickColorData in _clickData) 
        {
            //Change the color if current _clickCount is MinClicksCount-MaxClicksCount range
            if((_clickCount >= clickColorData.MinClickCount) &&
                (_clickCount <= clickColorData.MaxClickCount))
            {
                SetColor(clickColorData.Color);
            }
        }
    }

}
