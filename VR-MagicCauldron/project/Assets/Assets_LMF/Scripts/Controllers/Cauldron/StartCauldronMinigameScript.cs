using UnityEngine;

public class StartCauldronMinigameScript : MonoBehaviour
{
    [SerializeField]
    GameEvent On_Start;

    public void StartMinigame()
    {
        On_Start.Raise();
        gameObject.SetActive(false);
    }
}
