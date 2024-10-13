using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// need to use this namespace to use save / load data functions
using System.IO;
// Need to only include this name space is compiling for editor, as isn't included in external builds so would cause an error
#if UNITY_EDITOR
using UnityEditor;
#endif




public class DataHandler : MonoBehaviour
{
    // make it a static class so values stored in this class member will be shared by all instances of that class
    public static DataHandler Instance;

    public string userName;

    public string bestScoreUserName;
    public int bestScorePoints;

    // Make sure it's a singleton when script is loaded, before Start() is called
    private void Awake()
    {
        // only allow instance to live if there isn't one already,
        // keeping it a singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // set Instance variable (Instance is just name of variable) to this script - so any reference to MainManager.Instance will refer to this script, and don't need to bother finding it and storing references (which is only necessary when there are multiple instances)
        Instance = this;
        // mark is not to be destroyed when the scene changes
        DontDestroyOnLoad(gameObject);
    }

    // need to tag things like this for the JsonUtility to act on them
    [System.Serializable]
    // class with the variables to be saved
    class SaveData
    {
        public int savedBestScorePoints;
        public string savedBestScoreUserName;
    }

    // method to save color
    public void SaveBestScore()
    {
        // make new instance of SaveData class and store in data variable
        SaveData data = new SaveData();
        // use dot notation to access TeamColor attribute of SaveData class and save in the TeamColor global variable of MainManager
        data.savedBestScoreUserName = bestScoreUserName;
        data.savedBestScorePoints = bestScorePoints;
        // convert the data variable to json and store in new variable
        string json = JsonUtility.ToJson(data);
        // on the new json variable, use built in Unity method 'Application.persistentDataPath' that will give folder to save data that will persist between application reinstall or udpate and append to filename 'savefile.json'
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // method to load saved colour
    public void LoadBestScore()
    {
            // path to loaded file
            string path = Application.persistentDataPath + "/savefile.json";
        // avoid errors by only trying to access file if path exists
        if(File.Exists(path))
        {
            // save the data in the file to the json variable
            string json = File.ReadAllText(path);
            // parse the json data and save in data variable
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // save the loaded TeamColor into gloabl  variable
            bestScoreUserName = data.savedBestScoreUserName;
            bestScorePoints = data.savedBestScorePoints;
        }
    }
}
