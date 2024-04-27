using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNLayerTool : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer = null;

    void Update()
    {
        if (transform.position.y >  -1)
        {
            SpriteRenderer.sortingOrder = 2;
        }
        else if (transform.position.y <= -1 && transform.position.y > -3.5)
        {
            SpriteRenderer.sortingOrder = 4;
        }
        else if (transform.position.y <= -3.5 && transform.position.y > -4f)
        {
            SpriteRenderer.sortingOrder = 5;
        }
        else if (transform.position.y <= -4 && transform.position.y > -4.5f)
        {
            SpriteRenderer.sortingOrder = 6;
        }
        else if (transform.position.y <= -4.5 && transform.position.y > -5f)
        {
            SpriteRenderer.sortingOrder = 7;
        }
    }
}
