using System;
using System.Collections;
using System.Collections.Generic;
using Radishmouse;
using TMPro;
using UnityEngine;


public class MapCanvas : MonoBehaviour
{
    public GameObject playerMarker;
    public GameObject tablet;
    public GameObject circleImg1;
    public GameObject circleImg2;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject roullete;
    public GameObject distanceText;
    public GameObject line;

    

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
            circleImg2.SetActive(true);
            circleImg2.GetComponent<RectTransform>().anchoredPosition = 
                new Vector3(circle2.transform.position.x, circle2.transform.position.z, 0);
            line.SetActive(true);
            line.GetComponent<UILineRenderer>().points[0] =  circleImg1.GetComponent<RectTransform>().anchoredPosition;
            line.GetComponent<UILineRenderer>().points[1] =  circleImg2.GetComponent<RectTransform>().anchoredPosition;
            distanceText.SetActive(true);
            distanceText.GetComponent<TextMeshPro>().text = roullete.GetComponent<Roulette>().DistanceBetweenPoints.ToString();
            distanceText.GetComponent<RectTransform>().anchoredPosition =
                (circleImg1.GetComponent<RectTransform>().anchoredPosition +
                circleImg2.GetComponent<RectTransform>().anchoredPosition)/2;
            distanceText.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, (float)Math.Atan2(circle2.transform.position.y-circle1.transform.position.y, circle2.transform.position.x-circle1.transform.position.x));
        }
        else if (circleImg2.activeSelf)
        {
            circleImg2.SetActive(false);
            line.SetActive(false);
            distanceText.SetActive(false);
        }
    }
}
