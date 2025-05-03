/*using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private string savePath;

    [System.Serializable]
    private class SaveData
    {
        public Dictionary<string, object> data = new Dictionary<string, object>();
    }

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.dat");
    }

    public void SaveGame()
    {
        var saveables = FindObjectsOfType<MonoBehaviour>(true);
        var data = new SaveData();

        foreach (var obj in saveables)
        {
            if (obj is ISaveable saveable)
            {
                string id = obj.gameObject.name + obj.GetInstanceID();
              //  data.data[id] = saveable.CaptureState();
            }
        }

        using (FileStream stream = File.Create(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath)) return;

        SaveData data;
        using (FileStream stream = File.OpenRead(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            data = (SaveData)formatter.Deserialize(stream);
        }

        var saveables = FindObjectsOfType<MonoBehaviour>(true);
        foreach (var obj in saveables)
        {
            if (obj is ISaveable saveable)
            {
                string id = obj.gameObject.name + obj.GetInstanceID();
                if (data.data.TryGetValue(id, out object state))
                {
                 //   saveable.RestoreState(state);
                }
            }
        }
    }
}
*/