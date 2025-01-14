using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausepanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Pause()//点击暂停按钮时
    {
        button.SetActive(false);
        anim.SetBool("ispause", true); 
    }

    public void Home()
    {
        Time.timeScale = 1; //要把游戏设置成继续游戏状态，不然无法运行下面切换场景的操作
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("ispause", false);
    }

    public void PasueAnim()//播放暂停的弹出动画
    {
        Time.timeScale = 0;
    }

    public void ResumeAnim()//播放解除暂停的弹回动画
    {
        button.SetActive(true);
    }
}
