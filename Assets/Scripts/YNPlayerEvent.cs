using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YNPlayerEvent : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private GameObject FIcon = null;
    #endregion

    #region Private
    private Action action = null;
    private bool Listening = false;
    #endregion

    #region Unity Method
    void Start()
    {
        FIcon.SetActive(false);
    }

    private void OnDestroy()
    {
        StopCoroutine("ButtonClick");
    }

    private void OnDisable()
    {
        StopCoroutine("ButtonClick");
    }
    #endregion

    #region Event Setting
    public void setButtonEvent(Action a)
    {
        action += a;

        if (action != null && !Listening)
        {
            FIcon.SetActive(true);
            StartCoroutine("ButtonClick");
        }
    }

    public void DoButtonEvent(bool claerEvent)
    {
        if (action == null)
        {
            FIcon.SetActive(false);
            Debug.LogError("action is null");
            return;
        }

        action();

        if (claerEvent)
        {
            clearButtonEvent();
        }
    }

    public void clearButtonEvent()
    {
        FIcon.SetActive(false);
        action = null;
        Listening = false;
    }
    #endregion

    #region  ButtonClick
    private IEnumerator ButtonClick()
    {
        Listening = true;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Listening = false;

                DoButtonEvent(true);

                yield break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
    #endregion
}
