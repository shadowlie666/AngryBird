using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite levelBG;//���������Ϸ������������ͼƬ
    private Image image;
    public GameObject[] stars;


    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name)//ֱ�Ӽ����һ��
        {
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name)-1;
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString())>0)//���ǰһ�ص�����������0
            {
                isSelect = true;
            }
        }
        if(isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("level" + gameObject.name); //ѡ��ؿ��������ǵ���ʾ
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
            PlayerPrefs.SetString("nowlevel", "level" + gameObject.name);//�ѵ�ǰ�ؿ������ڼ��أ���ֵ��nowlevel����
            SceneManager.LoadScene(2);
        }
    }
}
