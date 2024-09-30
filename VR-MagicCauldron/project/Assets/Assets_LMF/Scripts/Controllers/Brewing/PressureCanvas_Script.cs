using System.Collections;
using UnityEngine;

public class PressureCanvas_Script : MonoBehaviour
{
    [SerializeField] RectTransform rT_Selector;

    [SerializeField] Transform g_Valve;

    public bool safe;
    private bool untouched, chaos;

    public float speed = 0.01f;

    [SerializeField] GameEvent On_UpdateInfo;

    private void Awake()
    {
        rT_Selector.anchoredPosition = new Vector2(0, 0);
        safe = true;
        resetSelector();
        untouched = true;
    }



    private void Update()
    {
        CheckSelector();
        MoveSelector();
        CreateRandomness();
    }
    //GameEvent Randomness
    public void CreateRandomness()
    {
        if (chaos)
        {
            if (rT_Selector.anchoredPosition.x >= 0)
            {
                rT_Selector.anchoredPosition += new Vector2(Time.deltaTime * 0.01f, 0);
            }
            else if (rT_Selector.anchoredPosition.x < 0)
            {
                rT_Selector.anchoredPosition += new Vector2(Time.deltaTime * -0.01f, 0);
            }
        }
    }

    //GameEvent StartMinigame
    public void resetSelector()
    {
        rT_Selector.anchoredPosition = new Vector2(-0.25f, 0);
    }
    public void randomPos()
    {
        rT_Selector.anchoredPosition = new Vector2(Random.Range(-0.25f,0.25f), 0);
        ChangeUntouched(true);
    }
    public void ChangeUntouched(bool i)
    {
        untouched = i;
        if (untouched)
        {
            StartCoroutine(StartUntouched());
        }
        else if(!untouched)
        {
            chaos = false;
            StopAllCoroutines();
        }
    }

    IEnumerator StartUntouched()
    {
        yield return new WaitForSeconds(5);
        chaos = true;
        StopAllCoroutines();
    }
    void CheckSelector()
    {
        if (rT_Selector.anchoredPosition.x < -0.25f)
        {
            rT_Selector.anchoredPosition = new Vector2(-0.25f, 0);
        }
        if (rT_Selector.anchoredPosition.x > 0.25f)
        {
            rT_Selector.anchoredPosition = new Vector2(0.25f, 0);

        }

        if (rT_Selector.anchoredPosition.x < 0.1 && rT_Selector.anchoredPosition.x > -0.1)
        {
            safe = true;
            On_UpdateInfo.Raise();
        }
        else
        {
            safe = false;
            On_UpdateInfo.Raise();
        }
    }
    void MoveSelector()
    {
        float rotationspeed = g_Valve.rotation.x * Time.deltaTime * speed;
        rT_Selector.anchoredPosition += new Vector2(rotationspeed, 0);

       

       /* if (g_Valve.rotation.x > 0.1)
        {
            rT_Selector.anchoredPosition += new Vector2(speed, 0);
        }
        else if (g_Valve.rotation.x < -0.1)
        {
            rT_Selector.anchoredPosition -= new Vector2(speed, 0);
        }*/
    }


    public void ResetValvePosition()
    {
        g_Valve.Rotate(0, 0, 0);
    }
}
