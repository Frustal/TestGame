using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _finishCube;

    private float _fullDis;

    private void Start()
    {
        _fullDis = _finishCube.position.x - _player.position.x;
    }

    private void Update()
    {
        float dis =_finishCube.position.x - _player.position.x;
        _slider.value = (_fullDis - dis) / _fullDis;
    }
}
