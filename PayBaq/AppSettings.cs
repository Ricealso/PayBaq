using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.Collections.Generic;
using PayBaq.Resources;

namespace PayBaq
{
    public class AppSettings
    {
        private static AppResources _appResources = new AppResources();

        public AppResources AppResources { get { return _appResources; } }
        
        IsolatedStorageSettings settings;

        //Set self awareness aka Adult Phrasing
        const string SelfAwarenessKeyName = "SelfAwareness";

        const bool SelfAwarenessDefault = false;

        public AppSettings()
        {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
            {
                settings = IsolatedStorageSettings.ApplicationSettings;
            }
        }

        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            if (settings.Contains(Key))
            {
                if (settings[Key] != value)
                {
                    settings[Key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                settings.Add(Key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;

            if (settings.Contains(Key))
            {
                value = (T)settings[Key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }

        public void Save()
        {
            settings.Save();
        }

        public bool SelfAwarenessSetting
        {
            get
            {
                return GetValueOrDefault<bool>(SelfAwarenessKeyName, SelfAwarenessDefault);
            }
            set
            {
                if (AddOrUpdateValue(SelfAwarenessKeyName, value))
                {
                    Save();
                }
            }
        }
    }
}