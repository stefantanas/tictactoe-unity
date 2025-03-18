using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace TicTacToe.General
{
    /// <summary>
    ///     A utility class that wraps Unity's PlayerPrefs for easy JSON-based save/load operations.
    /// </summary>
    public class PlayerPrefsManager
    {
        /// <summary>
        ///     Loads and deserializes data from PlayerPrefs using the specified key.
        ///     Returns default(T) if the key doesn't exist or if deserialization fails.
        /// </summary>
        public T Load<T>(string key)
        {
            try
            {
                var data = PlayerPrefs.GetString(key);
                if (!string.IsNullOrEmpty(data))
                    return JsonConvert.DeserializeObject<T>(data);
            }
            catch (JsonReaderException)
            {
                // If the stored string is invalid JSON, remove it to prevent repeated errors.
                Delete(key);
            }

            return default;
        }

        /// <summary>
        ///     Loads multiple values of type T from the given keys.
        ///     Returns an IEnumerable of T in the same order as the provided keys.
        /// </summary>
        public IEnumerable<T> Load<T>(params string[] keys)
        {
            return keys.Select(Load<T>);
        }

        /// <summary>
        ///     Saves data into PlayerPrefs, automatically serializing non-string objects as JSON.
        /// </summary>
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

        /// <summary>
        ///     Saves multiple key-value pairs in a single call.
        /// </summary>
        public void Save(params (string key, object value)[] values)
        {
            foreach (var (key, value) in values)
                Save(key, value);
        }

        /// <summary>
        ///     Deletes data from PlayerPrefs using the provided key.
        /// </summary>
        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        ///     Deletes ALL data from PlayerPrefs.
        /// </summary>
        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        /// <summary>
        ///     Ensures that a given key is set to a default value if it doesn't already exist in PlayerPrefs.
        /// </summary>
        public void PrepopulatePlayerPrefs<T>(string key, T defaultValue)
        {
            if (!PlayerPrefs.HasKey(key))
                Save(key, defaultValue);
        }
    }
}