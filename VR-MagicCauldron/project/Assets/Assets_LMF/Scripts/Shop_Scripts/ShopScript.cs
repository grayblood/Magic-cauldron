using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class ShopScript : MonoBehaviour
{
    //Other things
    [SerializeField]
    int max_Spawn = 5;


    //Ingredients things

    [SerializeField]
    GameObject ingredientUI;
    [SerializeField]
    Transform exitPos;

    

    //money things
    [SerializeField] private TextMeshProUGUI money_Text;
    // canvas
    [SerializeField]
    Transform content;


    private void Start()
    {
        updateText();
        ReloadItems();
    }


    public void ReloadItems()
    {
        Debug.Log("netejant");
        foreach (Transform child in content)
            Destroy(child.gameObject);

        Debug.Log("creant botons");
        foreach (Ingredient_Data data in GC_Script.Instance.GetComponent<Database_Ingredients>().ingre_Data)
        {
            if (data.avalaible)
            {
               GameObject ui_Ingre = Instantiate(ingredientUI, content);
                ui_Ingre.GetComponent<UI_Ingredient_Script>().Load(data, exitPos.position,max_Spawn);
            }
        }
    }


    //debug Buttons borrar en Sprint 4
    public void UpdateMoney(int a)
    {
        GC_Script.Instance.ChangeMoneyValues(a);
        updateText();
    }
    public void updateText()
    {
        money_Text.text = GC_Script.Instance.gold.ToString();
    }
}
