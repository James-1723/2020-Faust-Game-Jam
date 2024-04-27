using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{

    public SpriteRenderer randerer = null;

    [Range(0.02f,100f)]
    public float Speed = 0.02f;
    private readonly float A_min = 40 / 255 ; 
    private readonly float A_max = 255 / 255 ; 

    // Start is called before the first frame update
    void Start()
    {
        //randerer.color = color;
        StartCoroutine("Show");
    }


    public void setColorA(bool open)
    { 
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        while(randerer.color.a < A_max)
        {
            randerer.color  += new Color( 0,0, 0, Speed);
            yield return new WaitForSeconds(0.03f);
        }

        yield break;
    }

    
    IEnumerator Hide()
    {
        while(randerer.color.a > A_min)
        {
            randerer.color  -= new Color( 0,0, 0, Speed);
            yield return new WaitForSeconds(0.03f);
        }
        yield break;
    }
}
