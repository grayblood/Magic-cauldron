using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfoReceta : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI i_Name;
    [SerializeField] Image i_Image;

    public void SetInformation(Ingredient_Data data)
    {
        i_Name.text = data.i_Name;
        i_Image.sprite = data.image;
    }
}
