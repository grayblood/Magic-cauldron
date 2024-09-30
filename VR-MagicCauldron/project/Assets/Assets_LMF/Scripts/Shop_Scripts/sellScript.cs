using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellScript : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PotionScript>())
        {
            GC_Script.Instance.ChangeMoneyValues(other.GetComponent<PotionScript>().GetPData().money_Sell);
            Destroy(other.gameObject);
        }
    }

   
}
