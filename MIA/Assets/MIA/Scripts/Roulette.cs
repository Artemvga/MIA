using System;
using UnityEngine;
using Valve.VR;

public class Roulette : MonoBehaviour
{
    public LineRenderer line;
    public GameObject firstPoint;
    public GameObject secondPoint;
    private float distanceBetweenPoints;
    public float DistanceBetweenPoints {get {return distanceBetweenPoints;}}
    [Space]
    public bool useYToRoullete;

    private SteamVR_Action_Boolean action = SteamVR_Input.actionsBoolean[0];

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isPlacedFirst)
            {
                firstPoint.transform.position = gameObject.transform.position;
                _isPlacedFirst = true;
            }
            else if (!_isPlacedSecond)
            {
                secondPoint.transform.position = gameObject.transform.position;
                _isPlacedSecond = true;
                line.SetPosition(0, firstPoint.transform.position);
                line.SetPosition(1, secondPoint.transform.position);

                var v1 = line.GetPosition(0);
                var v2 = line.GetPosition(1);
                if (!useYToRoullete)
                {
                    distanceBetweenPoints = Mathf.Sqrt(Mathf.Pow((v1.x - v2.x), 2) + Mathf.Pow((v1.z - v2.z), 2));
                    Debug.Log(distanceBetweenPoints);
                }
                else
                {
                    distanceBetweenPoints = Vector3.Distance(v1, v2);
                    Debug.Log(distanceBetweenPoints);
                }

                if (distanceBetweenPoints > 19 && distanceBetweenPoints < 24)
                {
                    Base.instance.RoulleteMake();
                }
            }
            else
            {
                _isPlacedFirst = false;
                _isPlacedSecond = false;
                firstPoint.transform.position = new Vector3(0f, -10f, 0f);
                secondPoint.transform.position = new Vector3(0f, -10f, 0f);
                line.SetPosition(0, firstPoint.transform.position);
                line.SetPosition(1, secondPoint.transform.position);
            }
        }
#endif
    }

    private void OnEnable()
    {
        line.positionCount = 2;

        foreach (var el in SteamVR_Input.actionsBoolean)
        {
            if (el.GetShortName() == "TryTakePhoto")
            {
                action = el;
                break;
            }
        }
    }

    private bool _isPlacedFirst;
    private bool _isPlacedSecond;
    public bool IsPlacedFirst { get {return _isPlacedFirst;}}
    public bool IsPlacedSecond { get {return _isPlacedSecond;}}
    public void Checking()
    {
        if (action.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (!_isPlacedFirst)
            {
                firstPoint.transform.position = gameObject.transform.position;
                _isPlacedFirst = true;
            }
            else if (!_isPlacedSecond)
            {
                secondPoint.transform.position = gameObject.transform.position;
                _isPlacedSecond = true;
                line.SetPosition(0, firstPoint.transform.position);
                line.SetPosition(1, secondPoint.transform.position);

                var v1 = line.GetPosition(0);
                var v2 = line.GetPosition(1);
                if (!useYToRoullete)
                {
                    distanceBetweenPoints = Mathf.Sqrt(Mathf.Pow((v1.x - v2.x), 2) + Mathf.Pow((v1.z - v2.z), 2));
                    Debug.Log(distanceBetweenPoints);
                }
                else
                {
                    distanceBetweenPoints = Vector3.Distance(v1, v2);
                    Debug.Log(distanceBetweenPoints);
                }
                
                if (distanceBetweenPoints > 19 && distanceBetweenPoints < 24)
                {
                    Base.instance.RoulleteMake();
                }
            }
            else
            {
                _isPlacedFirst = false;
                _isPlacedSecond = false;
                firstPoint.transform.position = new Vector3(0f, -10f, 0f);
                secondPoint.transform.position = new Vector3(0f, -10f, 0f);
                line.SetPosition(0, firstPoint.transform.position);
                line.SetPosition(1, secondPoint.transform.position);
            }
        }
    }
}
