using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/17l2xHpUDyDNRuh7rBE6A9BQmsleYnjUMd1S0OZfUM4g/export?format=tsv&range=A2:B";


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
