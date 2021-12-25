using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(GameEventTrigger))]
public class GameEventTriggerEditor : Editor
{
    private SerializedProperty eventsProperty;
    private GUIContent minusIcon;
    private GUIContent eventName;
 
    [SerializeField]
    private List<GameEvent> gameEvents = new List<GameEvent>();
    [SerializeField]
    private List<GUIContent> menuItems = new List<GUIContent>();

    void OnEnable()
    {
        eventName = new GUIContent("");
        minusIcon = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
        eventsProperty = serializedObject.FindProperty("events");

        var ids = AssetDatabase.FindAssets("t:GameEvent");
        foreach (var id in ids)
        {
            var gameEvent = (GameEvent)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(id), typeof(GameEvent));

            gameEvents.Add(gameEvent);
            menuItems.Add(new GUIContent(gameEvent.name));
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Handles all callback responses of the defined Event", MessageType.Info);
        GUILayout.Space(15f);
        serializedObject.Update();

        var removeIndex = -1;
        var removeButtonSize = GUIStyle.none.CalcSize(minusIcon);

        for (int i = 0; i < eventsProperty.arraySize; ++i)
        {
            var ev = eventsProperty.GetArrayElementAtIndex(i);
            var gameEvent = ev.FindPropertyRelative("gameEvent");
            var response = ev.FindPropertyRelative("response");
            var name = ev.FindPropertyRelative("name");
            eventName.text = name.stringValue;

            gameEvent.isExpanded = EditorGUILayout.Foldout(gameEvent.isExpanded, eventName.text);

            if (gameEvent.isExpanded)
            {
                //EditorGUILayout.PropertyField(gameEvent);
                EditorGUILayout.PropertyField(response, eventName);
                var responseRect = GUILayoutUtility.GetLastRect();
                var removeButtonPosition = new Rect(responseRect.xMax - removeButtonSize.x - 8, responseRect.y + 1, removeButtonSize.x, removeButtonSize.y);

                if (GUI.Button(removeButtonPosition, minusIcon, GUIStyle.none))
                {
                    removeIndex = i;
                }
            }

            GUILayout.Space(5f);
        }

        if (removeIndex > -1)
            RemoveEvent(removeIndex);

        if (GUILayout.Button("Add New Event"))
        {
            DrawMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void RemoveEvent(int index)
    {
        eventsProperty.DeleteArrayElementAtIndex(index);
    }

    void DrawMenu()
    {
        GenericMenu menu = new GenericMenu();
        
        for (int i = 0; i < menuItems.Count; i++)
        {
            var used = false;
            var name = menuItems[i];

            for (int j = 0; j < eventsProperty.arraySize; j++)
            {
                var ev = eventsProperty.GetArrayElementAtIndex(j);

                if (string.Compare(menuItems[i].text, ev.FindPropertyRelative("name").stringValue) == 0)//(menuItems[i].text.Contains(ev.FindPropertyRelative("name").stringValue))
                    used = true;
            }
            
            if(used)
                menu.AddDisabledItem(name, false);
            else
                menu.AddItem(name, false, AddEvent, i);
        }

        menu.ShowAsContext();
    }

    void AddEvent(object index)
    {
        eventsProperty.arraySize += 1;
        var gameEvent = eventsProperty.GetArrayElementAtIndex(eventsProperty.arraySize-1).FindPropertyRelative("gameEvent");
        var eventName = eventsProperty.GetArrayElementAtIndex(eventsProperty.arraySize - 1).FindPropertyRelative("name");
        
        gameEvent.objectReferenceValue = gameEvents[(int)index];
        eventName.stringValue = gameEvents[(int)index].name;
        
        serializedObject.ApplyModifiedProperties();
    }
}
