using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject gm;

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        gm.gameObject.SetActive(false);
    }

}
