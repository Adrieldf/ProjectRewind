using Assets.Scripts.Infrastructure;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [SerializeField]
    private int _capacity = 200;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    private LimitedStack<PositionState> _positionStack;
    [SerializeField]
    private Battery _battery = null;
    
    public bool IsRewinding { get; private set; } = false;

    void Start()
    {
        _positionStack = new LimitedStack<PositionState>(_capacity);
    }

    void Update()
    {
        if (Input.GetButtonDown("Rewind"))
            IsRewinding = true;
        if (Input.GetButtonUp("Rewind"))
            IsRewinding = false;
    }

    void FixedUpdate()
    {
        if (IsRewinding)
            RewindPosition();
        else
            SavePosition(transform.position);
    }

    private void RewindPosition()
    {
        var lastState = GetLastState();
        
        if (lastState.Position != Vector3.zero)
        {
            transform.position = lastState.Position;
            _battery.RefillBattery(lastState.BatteryLeft);
        }
    }

    public void SavePosition(Vector3 position)
    {
        if (_rigidbody.velocity.x != 0 || _rigidbody.velocity.y != 0)
            _positionStack.Push(new PositionState(position, _battery.BatteryLeft));
    }

    private PositionState GetLastState()
    {
        if (_positionStack.HasItems())
            return _positionStack.Pop();

        return PositionState.CreateDefault();
    }
}