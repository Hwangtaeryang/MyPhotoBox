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

        mail.From = new MailAddress("gateways2021@naver.com");    //������ ���
        mail.To.Add(emailAddrInputField.text);    //�޴� ���
        mail.Subject = "MyPhotoBooth ����";
        mail.Body = "MyPhotoBooth�� �̿��� �ּż� �����մϴ�.\n" +
            "������ ������ �߾��� �����帳�ϴ�.\n\n";

        //÷������ = ��뷮 �ȵʵ�
        System.Net.Mail.Attachment attachment;
        //���� ���
        attachment = new System.Net.Mail.Attachment(Application.persistentDataPath + "/Print/PrintPicture.png");
        mail.Attachments.Add(attachment);

        SmtpClient smtpServer = new SmtpClient("smtp.naver.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("gateways2021@naver.com", "gw2022!!##"); //������ ��� �ּ� �� ���
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);

        Debug.Log("����!!!");
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
