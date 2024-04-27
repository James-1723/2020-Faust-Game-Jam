using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    #region LevelData    
    [System.Serializable]
    public class LevelData
    {
        public Transform Max;
        public Transform min;

        public Moving_Speed.limit Getlimit
        {
            get
            {
                return new Moving_Speed.limit(min.position.x, Max.position.x, min.position.y, Max.position.y);
            }
        }
    }
    #endregion 

    #region SerializeField
    [SerializeField]
    private SpriteRenderer _SpriteRenderer = null;
    [SerializeField]
    private BoxCollider2D Collider = null;
    [SerializeField]
    private LevelData[] _leveldata = null;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float Speed = 0.02f;
    #endregion

    #region public 
    public bool Done { get; private set; } = true;

    public int LevelCount
    {
        get {
            return _leveldata.Length;
        }
    }
    #endregion

    #region private
    private bool checkIn = false;

    private readonly float LeaveAlpha = 1.0f;
    private readonly float CheckInAlpha = 0.35f;
    #endregion

    #region Event
    public void Event()
    {
        if (!Done)
        {
            return;
        }

        checkIn = !checkIn;

        if (checkIn)
        {
            Moving_Speed.player.CheckInHouse(_leveldata[0].Getlimit, this);
        }
        else
        {
            Moving_Speed.player.LeaveHouse();
        }

        StartCoroutine("ChangeAlpha");
    }
    #endregion

    #region ChangeAlpha
    private IEnumerator ChangeAlpha()
    {

        float alpha = (checkIn) ? CheckInAlpha : LeaveAlpha;
        float speed = (checkIn) ? -Speed : Speed;

        bool b = (checkIn) ? _SpriteRenderer.color.a > CheckInAlpha : _SpriteRenderer.color.a < LeaveAlpha;

        Done = false;

        while (b)
        {
            _SpriteRenderer.color += new Color(0, 0, 0, speed);

            if (_SpriteRenderer.color.a <= CheckInAlpha || _SpriteRenderer.color.a >= LeaveAlpha)
            {
                Collider.enabled = false;
                yield return new WaitForSeconds(0.2f);
                Collider.enabled = true;

                Done = true;

                yield break;
            }

            yield return new WaitForSeconds(0.03f);
        }

        Collider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        Collider.enabled = true;

        Done = true;

        yield break;
    }
    #endregion

    #region getLeveData
    public Moving_Speed.limit getLeveData(int level)
    {
        return _leveldata[level].Getlimit;
    }
    #endregion
}
