using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveFile {

    public static void SaveData(SlotData data, int slot) {
        string path = Application.persistentDataPath + "/untitledstoryredux_" + slot + ".save";
        BinaryFormatter formatter = new();
        FileStream stream = new(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SlotData LoadData(int slot) {
        string path = Application.persistentDataPath + "/untitledstoryredux_" + slot + ".save";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SlotData data = (SlotData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        } else {
            return null;
        }
    }
    
}