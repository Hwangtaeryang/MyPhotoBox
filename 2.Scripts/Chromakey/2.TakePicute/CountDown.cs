using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public static CountDown instance;

    public Image timerRota;
    public Image countImg;
    public Sprite[] countSprites;

    int pictureCount = 0;


    void Start()
    {
        instance = this;
        FiveCountDown();
    }

    

    void FiveCountDown()
    {
        StartCoroutine(_FiveCountDown());
    }

    public static void Static_FiveCountDown()
    {
        instance.FiveCountDown();
    }

    public static int PictureCount()
    {
        return instance.pictureCount;
    }

    IEnumerator _FiveCountDown()
    {
        countImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        timerRota.transform.DORotate(new Vector3(0f, 0f, -360f), 2.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear)
                     .SetLoops(-1);

        if (pictureCount != 1)
            yield return new WaitForSeconds(1f);


        countImg.sprite = countSprites[0];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);

        countImg.sprite = countSprites[1];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);

        countImg.sprite = countSprites[2];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);

        countImg.sprite = countSprites[3];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1);

        countImg.sprite = countSprites[4];
        GameManager.instance.SFXSound("Countdown_end");
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.GetString("MyPhoto_PictureMode").Equals("Vertical"))
        {
            UICameraPictrueShot.Static_UiTeakePictureShot(1600, 2400, 0);

            //GameManager.instance.SFXSound("Countdown_end");
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(_SceneManager());
        }
        else if(PlayerPrefs.GetString("MyPhoto_PictureMode").Equals("FourCut"))
        {
            pictureCount++;

            UICameraPictrueShot.Static_UiTeakePictureShot(1680, 2400, pictureCount);

            //GameManager.instance.SFXSound("Countdown_end");
            yield return new WaitForSeconds(0.5f);

            if (pictureCount < 6)
                StartCoroutine(_FiveCountDown());
            else StartCoroutine(_SceneManager());
        }
    }

    IEnumerator _SceneManager()
    {
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name.Equals("3_1_1_VerticalTakePicture"))
            //SceneManager.LoadScene("3_1_2_VerticalFilterChoice");
            LoadScene.instance.LoadByName("3_1_2_VerticalFilterChoice");
        else if (SceneManager.GetActiveScene().name.Equals("4_1_FourCutTakePicture"))
            //SceneManager.LoadScene("4_2_FourPictureChoice");
            LoadScene.instance.LoadByName("4_2_FourPictureChoice");
    }

}
