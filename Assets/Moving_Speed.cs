using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Speed : MonoBehaviour
{
    #region limit Data
    public class limit
    {
        private Vector4 _Limit = new Vector4();

        public limit(float minX, float maxX, float minY, float maxY)
        {
            _Limit = new Vector4(minX, maxX, minY, maxY);
        }

        public limit(Vector4 l)
        {
            _Limit = l;
        }

        public float minX
        {
            get
            {
                return _Limit.x;
            }
        }

        public float maxX
        {
            get
            {
                return _Limit.y;
            }
        }

        public float minY
        {
            get
            {
                return _Limit.z;
            }
        }

        public float maxY
        {
            get
            {
                return _Limit.w;
            }
        }

        public void Log()
        {
            Debug.Log("_Limit : " + _Limit);
        }
    }
    #endregion

    #region Static
    public static Moving_Speed player = null;
    #endregion

    #region Public
    public Transform Sprite = null;
    public SpriteRenderer m_SpriteRenderer = null;
    public float MoveSpeed = 3.5f;
    public float jumpForce = 750.0f;
    #endregion

    #region  Private
    private Rigidbody2D m_Rigidbody2D = null;
    private Animator m_Animator = null;
    private BuildingScript Building = null;
    private limit limitData = new limit(-11, 100, -5, -3.3f);
    private Vector4 _limitInfo = new Vector4(-11, 100, -5, -3.3f);
    private Vector2 screenBounds;
    private int CurrentBuildingLevel = 0;
    private bool isGrounded = true;
    #endregion

    #region Value
    public bool isMove { get; private set; } = false;

    public bool inBuilding { get; private set; } = false;
    #endregion

    #region ReadOnly
    private readonly float _g = 50f;
    #endregion

    #region Unity Method
    private void Awake()
    {
        player = this;
    }

    void Start()
    {
        m_Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //跳躍控制
        JumpEvent();

        //移動控制
        MoveEvent();

        //最大位移限制
        LimitPosition();
    }
    #endregion

    #region limitPosition
    private void LimitPosition()
    {
        Vector3 pos = transform.position;

        if (transform.position.x >= limitData.maxX)
        {
            pos.Set(limitData.maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= limitData.minX)
        {
            pos.Set(limitData.minX, transform.position.y, transform.position.z);
        }

        transform.position = pos;

        if (isGrounded)
        {
            if (transform.position.y >= limitData.maxY)
            {
                pos.Set(transform.position.x, limitData.maxY, transform.position.z);
            }
            else if (transform.position.y <= limitData.minY)
            {
                pos.Set(transform.position.x, limitData.minY, transform.position.z);
            }
        }

        transform.position = pos;
    }
    #endregion

    #region MoveEvent
    private void MoveEvent()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(true, MoveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(false, -MoveSpeed, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            Move(m_SpriteRenderer.flipX, 0, 0);
        }

        if (isGrounded)
        {
            if (!inBuilding)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Move(m_SpriteRenderer.flipX, 0, MoveSpeed * .75f);

                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    Move(m_SpriteRenderer.flipX, 0, -MoveSpeed * .75f);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    Move(m_SpriteRenderer.flipX, 0, 0);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (++CurrentBuildingLevel >= Building.LevelCount)
                    {
                        CurrentBuildingLevel = Building.LevelCount - 1;
                    }

                    limitData = Building.getLeveData(CurrentBuildingLevel);

                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (--CurrentBuildingLevel < 0)
                    {
                        CurrentBuildingLevel = 0;
                    }

                    limitData = Building.getLeveData(CurrentBuildingLevel);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    Move(m_SpriteRenderer.flipX, 0, 0);
                }
            }
        }      
    }
    #endregion

    #region JumpEnent
    private void JumpEvent()
    {
        if (inBuilding)
        {
            return;
        }

        m_Animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_Animator.SetTrigger("Jump");
            StartCoroutine("Jump_movement");
        }
    }
    #endregion

    #region setAnimation
    private void Move(bool flip, float x, float y)
    {
        m_SpriteRenderer.flipX = flip;
        isMove = x != 0 || y != 0;
        m_Animator.SetBool("isMove", isMove);

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }
    #endregion

    #region Jumping
    IEnumerator Jump_movement()
    {
        float j = jumpForce;  /*加速度的值*/

        Vector3 Old_Position = Sprite.localPosition;

        Sprite.localPosition += Vector3.up * 0.0002f;
        isGrounded = false;

        while (Sprite.localPosition.y > Old_Position.y)
        {
            j = j - _g * Time.deltaTime;

            this.Sprite.localPosition += Vector3.up * j * Time.deltaTime;
            yield return null;
        }

        isGrounded = true;

        Sprite.localPosition = Old_Position;
    }
    #endregion

    #region CheckIn House
    public void CheckInHouse(limit l, BuildingScript building)
    {
        Building = building;
        inBuilding = true;
        limitData = l;
        m_SpriteRenderer.sortingOrder = 2;
    }

    public void LeaveHouse()
    {
        Building = null;
        inBuilding = false;
        limitData = new limit(_limitInfo);
        m_SpriteRenderer.sortingOrder = 4;
        CurrentBuildingLevel = 0;
    }
    #endregion
}
