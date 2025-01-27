
using UnityEngine;

public static class SaveLoadData
{
   public static void SaveDataInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public static int LoadDataInt(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
            return 0;      
    }

    public static int LoadDataInt(string name, int defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
            return defaultValue;
    }

    public static void SaveDataFloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    public static float LoadDataFloat(string name, float defaultValue)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetFloat(name);
        else
            return defaultValue;
        
        
    }
}
