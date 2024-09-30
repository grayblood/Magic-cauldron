using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingredient_Script : MonoBehaviour
{
    private Ingredient_Data ingredientData;
    public Image image;
    public TextMeshProUGUI name_Text;
    public Button button;
    public TextMeshProUGUI button_Text;
    [SerializeField] GameEvent effect;

    public void Load(Ingredient_Data data, Vector3 pos,int maxSpawn)
    {
        ingredientData = data;
        image.sprite = data.image;
        name_Text.text = data.i_Name;
        button_Text.text = data.price.ToString();
        button.onClick.AddListener(() => spawnObject(data,pos,maxSpawn));
        
    }

    public void spawnObject(Ingredient_Data ingre,Vector3 pos, int max_Spawn)
    {
        if (ingre.currentlySpawned < max_Spawn && GC_Script.Instance.gold >= ingre.price)
        {

            GC_Script.Instance.ChangeMoneyValues(-ingre.price);
            effect.Raise();
            GameObject a  = Instantiate(ingre.ingredient, pos, Quaternion.identity);
            a.name = "Ingredient";
            ingre.currentlySpawned++;
        }
    }


}
