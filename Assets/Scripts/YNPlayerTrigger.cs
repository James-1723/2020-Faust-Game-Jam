using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNPlayerTrigger : MonoBehaviour
{
    [SerializeField]
    private YNPlayerEvent PlayerEvent = null;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        switch (obj.tag)
        {
            case "Building":
                PlayerEvent.setButtonEvent(obj.GetComponent<BuildingScript>().Event);
                break;

            case "Washer":
                if (Coin_Trigger.number == 3)
                {
                    PlayerEvent.setButtonEvent(obj.GetComponent<Winning>().Won);
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        switch (obj.tag)
        {
            case "Building":
                PlayerEvent.clearButtonEvent();
                break;
            default:
                break;
        }
    }
}
