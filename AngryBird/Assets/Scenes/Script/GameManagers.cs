using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    public List<BIrd> bIrds;
    public List<PIg> pIgs;
    public static GameManagers instance;
    private Vector3 originPos;
    public GameObject win;
    public GameObject lose;
    public GameObject[] star;
    private int starsNum = 0;
    private int totalNum = 10;

    private void Awake()
    {
        instance = this;
        if(bIrds.Count>0)
        {
            originPos = bIrds[0].transform.position;
        }
        
    }

    private void Start()
    {
        BirdInit();
    }

    private void BirdInit()
    {
        for(int i = 0;i<bIrds.Count;i++)
        {
            if(i == 0)//第一次执行该方法的时候只让第一只小鸟在绳子组件上
            {       //后续调用该方法的时候，只让当前的小鸟获取绳子组件，其他鸟都不能获取这个组件
                bIrds[i].transform.position = originPos;
                bIrds[i].enabled = true; 
                bIrds[i].sp.enabled = true; 
            }
            else
            {
                bIrds[i].enabled = false;//禁用脚本组件
                bIrds[i].sp.enabled = false;//禁用spring joint组件
            }
        }
    }

    public void PreNextBird()
    {
        if(pIgs.Count>0)
        {
            if(bIrds.Count>0)
            {
                //让下一只上来
                BirdInit();
            }
            else
            {
                //输了
                lose.SetActive(true);  //激活lose所代表的物体
            }
        }
        else
        {
            //赢了
            win.SetActive(true); 
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show"); //开启这个协程
    }

    IEnumerator show() //创建一个协程
    {
        for (; starsNum < bIrds.Count + 1; starsNum++)
        {
            if(starsNum >= star.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f); //每次循环的时候等0.2秒再执行下面的代码
            star[starsNum].SetActive(true);
        }
    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if(starsNum>PlayerPrefs.GetInt(PlayerPrefs.GetString("nowlevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowlevel"), starsNum);//把星星数赋值给当前关卡名中存储
        }
        int sum = 0;
        for(int i = 1; i<=totalNum; i++) //记录所有关卡的星星之和
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
