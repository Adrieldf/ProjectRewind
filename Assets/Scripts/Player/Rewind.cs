using Assets.Scripts.Infrastructure;
using TMPro;
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
    [SerializeField]
    private TextMeshProUGUI _textMesh;
    [SerializeField]
    private AudioSource _audioSource = null;
    [SerializeField]
    private AudioClip _rewindSFX = null;
    [SerializeField]
    private GameObject _rewindPanel = null;

    private int _rewindCount = 3;

    public bool IsRewinding { get; private set; } = false;

    void Start()
    {
        SetProperties(_capacity, _rewindCount);
    }

    public void SetProperties(int capacity, int rewindCount)
    {
        _rewindCount = rewindCount;
        _capacity = capacity;
        _positionStack = new LimitedStack<PositionState>(_capacity);
        UpdateRewindsCounterText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Rewind"))
        {
            IsRewinding = true;
        }
        if (Input.GetButtonUp("Rewind"))
        {
            IsRewinding = false;
            _audioSource.loop = false;
            _audioSource.Stop();
            _rewindPanel.SetActive(false);
            UpdateRewindsCounter();
        }
    }

    void FixedUpdate()
    {
        if (IsRewinding)
            RewindPosition();
        else
            SavePosition(transform.position);
    }

    private void UpdateRewindsCounter()
    {
        if (_rewindCount > 0)
        {
            _rewindCount--;
            UpdateRewindsCounterText();
        }
    }

    private void UpdateRewindsCounterText()
    {
        _textMesh.text = $"Rewinds left: {_rewindCount}";
    }

    private void RewindPosition()
    {
        if (_rewindCount > 0)
        {
            if (_audioSource.clip != _rewindSFX || !_audioSource.isPlaying)
            {
                _audioSource.clip = _rewindSFX;
                _audioSource.loop = true;
                _audioSource.Play();
                _rewindPanel.SetActive(true);
            }

            var lastState = GetLastState();

            if (lastState.Position != Vector3.zero)
            {
                transform.position = lastState.Position;
                _battery.RefillBattery(lastState.BatteryLeft);
            }
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