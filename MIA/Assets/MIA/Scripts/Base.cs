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
            Debug.Log("Вы проверили участников ДТП");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы еще не сообщили о прибытии");
        }
    }

    public void InstallationConeRed()
    {
        if (!installationConeRedComplite && inspectionDTPComplite)
        {
            installationConeRedComplite = true;
            //Вызвать звук одобрения от инспектора
            Debug.Log("Вы установили красный конус в месте столкновения");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущий шаг");
        }
    }

    public void InstallationConeOrange()
    {
        if(!installationConeOrangeComplite && installationConeRedComplite)
        {
            installationConeOrangeComplite = true;
            //Вызвать звук одобрения от инспектора  
            Debug.Log("Вы установили оранжевые конусы");
        }
        else
        {
            //Вызвать звук неодобрения
            Debug.Log("Вы не выполнили предыдущий шаг");
        }
    }

}
