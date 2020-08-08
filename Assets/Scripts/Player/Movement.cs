using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _jumpVelocity = 7.5f;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _speed = 200;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rewind _rewind;
    [SerializeField]
    private Battery _battery = null;
    [SerializeField]
    private AudioSource _audioSource = null;
    [SerializeField]
    private AudioClip _noBatterySFX = null;
    [SerializeField]
    private AudioClip _walkingSFX = null;
    [SerializeField]
    private AudioClip _jumpingSFX = null;

    private bool _isOnTheFloor;
    private bool _isFacingRight = true;
    private bool _hasBatteryLeft = true;
    private bool _playedWindowsErrorSFX = false;

    void Update()
    {
        CheckJump();

        _animator.SetBool("isOutOfBattery", _battery.BatteryLeft <= 0);
    }

    void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");

        var isMoving = movement != 0 && !_rewind.IsRewinding;

        _animator.SetBool("isWalking", isMoving && _hasBatteryLeft);

        if (isMoving)
        {
            _hasBatteryLeft = _battery.ConsumeBattery();

            if (_hasBatteryLeft)
                PlayWalkingSFX();
            else if (IsPlayingWalkinSFX())
                StopSFX();
        }
        else
        {
            if (!_rewind.IsRewinding && IsPlayingWalkinSFX() && !IsPlayingNoBatterySFX())
                StopSFX();
        }

        if (!_hasBatteryLeft)
            PlayNoBatterySFX();
        else
            _playedWindowsErrorSFX = false;

        MoveHorizontally(_hasBatteryLeft && !_rewind.IsRewinding ? movement : 0f);
    }



    void MoveHorizontally(float movement)
    {
        if (movement > 0 && !_isFacingRight || movement < 0 && _isFacingRight)
            Flip();

        _rigidbody.velocity = new Vector2(movement * Time.deltaTime * _speed, _rigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _isOnTheFloor = !_isOnTheFloor && isOnGround(other) ? true : _isOnTheFloor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isOnGround(other))
            _isOnTheFloor = false;
    }

    private bool isOnGround(Collider2D other) => other.CompareTag("Ground") || other.CompareTag("Crate");

    private void CheckJump()
    {
        var spaceButton = Input.GetButtonDown("Jump");

        if (spaceButton && _isOnTheFloor && _hasBatteryLeft)
        {
            PlayJumpingSFX();
            _rigidbody.velocity = Vector2.up * _jumpVelocity;
            _hasBatteryLeft = _battery.ConsumeBattery();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    #region SFX

    bool IsPlayingWalkinSFX() => _audioSource.clip == _walkingSFX && _audioSource.isPlaying;
    bool IsPlayingJumpingSFX() => _audioSource.clip == _jumpingSFX && _audioSource.isPlaying;
    bool IsPlayingNoBatterySFX() => _audioSource.clip == _noBatterySFX && _audioSource.isPlaying;
    void PlayWalkingSFX()
    {
        if (IsPlayingJumpingSFX() || IsPlayingNoBatterySFX())
            return;

        if (_audioSource.clip != _walkingSFX || !_audioSource.isPlaying)
        {
            _audioSource.clip = _walkingSFX;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
    void PlayNoBatterySFX()
    {
        if (_playedWindowsErrorSFX)
            return;

        _audioSource.clip = _noBatterySFX;
        _audioSource.loop = false;
        _audioSource.Play();
        _playedWindowsErrorSFX = true;

    }
    void PlayJumpingSFX()
    {
        _audioSource.clip = _jumpingSFX;
        _audioSource.loop = false;
        _audioSource.Play();
    }
    void StopSFX()
    {
        _audioSource.clip = null;
        _audioSource.loop = false;
        _audioSource.Stop();
    }
    #endregion
}