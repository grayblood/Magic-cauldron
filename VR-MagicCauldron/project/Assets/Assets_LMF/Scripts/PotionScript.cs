using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    [SerializeField]
    Potion_Data data;

    [SerializeField]
    MeshRenderer p_Liquid;
    [SerializeField]
    TextMeshProUGUI potion_Name;

    public string GetName()
    {
        return data.p_Name;
    }
    public Potion_Data GetPData()
    {
        return data;
    }
    public void SetDataPotion(Object_Data pD)
    {
        data = (Potion_Data) pD;
        name = "Potion";
        p_Liquid.material = data.potion_Material;
        potion_Name.text = data.p_Name;
    }

}
