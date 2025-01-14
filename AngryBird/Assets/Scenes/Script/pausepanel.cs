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

    public void Pause()//�����ͣ��ťʱ
    {
        button.SetActive(false);
        anim.SetBool("ispause", true); 
    }

    public void Home()
    {
        Time.timeScale = 1; //Ҫ����Ϸ���óɼ�����Ϸ״̬����Ȼ�޷����������л������Ĳ���
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("ispause", false);
    }

    public void PasueAnim()//������ͣ�ĵ�������
    {
        Time.timeScale = 0;
    }

    public void ResumeAnim()//���Ž����ͣ�ĵ��ض���
    {
        button.SetActive(true);
    }
}
