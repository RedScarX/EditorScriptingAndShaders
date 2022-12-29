using System;
using UnityEditor;
using UnityEngine;

namespace DemoEditor
{
    [CustomEditor(typeof(Player))]
    public class PlayerInspector : Editor
    {
        private Player player;
        private bool canFold;
        private int selectedIndex;

        private void OnEnable()
        {
            player = (Player)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.skin.label.fontSize = 20;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Player Class");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.LowerLeft;
            
            EditorGUILayout.LabelField("Player ID: " + player.id);
            player.name = EditorGUILayout.TextField("Player Name: " , player.name);
            EditorGUILayout.LabelField("Description");
            player.description = EditorGUILayout.TextArea(player.description, GUILayout.MinHeight(70));

            player.damage = EditorGUILayout.Slider("Damage",player.damage, 0, 100);
            player.health = player.damage;
            Rect progressRect = GUILayoutUtility.GetRect(20, 20);
            serializedObject.Update();
            
            if(player.damage > 80)
                GUI.color = Color.red;
            else if (player.damage > 60 && player.damage < 80)
                GUI.color = Color.green;
            else
                GUI.color = Color.cyan;
            
            EditorGUI.ProgressBar(progressRect,player.damage/100,"Player Health");
            GUI.color = Color.white;

            EditorGUI.indentLevel++;
            canFold = EditorGUILayout.Foldout(canFold, "Weapons");
            EditorGUI.indentLevel--;
        
            serializedObject.Update();
            var items = serializedObject.FindProperty(nameof(player.weapon));
                
            if (canFold)
            {
                for (int i = 0; i < items.arraySize; i++)
                {
                    if (GUILayout.Button(items.GetArrayElementAtIndex(i).FindPropertyRelative(nameof(Weapon.weaponName)).stringValue + i))
                    {
                        selectedIndex = i;
                    }
                }

                if (selectedIndex >= 0 && selectedIndex <= items.arraySize)
                {
                    EditorGUI.indentLevel++;
                    var gg = EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(selectedIndex),new GUIContent("Weapons "+selectedIndex),true);
                    EditorGUI.indentLevel--;
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }

        private static void DrawElemrnds(SerializedProperty items)
        {
            var indent = EditorGUI.indentLevel;
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel += 10;
            EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(1));
            EditorGUI.indentLevel = indent;
            EditorGUILayout.EndVertical();
        }
    }
}