using System.Collections.Generic;
using UnityEngine;

public class PlaneCollector : MonoBehaviour
{
    [SerializeField] private int _amountToAdd;
    [SerializeField] private PlaneSpawner _planeSpawner;

    [Header("BackPack System")]
    private List<GameObject> _planesCollected = new();
    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private GameObject _backPack;
    [SerializeField] private Vector3 _offset;
    //times to add planes to backpack
    [SerializeField] private int _timesToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlaneLoot"))
        {
            _planeSpawner.AddPlanes(_amountToAdd);
            AddPlane();
            Destroy(other.gameObject);
        }
    }

    private void AddPlane()
    {
        for (int i = 0; i < _timesToAdd; i++) 
        {
            GameObject plane;
            if (_planesCollected.Count > 0)
            {
                plane = Instantiate(_planePrefab, _planesCollected[_planesCollected.Count - 1].transform.position + _offset, Quaternion.identity, _backPack.transform);
            }
            else
            {
                plane = Instantiate(_planePrefab, _backPack.transform.position + _offset, Quaternion.identity, _backPack.transform);
            }
            _planesCollected.Add(plane);
        }
    }

    public void DeletePlane()
    {
        GameObject plane = _planesCollected[_planesCollected.Count - 1];
        _planesCollected.Remove(plane);
        Destroy(plane);
    }

    public int AddAmountGetter()
    {
        return _amountToAdd;
    }

    public int TimesAddGetter()
    {
        return _timesToAdd;
    }
}
