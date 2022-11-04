using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Robber : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;

    private int _rightDirectionY = 0;
    private int _leftDirectionY = 180;
    private int _hitHash = Animator.StringToHash("Hit");
    private int _walkHash = Animator.StringToHash("IsWalk");
    private bool _isWalk;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Rotate(_rightDirectionY);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Rotate(_leftDirectionY);
        }
        float moveX = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        transform.position += Vector3.right * moveX;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minPositionX, _maxPositionX), transform.position.y, transform.position.z);

        if (Mathf.Abs(moveX) > 0)
            _isWalk = true;
        else
            _isWalk = false;

        _animator.SetBool(_walkHash, _isWalk);
    }

    private void Rotate(int rotateAngle)
    {
        Vector3 rotate = transform.eulerAngles;
        rotate.y = rotateAngle;
        transform.rotation = Quaternion.Euler(rotate);
    }

    public void KickDoor()
    {
        _animator.SetTrigger(_hitHash);
        _audioSource.Play();
    }
}
