using System.Collections;
using System.Collections.Generic;
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
    public GameObject textpls;

    

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
            
        }
        else if(circleImg2.activeSelf){circleImg2.SetActive(false);}
    }
}
