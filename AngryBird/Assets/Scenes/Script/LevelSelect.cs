using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite levelBG;//代表这个游戏物体组件里面的图片
    private Image image;
    public GameObject[] stars;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name)//直接激活第一关
        {
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name)-1;
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString())>0)//如果前一关的星星数大于0
            {
                isSelect = true;
            }
        }
        if(isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("level" + gameObject.name); //选择关卡界面星星的显示
            if(count > 0)
            {
                for(int i = 0; i<count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected()
    {
        if(isSelect)
        {
            PlayerPrefs.SetString("nowlevel", "level" + gameObject.name);//把当前关卡名（第几关）赋值给nowlevel变量
            SceneManager.LoadScene(2);
        }
    }
}
