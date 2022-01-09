using System;
using System.Net;
using UnityEngine;

public class FileDownloader : MonoBehaviour
{

    public string fileURL;

    private void Start()
    {
        DownloadFile();
    }

    private void DownloadFile()
    {
        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
        client.DownloadFileAsync(new Uri("https://www.motorolasolutions.com/content/dam/msi/docs/products/two-way-radios/mototrbo/portable-radios/sl3500e/sl3500e_brochure.pdf"), Application.dataPath + "/../Photos/motorola_eBrochure.pdf");
    }

    private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            Debug.Log("Complete");
        }
    }

}
