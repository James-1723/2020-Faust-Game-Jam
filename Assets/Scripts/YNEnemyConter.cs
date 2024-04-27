using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNEnemyConter : MonoBehaviour
{
    public Transform Min, Max;

    public static Moving_Speed.limit limit { get; private set; }

    private void Awake()
    {
        limit = new Moving_Speed.limit(Min.position.x , Max.position.x, Min.position.y, Max.position.y);
    }
}
