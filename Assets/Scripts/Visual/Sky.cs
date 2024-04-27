using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField]
    private Vector2 yBound; // x: min, y: max
    [SerializeField]
    private int maxCountOfCloud;
    private int countOfCloud;
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private Sprite[] cloudSprites;
    private float lastCameraX;
    private Transform self;

    void Start()
    {
        this.self = transform;
    }

    void Update()
    {
        // calculate camera x velocity
        var currCameraX = Camera.main.transform.position.x;
        var cameraVelocity = (currCameraX - this.lastCameraX) * Time.deltaTime;
        this.lastCameraX = currCameraX;
        if(this.countOfCloud < this.maxCountOfCloud)
        {
            // calculate cloud position
            var cloudPos = Vector3.zero;
            cloudPos.y = Random.Range(this.yBound.x, this.yBound.y);
            cloudPos.x = Camera.main.ViewportToWorldPoint(Vector3.right * (cameraVelocity < 0.005f ? -0.2f : 1.2f)).x;
            var go = Instantiate(this.cloud, cloudPos, Quaternion.identity, this.self);
            // setup cloud
            this.countOfCloud++;
            go.GetComponent<Cloud>()
                .OnDisappear
                .AddListener(() =>
                {
                    this.countOfCloud--;
                });
            // setup cloud sprite
            go.GetComponent<SpriteRenderer>().sprite = this.cloudSprites[Random.Range(0, this.cloudSprites.Length)];
        }
    }
}