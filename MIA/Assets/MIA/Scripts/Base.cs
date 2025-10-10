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
            //Âûçâàòü çâóê îäîáðåíèÿ îò èíñïåêòîðà
            Debug.Log("Âû ñîîáùèëè î ïðèáûòèè");
        }
    }

    public void InspectionDTP()
    {
        if(!inspectionDTPComplite && radioComplite)
        {
            inspectionDTPComplite = true;
            //Âûçâàòü çâóê îäîáðåíèÿ îò èíñïåêòîðà
            Debug.Log("Âû ñîîáùèëè î ïðèáûòèè");
        }
        else
        {
            //Âûçâàòü çâóê íåîäîáðåíèÿ
            Debug.Log("Âû íå âûïîëíèëè ïðåäûäóùèå äåéñòâèÿ");
        }
    }

    public void InstallationConeRed()
    {
        if (!installationConeRedComplite && inspectionDTPComplite)
        {
            installationConeRedComplite = true;
            //Âûçâàòü çâóê îäîáðåíèÿ îò èíñïåêòîðà
            Debug.Log("Âû óñòàíîâèëè êðàñíûé êîíóñ");
        }
        else
        {
            //Âûçâàòü çâóê íåîäîáðåíèÿ
            Debug.Log("Âû íå âûïîëíèëè ïðåäûäóùèå äåéñòâèÿ");
        }
    }

    public void InstallationConeOrange()
    {
        if(!installationConeOrangeComplite && installationConeRedComplite)
        {
            installationConeOrangeComplite = true;
            //Âûçâàòü çâóê îäîáðåíèÿ îò èíñïåêòîðà
            Debug.Log("Âû ðàññòàâèëè îðàíæåâûå êîíóñû");
        }
        else
        {
            //Âûçâàòü çâóê íåîäîáðåíèÿ
            Debug.Log("Âû íå âûïîëíèëè ïðåäûäóùèå äåéñòâèÿ");
        }
    }

    public void MakePhoto()
    {
        if (!makePhotoCompition && installationConeOrangeComplite)
        {
            installationConeOrangeComplite = true;
            //Âûçâàòü çâóê îäîáðåíèÿ îò èíñïåêòîðà  
            Debug.Log("Âû ñäåëàëè ôîòî óëèöè, ïåðåõîäèòå ê çàìåðàì");
        }
        else
        {
            //Âûçâàòü çâóê íåîäîáðåíèÿ
            Debug.Log("Âû íå âûïîëíèëè ïðåäûäóùèå äåéñòâèÿ");
        }
    }

}
