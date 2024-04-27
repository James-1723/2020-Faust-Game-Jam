using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNCamera : MonoBehaviour
{
    #region SerializeField.
    [SerializeField]
    private Transform Player = null;
    [SerializeField]
    private float speed = 0.01f;
    #endregion

    #region  Private
    private Moving_Speed player = null;
    private float Range = 0;
    #endregion

    #region Unity Method
    private void Start()
    {
        player = Player.GetComponent<Moving_Speed>();
        Range = transform.position.x - Player.position.x;
    }

    void Update()
    {

        if (!player.isMove)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.position.x + Range, transform.position.y, transform.position.z), speed);

    }
    #endregion
}
