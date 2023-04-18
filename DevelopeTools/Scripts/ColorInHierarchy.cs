using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorInHierarchy : MonoBehaviour
{
    public string prefix;
    public Color backColor;
    public Color FontColor;
    

#if UNITY_EDITOR
    private static Dictionary<Object, ColorInHierarchy> _coloredObjects = new Dictionary<Object, ColorInHierarchy>();

    static ColorInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //인스턴스 아이디를 주면 오브젝트를 반환한다.

        if(obj != null && _coloredObjects.ContainsKey(obj))
        {
            GameObject gameobj = obj as GameObject; //다운캐스팅이라 안전하진 않음
            ColorInHierarchy cih = gameobj.GetComponent<ColorInHierarchy>();

            if(cih != null)
            {
                //원하는대로 그리기
                PaintObject(gameobj, selectionRect, cih);

            }
            else
            {
                 _coloredObjects.Remove(obj); //사용자가 컴포넌트를 제거한 것
            }
        }
    }

    public static void PaintObject(Object obj, Rect selectionrect, ColorInHierarchy cih)
    {
        //배경화면
        Rect bgRect = new Rect(selectionrect.x, selectionrect.y, selectionrect.width + 50, selectionrect.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, cih.backColor);

            string name = $"{cih.prefix} {obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = cih.FontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate() //벨리데이트 이벤트가 들어올때만(마우스가 움직일때) 
    {
        if(_coloredObjects.ContainsKey(this.gameObject) == false)
        {
            _coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
