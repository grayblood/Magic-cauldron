using Autohand;
using System.Collections.Generic;
using UnityEngine;

public class PotionPlace : MonoBehaviour
{
    PotionScript potion;
    [SerializeField]
    List<Transform> pos = new List<Transform>();


    public void StartDecomposePotion()
    {
        if (GetComponentInChildren<PotionScript>())
        {
            potion = GetComponentInChildren<PotionScript>();
            Debug.Log(potion);
            if (potion != null)
            {
                List<Ingredient_Data> ingre_data = GC_Script.Instance.DecomposePotion(potion);
                Debug.Log("Lista" + ingre_data);
                for (int i = 0; i < ingre_data.Count; i++)
                {
                    Debug.Log(ingre_data[i]);
                    spawnObject(ingre_data[i], pos[i].position);
                    GetComponent<AudioSource>().Play();
                }
                Destroy(potion.gameObject);
                potion = null;
            }
        }
    }

    void spawnObject(Ingredient_Data ingre, Vector3 pos)
    {
        Debug.Log("spawneando");
        Instantiate(ingre.ingredient, pos, Quaternion.identity);
        ingre.currentlySpawned++;
        GetComponent<PlacePoint>().Remove();
    }
}

