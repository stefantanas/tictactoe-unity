using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace TicTacToe.General
{
    public class PlayerPrefsManager
    {
        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public T Load<T>(string key)
        {
            try
            {
                var data = PlayerPrefs.GetString(key);
                if (!string.IsNullOrEmpty(data)) return JsonConvert.DeserializeObject<T>(data);
            }
            catch (JsonReaderException)
            {
                Delete(key);
            }

            return default;
        }

        public IEnumerable<T> Load<T>(params string[] keys)
        {
            return keys.Select(Load<T>);
        }

        public void Save<T>(string key, T value)
        {
            if (value is string s)
            {
                PlayerPrefs.SetString(key, s);
            }
            else
            {
                var data = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString(key, data);
            }
        }

        public void Save(params (string key, object value)[] values)
        {
            foreach (var (key, value) in values) Save(key, value);
        }

        public void PrepopulatePlayerPrefs<T>(string key, T defaultValue)
        {
            if (!PlayerPrefs.HasKey(key)) Save(key, defaultValue);
        }
    }
}