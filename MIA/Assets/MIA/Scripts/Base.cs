using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base instance;
    public static Base Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Base>();
            return instance;
        }
    }
    private  bool radioComplite = false;
    private bool inspectionDTPComplite = false;
    private bool installationConeRedComplite = false;
    private bool installationConeOrangeComplite = false;
    private bool makePhotoCompition = false;

    public void Radio()
    {
        if(!radioComplite)
        {
            radioComplite = true;
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы сообщили о прибытии");
        }
    }

    public void InspectionDTP()
    {
        if(!inspectionDTPComplite && radioComplite)
        {
            inspectionDTPComplite = true;
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы сообщили о прибытии");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущие действия");
        }
    }

    public void InstallationConeRed()
    {
        if (!installationConeRedComplite && inspectionDTPComplite)
        {
            installationConeRedComplite = true;
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы установили красный конус");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущие действия");
        }
    }

    public void InstallationConeOrange()
    {
        if(!installationConeOrangeComplite && installationConeRedComplite)
        {
            installationConeOrangeComplite = true;
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы расставили оранжевые конусы");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущие действия");
        }
    }

    public void MakePhoto()
    {
        if (!makePhotoCompition && installationConeOrangeComplite)
        {
            installationConeOrangeComplite = true;
            //Вызвать звук одобрения от инспектора  
            Debug.Log("Вы сделали фото улици, переходите к замерам");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущие действия");
        }
    }

}
