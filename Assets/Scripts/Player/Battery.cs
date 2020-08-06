using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private Image _batteryBar = null;
    [SerializeField]
    private GameObject _batteryBubble = null;
    private float _maxBattery = 100f;
    private float _batteryDecay = 1f;
    //[SerializeField]
    //private LevelManager _levelManager = null;

    public float BatteryLeft { get; private set; } = 100f;
    
    private void Start()
    {
        _batteryBar.fillAmount = 1f;
    }

    void Update()
    {
        _batteryBar.fillAmount = BatteryLeft / _maxBattery;
    }

    public bool ConsumeBattery()
    {
        BatteryLeft -= _batteryDecay;

        if (BatteryLeft <= 0)
        {
            BatteryLeft = 0f;
            return false;
        }

        return true;
    }

    public void RefillBattery(float batteryLeft)
    {
        BatteryLeft = batteryLeft;

        if (BatteryLeft > _maxBattery)
            BatteryLeft = _maxBattery;
    }

    public void SetMaxBattery(float max)
    {
        _maxBattery = max;
        BatteryLeft = _maxBattery;
    }
}