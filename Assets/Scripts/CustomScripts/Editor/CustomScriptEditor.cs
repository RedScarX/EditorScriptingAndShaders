using System;
using System.Collections.Generic;
using Demo;
using UnityEditor;
using UnityEngine;

namespace DemoEditor
{
    [Serializable]
    public class MyList
    {
    }

    public class CustomScriptEditor : EditorWindow
    {
        private string getNameSpace = "";
        private string getClassName = "";

        private bool addAwake = true;
        private bool addOnEnableAndDisable ;
        private bool addOnDestroy ;
        private bool addStart ;
        private bool addUpdate ;
        private bool addFixedUpdate ;
        private bool addOnTriggerMethods ;
        private bool addOnCollisionMethods ;

        private string createNameSpace = "";
        private string endNameSpace = "";
        private string awake="";
        private string onEnable="";
        private string start="";
        private string onDestroy="";
        private string update="";
        private string fixedUpdate="";
        private string collision="";
        private string trigger="";

        private SerializedObject serializedObject;

        private SerializedProperty listProperty;
        private int listLength;
        private  MyList myList;


        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
        }

        [MenuItem("Window/Create Custom Script")]
        public static void ShowWindow()
        {
            int width = 600;
            int height = 600;
            int x = Screen.width / 2;
            int y = Screen.height / 2;
           var window =  GetWindow<CustomScriptEditor>();
           window.position = new Rect(x,y,width,height);
           window.titleContent = new GUIContent("Create Script");
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            CreateHeader("Custom Script Creator");
            EditorGUI.indentLevel++;
           
            if (NameSpaceMethod()) return;

            EditorGUILayout.BeginVertical();
            
            GUILayout.Space(20);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 16;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label("Add Methods");
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            addAwake = EditorGUILayout.Toggle(addAwake, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add Awake Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addOnEnableAndDisable = EditorGUILayout.Toggle(addOnEnableAndDisable, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add OnEnable And Disable Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            
            EditorGUILayout.BeginHorizontal();
            addStart = EditorGUILayout.Toggle(addStart, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add Start Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addUpdate = EditorGUILayout.Toggle(addUpdate, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add Update Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addFixedUpdate = EditorGUILayout.Toggle(addFixedUpdate, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add FixedUpdate Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addOnDestroy = EditorGUILayout.Toggle(addOnDestroy, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add OnDestroy Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addOnCollisionMethods = EditorGUILayout.Toggle(addOnCollisionMethods, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add OnCollision Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            addOnTriggerMethods = EditorGUILayout.Toggle(addOnTriggerMethods, EditorStyles.radioButton,GUILayout.MaxWidth(20));
            GUILayout.Label("Add OnTrigger Method",EditorStyles.label);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
            
            getClassName = MakeFirstIndexUppercase(getClassName);
            getNameSpace = MakeFirstIndexUppercase(getNameSpace);
            if (GUILayout.Button("Create Script"))
            {
                CheckNameSpace();
                string path = EditorUtility.SaveFilePanel("Save Script", "Assets", getClassName, "cs");

                if (addAwake)
                    awake = "\n \t\tprivate void Awake()\n\t\t{\n\n\t\t}"; 
                
                if(addStart)
                    start = "\n \t\tprivate void Start()\n\t\t{\n\n\t\t}";
                
                if(addUpdate)
                    update = "\n \t\tprivate void Update()\n\t\t{\n\n\t\t}";
                
                if(addOnDestroy)
                    onDestroy = "\n \t\tprivate void OnDestroy()\n\t\t{\n\n\t\t}";
                
                if(addFixedUpdate)
                    fixedUpdate = "\n \t\tprivate void FixedUpdate()\n\t\t{\n\n\t\t}";
                
                if(addOnEnableAndDisable)
                    onEnable = "\n \t\tprivate void OnEnable()\n\t\t{\n\n\t\t}\n\n \t\tprivate void OnDisable()\n\t\t{\n\n\t\t}";
                
                if(addOnCollisionMethods)
                    collision = "\n \t\tprivate void OnCollisionEnter(Collision collisionInfo)\n\t\t{\n\n\t\t}\n \t\tprivate void OnCollisionStay(Collision collisionInfo)\n\t\t{\n\n\t\t}\n\t\tprivate void OnCollisionExit(Collision collisionInfo)\n\t\t{\n\n\t\t}";
                
                if(addOnTriggerMethods)
                    trigger = "\n \t\tprivate void OnCollisionEnter(Collider other)\n\t\t{\n\n\t\t}\n\n \t\tprivate void OnCollisionStay(Collider other)\n\t\t{\n\n\t\t}\n\t\tprivate void OnCollisionExit(Collider other)\n\t\t{\n\n\t\t}";
                
                
                if (path.Length > 0)
                {
                    string scriptText =
                        "using UnityEngine;\n"+createNameSpace+"\n\tpublic class "+ getClassName +" : MonoBehaviour\n\t{\n"
                        +awake +onEnable+start+update+fixedUpdate+collision+trigger+onDestroy+"\n\t}"+endNameSpace+"";
                    System.IO.File.WriteAllText(path, scriptText);
                    AssetDatabase.ImportAsset(path);
                    AssetDatabase.Refresh();
                }
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        private void CheckNameSpace()
        {
            if (getNameSpace.Length >= 2)
            {
                createNameSpace = "\nnamespace " + getNameSpace + "\n{";
                endNameSpace = "\n}";
            }
            else
            {
                var name = Application.productName;
                name = name.Replace(" ", "");
                createNameSpace = "\nnamespace " + name + "\n{";
                endNameSpace = "\n}";
            }
        }

        private bool NameSpaceMethod()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("NameSpace", GUILayout.MaxWidth(90));
            getNameSpace = EditorGUILayout.TextField(getNameSpace);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Class Name", GUILayout.MaxWidth(90));
            getClassName = EditorGUILayout.TextField(getClassName);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

            if (getNameSpace.Length <= 1)
                EditorGUILayout.HelpBox("Please enter namespace or default namespace will be added..!", MessageType.Warning);

            if (getClassName.Length <= 2)
            {
                EditorGUILayout.HelpBox("Please Enter Class Name more then 2 chars", MessageType.Error);
                return true;
            }

            return false;
        }

        private static void CreateHeader(String headerName)
        {
            GUILayout.Space(25);
            GUI.skin.label.fontSize = 20;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUILayout.Label(headerName);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.skin.label.fontStyle = FontStyle.Normal;
            GUILayout.Space(20);
        }

        private string MakeFirstIndexUppercase(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}
