using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSystemBM_Script : MonoBehaviour
{
    //-----------------------------------------------------------------------
    //---------------------------Imports-------------------------------------
    //-----------------------------------------------------------------------

    [SerializeField] List<PressureCanvas_Script> pressureCanvas = new List<PressureCanvas_Script>();

    [SerializeField] Image totalImage;

    //-----------------------------------------------------------------------
    //------------------------Variables--------------------------------------
    //-----------------------------------------------------------------------

    int totalSafe;

    //-----------------------------------------------------------------------
    //---------------------------Events--------------------------------------
    //-----------------------------------------------------------------------

    
    [SerializeField] GameEvent On_EndingMinigameBM;

    private void Awake()
    {
        totalImage.fillAmount = 0;
        totalSafe = 0;
    }
    private void Update()
    {
        switch (totalSafe)
        {
            case 0:
                break;
            case 1:
                totalImage.fillAmount += 0.0001f;
                break;
            case 2:
                totalImage.fillAmount += 0.0003f;
                break;
            case 3:
                totalImage.fillAmount += 0.0005f;
               // totalImage.fillAmount += 0.05f;
                break;
            default:
                Debug.Log("Add more Statements to the switchCase");
                break;
        }

        if (totalImage.fillAmount >= 1)
        {
            ResetProgress();
            On_EndingMinigameBM.Raise();
        }
    }


    //GameEvent CheckGreen

    public void UpdateProgress()
    {
        totalSafe = 0;
        foreach (PressureCanvas_Script pCanvas in pressureCanvas)
        {
            if (pCanvas.safe)
            {
                totalSafe++;
            }
        }
    }

    public void ResetProgress()
    {
        totalSafe = 0;
        totalImage.fillAmount = 0;
    }

}
