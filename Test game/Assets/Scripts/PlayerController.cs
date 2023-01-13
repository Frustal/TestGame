using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private int _speed;

    [Header("UI")]
    [SerializeField] private Slider _slider;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private bool _isGrounded;

    [Header("Spawner")]
    [SerializeField] private float _delay;
    [SerializeField] private float _offset;

    //game end
    public bool _isEnd { get; set; } = false;

    private float x, y;
    private PlaneSpawner _planeSpawner;
    private bool _spawnStarted = false;

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        _planeSpawner = gameObject.GetComponent<PlaneSpawner>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        x = _speed * Time.deltaTime;
        DifferentMovements();

        gameObject.transform.position += new Vector3(x , y, 0);
    }

    private void DifferentMovements()
    {
        if (!_isEnd)
        {
            if (Input.touchCount > 0 && _planeSpawner.planesAmount > 0)
            {
                if (_isGrounded && _slider.value > 0)
                {
                    y = _slider.value * _speed * Time.deltaTime;
                }
                else if (_isGrounded && _slider.value < 0)
                {
                    y = 0;
                }
                else if (!_isGrounded)
                {
                    y = _slider.value * _speed * Time.deltaTime;
                }

                if (!_spawnStarted)
                {
                    StartCoroutine(SpawnPlanes());
                }
            }
            else
            {
                StopAllCoroutines();
                _spawnStarted = false;

                if (_isGrounded)
                {
                    y = 0;
                }
                else
                {
                    //falling
                    y = -1 * _speed * Time.deltaTime;
                }
            }
        }
        else
        {
            x = 0;
            y = 0;
        }
    }

    private IEnumerator SpawnPlanes()
    {
        _spawnStarted = true;

        _planeSpawner.SpawnPlane(_groundCheck.position + new Vector3(_speed * Time.deltaTime + _offset, 0, 0), _slider.value);
        yield return new WaitForSeconds(_delay);

        StartCoroutine(SpawnPlanes());
    }
}
