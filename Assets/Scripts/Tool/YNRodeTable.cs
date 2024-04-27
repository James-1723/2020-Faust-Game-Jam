using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class YNRodeTable : MonoBehaviour
{
#if UNITY_EDITOR

    #region Serialize Field
    [SerializeField]
    private Sprite[] sprites = null;
    [SerializeField]
    private Vector3 spacing = Vector3.zero;
    #endregion

    #region Init
    private void Init()
    {
        if (!this.enabled)
        {
            return;
        }

        if (transform.childCount == 0)
        {
            return;
        }

        if (sprites.Length == 0)
        {
            Debug.LogError("Need sprite !!");
            return;
        }

        Vector3 pos = transform.GetChild(0).position;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = pos + spacing * i;

            if (transform.GetChild(i).GetComponent<SpriteRenderer>())
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = Random.Range(0, 100) < 30f;
            }
        }

        this.enabled = false;
    }
    #endregion

    #region Unity Method   
    private void Update()
    {
        Init();
    }
    #endregion

#endif
}
