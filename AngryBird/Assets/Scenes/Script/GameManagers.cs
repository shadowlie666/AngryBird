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
            if(i == 0)//��һ��ִ�и÷�����ʱ��ֻ�õ�һֻС�������������
            {       //�������ø÷�����ʱ��ֻ�õ�ǰ��С���ȡ��������������񶼲��ܻ�ȡ������
                bIrds[i].transform.position = originPos;
                bIrds[i].enabled = true; 
                bIrds[i].sp.enabled = true; 
            }
            else
            {
                bIrds[i].enabled = false;//���ýű����
                bIrds[i].sp.enabled = false;//����spring joint���
            }
        }
    }

    public void PreNextBird()
    {
        if(pIgs.Count>0)
        {
            if(bIrds.Count>0)
            {
                //����һֻ����
                BirdInit();
            }
            else
            {
                //����
                lose.SetActive(true);  //����lose�����������
            }
        }
        else
        {
            //Ӯ��
            win.SetActive(true); 
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show"); //�������Э��
    }

    IEnumerator show() //����һ��Э��
    {
        for (; starsNum < bIrds.Count + 1; starsNum++)
        {
            if(starsNum >= star.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f); //ÿ��ѭ����ʱ���0.2����ִ������Ĵ���
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
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowlevel"), starsNum);//����������ֵ����ǰ�ؿ����д洢
        }
        int sum = 0;
        for(int i = 1; i<=totalNum; i++) //��¼���йؿ�������֮��
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
