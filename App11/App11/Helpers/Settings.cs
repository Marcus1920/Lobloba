using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace App11.Helpers
{
    public class Settings
    {


        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LastEmailSettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;


        private const string LastPasswodrSettingsKey = "settings_key";
        private static readonly string SettingsDefaultPassword = string.Empty;

        private const string LastapikeyrSettingsKey = "settings_key";
        private static readonly string SettingsDefaultapikey = string.Empty;


        private const string ConversationIdSettingsKey = "12";
        private static readonly string ConversationIdSettingsDefaultapikey = string.Empty;

        #endregion


        public static string LatEmailSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastEmailSettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastEmailSettingsKey, value);
            }
        }


        public static string LatPasswordSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsDefaultPassword, SettingsDefaultPassword);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsDefaultPassword, value);
            }
        }



        public static string LastApiKeySetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastapikeyrSettingsKey, SettingsDefaultapikey);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastapikeyrSettingsKey, value);
            }
        }

        public static string ConversationId
        {
            get
            {
                return AppSettings.GetValueOrDefault(ConversationIdSettingsKey, ConversationIdSettingsDefaultapikey);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ConversationIdSettingsKey, value);
            }
        }


    }
}