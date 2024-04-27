using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLoad : MonoBehaviour
{
    public static float posX;
    public static float posY;
    public static int coinNum;
    public static int playerHp;
    public static int DIRTY;

    #region SerializeField
    [SerializeField]
    public GameObject getHero;
    public GameObject getCamera;

    #endregion

    #region saveData
    public void save()
    {
        Debug.Log("saveData");
        saveData();
    }
    #endregion

    #region readData
    public void read()
    {
        Debug.Log("readData");
        readData();
    }
    #endregion

    public void saveData()
    {
        PlayerPrefs.SetFloat("posX", getHero.transform.position.x);
        PlayerPrefs.SetFloat("posY", getHero.transform.position.y);
        // PlayerPrefs.SetInt("coinNum", 0);
        // PlayerPrefs.SetInt("playerHp", 0);
        // PlayerPrefs.SetInt("DIRTY", 0);
        PlayerPrefs.Save();
    }

    public void readData()
    {
        posX = PlayerPrefs.GetFloat("posX");
        posY = PlayerPrefs.GetFloat("posY");
        // coinNum = PlayerPrefs.GetInt("coinNum");
        // playerHp = PlayerPrefs.GetInt("playerHp");
        // DIRTY = PlayerPrefs.GetInt("DIRTY");

        getHero.transform.position = new Vector2(posX, posY);
        getCamera.transform.position = new Vector3(posX, 0, -10);
    }

    public void test()
    {
    }
}
