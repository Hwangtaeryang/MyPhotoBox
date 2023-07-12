using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using TMPro;
using UnityEngine.UI;

public class EmailPictureSend : MonoBehaviour
{
    public InputField virtualInputField;
    public TMP_InputField emailAddrInputField;

    public GameObject completePanel;
    public TextMeshProUGUI waitText;
    public GameObject waitImg;

    void Start()
    {
        
    }

    public void EmailSendButtonOn()
    {
        if(emailAddrInputField.text != "")
        {
            completePanel.SetActive(true);
            StartCoroutine(WaitSend());
            EmailSend();
        }
    }

    public void EmailSend()
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("gateways2021@naver.com");    //보내는 사람
        mail.To.Add(emailAddrInputField.text);    //받는 사람
        mail.Subject = "MyPhotoBooth 사진";
        mail.Body = "MyPhotoBooth를 이용해 주셔서 감사합니다.\n" +
            "고객님의 소중한 추억을 보내드립니다.\n\n";

        //첨부파일 = 대용량 안됨드
        System.Net.Mail.Attachment attachment;
        //파일 경로
        attachment = new System.Net.Mail.Attachment(Application.persistentDataPath + "/Print/PrintPicture.png");
        mail.Attachments.Add(attachment);

        SmtpClient smtpServer = new SmtpClient("smtp.naver.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("gateways2021@naver.com", "gw2022!!##"); //보내는 사람 주소 및 비번
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);

        Debug.Log("성공!!!");
    }

    IEnumerator WaitSend()
    {
        waitImg.SetActive(true);
        waitText.text = "Finishing..";
        yield return new WaitForSeconds(1);
        waitText.text = "Finishing...";
        yield return new WaitForSeconds(1);
        waitText.text = "Finishing..";
        yield return new WaitForSeconds(1);
        waitText.text = "Finishing...";
        yield return new WaitForSeconds(1);
        waitText.text = "Finishing..";
        yield return new WaitForSeconds(1);
        waitImg.SetActive(false);
    }

    public void EmailAddressShow()
    {
        emailAddrInputField.text = virtualInputField.text;
    }
}
