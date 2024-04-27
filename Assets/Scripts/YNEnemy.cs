using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region EnemyType
public enum EnemyType
{
    Idle = 50,
    Run = 70,
    Attack = 90
}
#endregion

public class YNEnemy : MonoBehaviour
{
    #region  SerializeField
    [SerializeField]
    YNnemyPool Pool = null;
    [SerializeField]
    private YNEnemyEvent Event = null;
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private float AtrtackRange = 10.0f;
    [SerializeField]
    private float speed = 18.0f;
    [SerializeField]
    private YNBullet Bullet = null;
    [SerializeField]
    private Transform ShootPos = null;
    #endregion

    #region Private 
    private Moving_Speed.limit _limit = null;
    private Transform Player = null;
    private Vector2 runPos;
    private int Hp = 50;
    private int Type = -1;
    private bool colddown = false;
    private bool OnAttack = true;

    private readonly int Hp_info = 50;
    #endregion

    #region Init
    public void Init()
    {
        AtrtackRange = Random.Range(5f, 12f);

        if (Pool == null)
        {
            Pool = (YNnemyPool) GameObject.FindObjectOfType(typeof(YNnemyPool));
        }
        

        Hp = Hp_info;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Event.Init(Shoot, Run, Dead);
        runPos = transform.position;
        _limit = YNEnemyConter.limit;
        StartCoroutine("ModeHub");


        _animator.SetBool("Run", false);
        _animator.SetBool("Attack", false);
    }
    #endregion

    #region OnAwake
    public void OnAwake()
    {
        gameObject.SetActive(true);

        _animator.SetBool("Awake", true);

        Invoke("Init", 1);
        Invoke("setAwakeAni", 1.2f);
    }

    private void setAwakeAni()
    {
        _animator.SetBool("Awake", false);
    }
    #endregion

    #region Unity Method
    private void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        StopCoroutine("Idle");
        StopCoroutine("ModeHub");
    }

    private void OnDestroy()
    {
        StopCoroutine("Idle");
        StopCoroutine("ModeHub");
    }
    #endregion

    #region ModeHub
    IEnumerator ModeHub()
    {
        while (Hp > 0)
        {
            if (!colddown && OnAttack)
            {
                Attack();
            }
            else
            {
                if (Type < (int) EnemyType.Idle)
                {
                    Type = Random.Range(0, 100);
                }

                if (Type < 50)
                {
                    StartCoroutine("Idle");
                }
                else if (Type >= (int) EnemyType.Idle)
                {
                    InitRun();

                    RandomRun();
                }
            }

            yield return new WaitForSeconds(0.03f);
        }

        Dead();

        StopCoroutine("Idle");
        StopCoroutine("ModeHub");

        yield break;
    }
    #endregion

    #region Idle
    IEnumerator Idle()
    {
        Debug.Log("Idle");
        _animator.SetBool("Run", false);
        _animator.SetBool("Attack", false);

        yield return new WaitForSeconds(10);

        Type = -1;

        yield break;
    }
    #endregion

    #region Attack
    private void Attack()
    {
        if (Moving_Speed.player.inBuilding)
        {
            //OnAttack = false;
            colddown = true;

            return;
        }

        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance < AtrtackRange)
        {
            _SpriteRenderer.flipX = Player.position.x > transform.position.x;
            _animator.SetBool("Attack", true);
            _animator.SetBool("Run", false);
        }
        else
        {

            Vector2 vec = ((Vector2) Player.position - (Vector2) transform.position).normalized;
            _SpriteRenderer.flipX = Player.position.x > transform.position.x;
            _animator.SetBool("Run", true);
            _animator.SetBool("Attack", false);

            transform.position += (Vector3) vec * speed * Time.deltaTime;

            if (transform.position.y > _limit.maxY)
            {
                transform.position.Set(transform.position.x, _limit.maxY, transform.position.z);
            }
        }
    }
    #endregion

    #region RandomRun
    private void InitRun()
    {
        if (runPos != (Vector2) transform.position)
        {
            return;
        }

        colddown = false;

        runPos = new Vector2(Random.Range(transform.position.x - AtrtackRange - 10, transform.position.x + AtrtackRange + 10), Random.Range(_limit.minY, _limit.maxY));
    }

    private void RandomRun()
    {
        Debug.Log("RandomRun");

        float distance = Vector2.Distance(transform.position, runPos);

        Vector2 vec = (runPos - (Vector2) transform.position).normalized;

        if (distance > AtrtackRange)
        {

            _SpriteRenderer.flipX = runPos.x > transform.position.x;
            _animator.SetBool("Run", true);
            _animator.SetBool("Attack", false);

            transform.position += (Vector3) vec * 18 * Time.deltaTime;
        }
        else
        {
            runPos = transform.position;
            Type = -1;
        }
    }
    #endregion

    #region Do Event
    private void Shoot()
    {
        YNBullet go = (YNBullet) Instantiate(Bullet, ShootPos.position, ShootPos.rotation);

        go.Shoot((_SpriteRenderer.flipX)? -ShootPos.right : ShootPos.right);

        go.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = _SpriteRenderer.flipX;
    }

    private void Run()
    {
        Debug.Log("腳步聲");
    }

    private void Dead()
    {
        Pool.Add(this);

        _animator.SetInteger("HP", 0);

        Invoke("Hide", 5);
    }
    #endregion

    #region Hide Function
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AtrtackRange);
    }
}