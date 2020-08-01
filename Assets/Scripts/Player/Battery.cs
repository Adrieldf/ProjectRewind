using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private Image _batteryBar = null;
    [SerializeField]
    private GameObject _batteryBubble = null;

    //[SerializeField]
    //private LevelManager _levelManager = null;

    private float _batteryLeft = 100f;
    private float _maxBattery = 100f; //get from current level
    private float _batteryDecay = 1f;
    private void Start()
    {
        _batteryBar.fillAmount = 1f;
    }

    void Update()
    {
        _batteryBar.fillAmount = _batteryLeft / _maxBattery;
    }

    public bool ConsumeBattery(bool isJumping = false)
    {
        _batteryLeft -= isJumping ? _batteryDecay * 10 : _batteryDecay;
        if (_batteryLeft <= 0)
            return false;

       // _batteryBubble.SetActive(_maxBattery * 100 / _batteryLeft > 20);

        return true;

    }
    public void RefillBattery()
    {
        _batteryLeft += 1f;
        if (_batteryLeft > _maxBattery)
            _batteryLeft = _maxBattery;
    }


    public void RecalibrateBatteries()
    {
        //_maxBattery = _levelManager.BatteryAmount;
        //_batteryLeft = _maxBattery;
    }
}
