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

    public float BatteryLeft = 100f;
    private float _maxBattery = 100f; //get from current level
    private float _batteryDecay = 1f;
    private void Start()
    {
        _batteryBar.fillAmount = 1f;
    }

    void Update()
    {
        _batteryBar.fillAmount = BatteryLeft / _maxBattery;
    }

    public bool ConsumeBattery(bool isJumping = false)
    {
        BatteryLeft -= isJumping ? _batteryDecay * 10 : _batteryDecay;
        if (BatteryLeft <= 0)
        {
            BatteryLeft = 0f;
            return false;
        }

        Debug.Log("Consuming - Battery Left: " + BatteryLeft);
        // _batteryBubble.SetActive(_maxBattery * 100 / _batteryLeft > 20);

        return true;

    }
    public void RefillBattery()
    {
        BatteryLeft += 1f;
        if (BatteryLeft > _maxBattery)
            BatteryLeft = _maxBattery;
        Debug.Log("Refilling - Battery Left: " + BatteryLeft);
    }


    public void RecalibrateBatteries()
    {
        //_maxBattery = _levelManager.BatteryAmount;
        //_batteryLeft = _maxBattery;
    }
}