using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UICameraPictrueShot : MonoBehaviour
{
    private static UICameraPictrueShot instance;

    Camera uiCamera;
    bool takeScreenShotOnNextFrame;


    string id_folderPath;
    int pictureCount;


    void Start()
    {
        instance = this;
        uiCamera = gameObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext _context, Camera _camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        if(takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = uiCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            FolderPath(byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            uiCamera.targetTexture = null;
        }
    }

    void FolderPath(byte[] _byteArray)
    {
        if(SceneManager.GetActiveScene().name.Equals("3_1_3_VerticalFrameChoice") ||
            SceneManager.GetActiveScene().name.Equals("4_4_FourFrameChoice"))
        {
            string folderPath = Application.persistentDataPath + "/Print";

            //������ ������ �����Ѵ�.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/Print/PrintPicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_1_1_VerticalTakePicture"))
        {
            string folderPath = Application.persistentDataPath + "/VertBasicPictureShot";

            //���� ������ �����Ѵ�.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/VertBasicPictureShot/BasicPicture.png", _byteArray);
            GameManager.instance.SFXSound("CameraShutter");
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_1_2_VerticalFilterChoice"))
        {
            string folderPath = Application.persistentDataPath + "/VertiFilterComposePicture";

            //������ ������ �����Ѵ�.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/VertiFilterComposePicture/V_FilterPicture.png", _byteArray);
        }
        else if (SceneManager.GetActiveScene().name.Equals("4_1_FourCutTakePicture"))
        {
            string folderPath = Application.persistentDataPath + "/FourCutPicture";

            //������ ������ �����Ѵ�.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + 
                "/FourCutPicture/FourCutPicture" + pictureCount + ".png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("4_3_FourFilterChoice"))
        {
            string folderPath = Application.persistentDataPath + "/FourCut_FilterComposePicture";

            //������ ������ �����Ѵ�.
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + 
                "/FourCut_FilterComposePicture/FourCutFilterPicture.png", _byteArray);
        }
        
    }

    void UiTakePictureShot(int _width, int _height, int _count)
    {
        pictureCount = _count;
        uiCamera.targetTexture = RenderTexture.GetTemporary(_width, _height, 64);
        takeScreenShotOnNextFrame = true;
    }

    public static void Static_UiTeakePictureShot(int _width, int _height, int _count)
    {
        instance.UiTakePictureShot(_width, _height, _count);
    }
}
