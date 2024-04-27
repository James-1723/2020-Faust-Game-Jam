using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class YNBuildingTable : MonoBehaviour
{
#if UNITY_EDITOR

    #region Serialize Field
    [SerializeField]
    [Range(-10, 10)]
    private float spacingX = 0;
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

        Vector3 pos = transform.GetChild(0).position;

        for (int i = 1; i < transform.childCount; i++)
        {
            if (i == 0)
            {
                transform.GetChild(0).position = pos;
                break;
            }

            float b1 = transform.GetChild(i).Find("Spine").GetComponent<SpriteRenderer>().bounds.extents.x;
            float b2 = transform.GetChild(i - 1).Find("Spine").GetComponent<SpriteRenderer>().bounds.extents.x;

            pos = transform.GetChild(i - 1).position + new Vector3(b1 + b2 + Random.Range(spacingX -0.5f, spacingX +0.5f), 0, 0);

            if (pos.x < b1 + b2)
            {
                pos = transform.GetChild(i - 1).position + new Vector3(b1 + b2, 0, 0);
            }

            transform.GetChild(i).position = pos;
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
