using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Trigger : MonoBehaviour
{
    public static int number = 0;
    public GameObject soundObject;    /*public GameObject Scoremanager;*/
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.tag == "Player")
        {
            Debug.Log(other.name);
            Instantiate(soundObject, transform.position, Quaternion.identity);
            Destroy (gameObject);
            number +=1 ;
        }
    }





}

    