using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cloud : MonoBehaviour
{
    public UnityEvent OnDisappear;
    [SerializeField]
    private float ttl; // time to live
    [SerializeField]
    private Vector2 speedRange; // x: min, y: max
    private float speed; // moving speed
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Transform self;

    void Start()
    {
        this.speed = Random.Range(this.speedRange.x, this.speedRange.y);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.mainCamera = Camera.main;
        this.self = transform;
        StartCoroutine(this.checkAlive());
    }

    void Update()
    {
        this.self.position += Vector3.right * (this.speed * Time.deltaTime);
    }

    private IEnumerator checkAlive()
    {
        bool outOfView = false;
        while(true)
        {
            // get position of view space
            var viewPos = this.mainCamera.WorldToViewportPoint(this.self.position);
            if(viewPos.x < 0 || viewPos.x > 1)
            {
                if(outOfView)
                    break;
                else
                    outOfView = true;
            }
            yield return new WaitForSeconds(this.ttl);
        }
        this.OnDisappear.Invoke();
        Destroy(gameObject);
    }
}