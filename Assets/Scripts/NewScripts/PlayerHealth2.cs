using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth2 : MonoBehaviour
{

    //主角的血量
    public int hearts = 6;//初始血量 拷貝用 不可更改
    public Slider healthSlider;//血條 顯示用
    private static int currentHealth;//目前血量 計算用
    //受到撞擊的聲音
    public AudioClip hitSound;
    //死亡動畫
    public GameObject deathAnim;
    //碰到補血道具的音效
    public AudioClip heartSound;

    //玩家是否死亡
    private bool isDeath = false;
    //是否受到傷害
    private bool damaged = true;
    //設置可接受傷害為是
    private bool canGetHurt = true;

    //渲染2D圖形
    private SpriteRenderer rend;
    //血量介面
    private int health;

    void Start()
    {
        Debug.Log("test");

        healthSlider.maxValue = hearts;
        currentHealth = hearts;
        if (currentHealth <= 0)
        {
            healthSlider.value = hearts;
            currentHealth = hearts;
        }
        else
        {
            healthSlider.value = hearts;
        }
    }

    public void takeDamage(int amount)
    {//受傷害
        damaged = true;
        if (isDeath) return;
        currentHealth -= amount;//血量減掉敵人攻擊力
        Debug.Log(currentHealth);

        healthSlider.value = currentHealth;
        canGetHurt = false;

        StartCoroutine(resetCanHurt());
        if (currentHealth <= 0)
        {
            Debug.Log("Death()1");
            StartCoroutine(Death());

            
        }
    }
    public IEnumerator Death()
    {
        Debug.Log("Death()2");
        //死亡動畫的位置
        Instantiate(deathAnim, transform.position, Quaternion.Euler(0, 180, 0));
        BroadcastMessage("died", SendMessageOptions.DontRequireReceiver);
        //將2D渲染設為關閉
        var rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        //重新復活時間為2秒
        yield return new WaitForSeconds(2);
        //重新讀取關卡
        SceneManager.GetSceneByName("Level1");
    }

    //重製可以受到傷害的各種數值與設定
    //受到傷害時變為紅色(1.0f,0.5f,0.5f,1.0f)就是CMYK
    //受到傷害後的無敵時間設為1秒,調到100或許就是所謂的超簡單遊戲
    //結束無敵時間後記得將顏色條回全白,不然就會保持在紅色的狀態
    public IEnumerator resetCanHurt()
    {
        Debug.Log("resetCanHurt()2");
 
        rend.color = new Vector4(1.0f, 0.5f, 0.5f, 1.0f);
        yield return new WaitForSeconds(1f);
        rend.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        canGetHurt = true;
    }

}
