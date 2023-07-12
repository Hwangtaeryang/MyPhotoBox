using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    public void NextSceneMove(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");
        StartCoroutine(_SceneMove(_name));
    }

    public void PictureMode(string _mode)
    {
        PlayerPrefs.SetString("MyPhoto_PictureMode", _mode);
        //Debug.Log(PlayerPrefs.GetString("MyPhoto_PictureMode"));
    }

    public void ID_Photo_Size(string _size)
    {
        PlayerPrefs.SetString("MyPhoto_IDPhotoSize", _size);
    }

    public void Quit_YesButtonOn()
    {
        GameManager.instance.SFXSound("ClickSound");
        Application.Quit();
    }

    public void HomeButtonOn()
    {
        GameManager.instance.SFXSound("ClickSound");
        SceneManager.LoadScene("0.IntroMain");
    }

    public void BackButtonOn(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");

        StartCoroutine(_SceneMove(_name));
    }

    public void ButtonClickSound()
    {
        GameManager.instance.SFXSound("ClickSound");
    }

    public void ComposeSaveButtonOn()
    {
        GameManager.instance.SFXSound("ClickSound");
        if (PlayerPrefs.GetString("MyPhoto_PictureMode").Equals("Vertical"))
            UICameraPictrueShot.Static_UiTeakePictureShot(1600, 2400, 0);
        else if (PlayerPrefs.GetString("MyPhoto_PictureMode").Equals("FourCut"))
            UICameraPictrueShot.Static_UiTeakePictureShot(1600, 2400, 0);
        //StartCoroutine(_SceneMove(_name));
    }


    IEnumerator _SceneMove(string _name)
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_name);
    }
}
