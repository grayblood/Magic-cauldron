using Autohand;
using System.Collections;
using UnityEngine;

public class Detect_Ingredient : MonoBehaviour
{
    Ingredient_Script item;

    [SerializeField]
    BrewingMachineScript brewingMachine;
    public void SendIngredientData()
    {
        StartCoroutine(WaitForSomeSeconds(.5f));
    }

    public void RemoveIngredientData()
    {
        if (item != null)
        {
            brewingMachine.RemoveIngredient(item);
            item = null;
        }
    }

    public void DestroyIngredient()
    {
        if (item != null)
        {
            item.GetIData().currentlySpawned--;
            Destroy(item.gameObject);
            item = null;
        }
        
        //GetComponent<PlacePoint>().Remove();
    }
    IEnumerator WaitForSomeSeconds(float ftime)
    {
        yield return new WaitForSeconds(ftime);
        if (GetComponentInChildren<Ingredient_Script>())
        {
            item = GetComponentInChildren<Ingredient_Script>();
            Debug.Log(item.GetName());
            if (item != null)
            {
                brewingMachine.AddIngredient(item);
            }
        }
    }

    public void disableGrab(bool grabb)
    {
        GetComponent<Grabbable>().isGrabbable = grabb;
    }

}
