using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip bgmClip;

    string framePath_V, framePath_F;
    FileInfo[] frameData_V, frameData_F;
    public int frameMaxIndex_V = 0;
    public int frameMaxIndex_F = 0;

    public Sprite[] spriteVerticalList;
    public Sprite[] spriteFourCutList;




    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        BGMSound();
        FrameVerticalCreation();
        FrameForCutCreation();
    }

    public void BGMSound()
    {
        //bgmSource.PlayOneShot(Resources.Load<AudioClip>("Sound/" + _name));
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void SFXSound(string _name)
    {
        sfxSource.PlayOneShot(Resources.Load<AudioClip>("Sound/" + _name));
    }



    

    public void FrameVerticalCreation()
    {
        framePath_V = Application.persistentDataPath + "/Frame/Vertical";
        Debug.Log("::  "+PlayerPrefs.GetString("MyPhoto_PictureMode"));

        DirectoryInfo di = new DirectoryInfo(framePath_V);
        frameData_V = di.GetFiles("*.png");
        frameMaxIndex_V = frameData_V.Length;

        spriteVerticalList = new Sprite[frameMaxIndex_V];

        //필터 갯수에 맞게 생성하기
        for (int i = 0; i < frameMaxIndex_V; i++)
        {
            byte[] frameByte = File.ReadAllBytes(Application.persistentDataPath +
                "/Frame/Vertical/" + frameData_V[i].Name);
            Texture2D frameTexture = null;
            frameTexture = new Texture2D(0, 0);
            frameTexture.LoadImage(frameByte);

            spriteVerticalList[i] = Sprite.Create(frameTexture, new Rect(0, 0, frameTexture.width, frameTexture.height),
                new Vector2(0, 0));
        }
    }

    public void FrameForCutCreation()
    {
        framePath_F = Application.persistentDataPath + "/Frame/FourCut";

        DirectoryInfo di = new DirectoryInfo(framePath_F);
        frameData_F = di.GetFiles("*.png");
        frameMaxIndex_F = frameData_F.Length;

        spriteFourCutList = new Sprite[frameMaxIndex_F];

        for(int i = 0; i < frameMaxIndex_F; i++)
        {
            byte[] frameByte = File.ReadAllBytes(Application.persistentDataPath +
                "/Frame/FourCut/" + frameData_F[i].Name);
            Texture2D frameTexture = null;
            frameTexture = new Texture2D(0, 0);
            frameTexture.LoadImage(frameByte);

            spriteFourCutList[i] = Sprite.Create(frameTexture, new Rect(0, 0, frameTexture.width, frameTexture.height),
                new Vector2(0, 0));
        }
    }
}
