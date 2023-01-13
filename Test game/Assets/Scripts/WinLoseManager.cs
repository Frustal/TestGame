using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;

    private void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _winText.SetActive(false);
        _loseText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishCube"))
        {
            _playerController._isEnd = true;
            _winText.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            _playerController._isEnd = true;
            _loseText.SetActive(true);
        }
    }
}
