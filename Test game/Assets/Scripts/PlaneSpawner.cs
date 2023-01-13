using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSpawner : MonoBehaviour
{
    public int planesAmount = 0;
    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private int _maxAngle = 90;
    [SerializeField] private GameObject PlaneHandler;
    [SerializeField] private Text _planeText;
    [SerializeField] private PlaneCollector _planeCollector;

    private int _deletedPlanes = 0;

    private void Start()
    {
        UpdatePlaneText();
    }

    public void SpawnPlane(Vector3 pos, float sliderValue)
    {
        GameObject plane = Instantiate(_planePrefab, pos, Quaternion.identity, PlaneHandler.transform);
        plane.transform.rotation = Quaternion.Euler(0, 0, sliderValue * _maxAngle);
        planesAmount -= 1;
        _deletedPlanes++;
        //delete plane from backpack every n used planes
        if (_deletedPlanes % (_planeCollector.AddAmountGetter() / _planeCollector.TimesAddGetter()) == 0) _planeCollector.DeletePlane();
        UpdatePlaneText();
    }

    public void AddPlanes(int amount)
    {
        planesAmount += amount;
        UpdatePlaneText();
    }

    public void UpdatePlaneText()
    {
        _planeText.text = planesAmount.ToString();
    }
}
