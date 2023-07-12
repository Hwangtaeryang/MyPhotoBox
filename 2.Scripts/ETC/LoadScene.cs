using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;



public class LoadScene : MonoBehaviour
{
    public static LoadScene instance { get; private set; }

    public GameObject loaderBack;
    public Image timerRot;

    private AsyncOperation async;

    [SerializeField]
    Slider sliderBar;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void LoadByName(string _loadSceneName)
    {
        timerRot.transform.DORotate(new Vector3(0f, 0f, -360f), 
            1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        loaderBack.SetActive(true);
        StartCoroutine(_LoadScene(_loadSceneName));
    }

    IEnumerator _LoadScene(string _nextScene)
    {
        yield return null;
        async = SceneManager.LoadSceneAsync(_nextScene);

        async.allowSceneActivation = false; //����� �غ�� ��� ����� Ȱ��ȭ�Ǵ� ���� ���

        float timer = 0f;


        while (!async.isDone)   //isDone - �ش� ������ �Ϸ�Ǿ����� ��Ÿ��
        {
            yield return null;
            timer += Time.deltaTime * 0.01f;

            if (async.progress < 0.9f)
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, async.progress, timer);
                if (sliderBar.value >= async.progress)
                    timer = 0;
            }
            else
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, 1f, timer);
                if (sliderBar.value >= 0.99f)
                {
                    async.allowSceneActivation = true;
                    yield break;
                }
            }
        }




        ///�ε� �� ����////https://alpharodun.tistory.com/25
        ///�����Ҷ� ���� ����////
        //async = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Additive);
        //yield return async;

        //if(SceneManager.GetActiveScene().name.Equals("0.IntroMain"))
        //    async = SceneManager.LoadSceneAsync("3_1_1_VerticalTakePicture");
        //else if (SceneManager.GetActiveScene().name.Equals("3_1_1_VerticalTakePicture"))
        //    async = SceneManager.LoadSceneAsync("3_1_2_VerticalFilterChoice");
        //else if (SceneManager.GetActiveScene().name.Equals("3_1_2_VerticalFilterChoice"))
        //    async = SceneManager.LoadSceneAsync("3_1_3_VerticalFrameChoice");
        //else if (SceneManager.GetActiveScene().name.Equals("3_1_3_VerticalFrameChoice"))
        //    async = SceneManager.LoadSceneAsync("3_1_4_Print");
        //yield return async;
    }
}
