using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.UI;
using TMPro;
public class DataManager : MonoBehaviour
{
    public UIManager UIM;

    private string _filename;
    [Serializable]
    public class Data
    {
        public float HighScore;
    }

    public void Save()
    {
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        Data data = new Data();

        //Save
        data.HighScore = UIM.HighScore;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void Load()
    {
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        if (!File.Exists(_filename))
        {
            return;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);
        Data data = (Data)formatter.Deserialize(stream);
        stream.Close();

        //Load
        UIM.LoadHighScore(data.HighScore);
    }

    public void Awake()
    {
        Load();
    }
}
