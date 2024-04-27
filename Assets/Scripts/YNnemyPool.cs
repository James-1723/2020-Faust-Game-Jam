using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNnemyPool : MonoBehaviour
{
    public Transform[] Pos = null;

    private  List<YNEnemy> DeadEnemy = new List<YNEnemy>();

    private bool Listening = false;

    public void Add(YNEnemy obj)
    {
        DeadEnemy.Add(obj);

        if (!Listening && DeadEnemy.Count > 0)
        {
            StartCoroutine("PoolListener");
        }
    }

    private IEnumerator PoolListener()
    {
        Listening = true;
            
        while (DeadEnemy.Count > 0)
        {
            DeadEnemy[DeadEnemy.Count - 1].transform.position = Pos[Random.Range(0, Pos.Length)].position;

            DeadEnemy[DeadEnemy.Count - 1].OnAwake();

            DeadEnemy.Remove(DeadEnemy[DeadEnemy.Count - 1]);

            yield return new WaitForSeconds(6);
        }

        Listening = false;

        yield break; 
    }
}
