using UnityEditor;
using UnityEngine;

namespace VTL.SimTimeControls
{
    [CustomEditor(typeof(PlaySpeedSliderControl))]
    public class PlaySpeedSliderControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PlaySpeedSliderControl script = (PlaySpeedSliderControl)target;
            script.speedSelectionInterval = 
                (SpeedSelectionInterval)EditorGUILayout.EnumPopup("Speed Selection Mode", script.speedSelectionInterval);

            if (script.speedSelectionInterval == SpeedSelectionInterval.Linear)
            {
                script.LinearSpeedMin = EditorGUILayout.FloatField("Minimum Hours / Second Rate", script.LinearSpeedMin);
                script.LinearSpeedMax = EditorGUILayout.FloatField("Maximum Hours / Second Rate", script.LinearSpeedMax);
            }
            else // Exponential
            {
                script.ExponentialSpeedSecToHourBaseline = 
                    EditorGUILayout.FloatField("Baseline Hours / Second Rate", script.ExponentialSpeedSecToHourBaseline);
            }


            if (GUI.changed)
            {
                EditorUtility.SetDirty(script);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}