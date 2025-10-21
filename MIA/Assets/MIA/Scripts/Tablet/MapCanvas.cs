using System;
using Radishmouse;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class MapCanvas : MonoBehaviour
{
    public GameObject playerMarker;
    public GameObject tablet;
    public GameObject circleImg1;
    public GameObject circleImg2;
    public GameObject circle1;
    public GameObject circle2;
    public Roulette roullete;
    public GameObject line;
    public UnityEvent onPlace = new UnityEvent();
    public UnityEvent removeText = new UnityEvent();
    

    void Update()
    {
        playerMarker.GetComponent<RectTransform>().anchoredPosition = 
            new Vector3(tablet.transform.position.x, tablet.transform.position.z, 0);
        playerMarker.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, tablet.transform.eulerAngles.y);
        if (roullete.GetComponent<Roulette>().IsPlacedFirst)
        {
            circleImg1.SetActive(true);
            circleImg1.GetComponent<RectTransform>().anchoredPosition = 
                new Vector3(circle1.transform.position.x, circle1.transform.position.z, 0);
        }
        else if(circleImg1.activeSelf){circleImg1.SetActive(false);}

        if (roullete.GetComponent<Roulette>().IsPlacedSecond)
        {
            onPlace.Invoke();
            circleImg2.SetActive(true);
            circleImg2.GetComponent<RectTransform>().anchoredPosition = 
                new Vector3(circle2.transform.position.x, circle2.transform.position.z, 0);
            line.SetActive(true);
            line.GetComponent<UILineRenderer>().points[0] =  circleImg1.GetComponent<RectTransform>().anchoredPosition;
            line.GetComponent<UILineRenderer>().points[1] =  circleImg2.GetComponent<RectTransform>().anchoredPosition;
        }
        else if (circleImg2.activeSelf)
        {
            removeText.Invoke();
            circleImg2.SetActive(false);
            line.SetActive(false);
        }
    }
}
