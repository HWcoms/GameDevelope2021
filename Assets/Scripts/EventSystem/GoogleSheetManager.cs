using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class GoogleSheetManager : MonoBehaviour
{
    public bool isUpdate;

    public string URL;

    public string folderName = "Dialog";
    public string fileName = "dialog.tsv";
    private string gameDataPath;

    public string data;

    public static GoogleSheetManager Singleton;
    #region singleton
    private void Awake()
    {
        if (null == Singleton)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
    #endregion

    void Start()
    {
        data = null;

        CreateGameInfoDir();
        GetFileInfo();

        /*
        data = null;
        if(data == null)
        {
            StartCoroutine(GetSheetInfo());
        }
        */
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGameInfoDir()
    {
        gameDataPath = Application.persistentDataPath;
        if (!Directory.Exists(gameDataPath + "/" + folderName + "/"))
        {
            print("folder doesn't exist, create folder");
            Directory.CreateDirectory(Application.persistentDataPath + "/" + folderName + "/");
        }
    }

    void GetFileInfo()
    {
        if(!File.Exists(gameDataPath + "/" + folderName + "/" + fileName) || isUpdate)
        {
            print("file doesn't exist, Import file or just Updating");

            StartCoroutine(GetSheetInfo());
        }
        else
        {
            string path = gameDataPath + "/" + folderName + "/" + fileName;
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            data = reader.ReadToEnd();
            //print("data reader: "+data);
            reader.Close();
        }
    }

    IEnumerator GetSheetInfo()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        data = www.downloadHandler.text;
        //print(data);

        //save data
        string path = gameDataPath + "/" + folderName + "/" + fileName;

        File.WriteAllText(path, data);
    }
}