using UnityEditor;

[CustomEditor(typeof(SettingsConfig))]
public class SettingsConfig_Editor : Editor {

    /// <summary>
    /// Important to keep this script file inside the folder named "Editor"!
    /// </summary>
    public override void OnInspectorGUI()
    {
        SettingsConfig settingsConfig = (SettingsConfig)target;

        base.DrawDefaultInspector(); //Draw default parameters

        EditorGUILayout.Space(); //Create little space

        EditorGUILayout.LabelField("Current Settings", EditorStyles.boldLabel); //Add label

        EditorGUI.BeginDisabledGroup(true);
        //Everything inside will be disabled.

        float vol = EditorGUILayout.FloatField("Volume", settingsConfig.Volume);
        int quality = EditorGUILayout.IntField("Quality", settingsConfig.Quality);
        bool Effects = EditorGUILayout.Toggle("Effects", settingsConfig.Effects);
        bool Fullscreen = EditorGUILayout.Toggle("FullScreen", settingsConfig.Fullscreen);
        string res = EditorGUILayout.TextField("Resolution", settingsConfig.Resolution);
        bool Shadows = EditorGUILayout.Toggle("Shadows", settingsConfig.Shadows);
        int ShadowsQuality = EditorGUILayout.IntField("Shadows Quality", settingsConfig.ShadowsQuality);
        float Brightness = EditorGUILayout.FloatField("Brightness", settingsConfig.Brightness);
        
        //Everything inside will be disabled.
        EditorGUI.EndDisabledGroup();
    }
}