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
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //�ν��Ͻ� ���̵� �ָ� ������Ʈ�� ��ȯ�Ѵ�.

        if(obj != null && _coloredObjects.ContainsKey(obj))
        {
            GameObject gameobj = obj as GameObject; //�ٿ�ĳ�����̶� �������� ����
            ColorInHierarchy cih = gameobj.GetComponent<ColorInHierarchy>();

            if(cih != null)
            {
                //���ϴ´�� �׸���
                PaintObject(gameobj, selectionRect, cih);

            }
            else
            {
                 _coloredObjects.Remove(obj); //����ڰ� ������Ʈ�� ������ ��
            }
        }
    }

    public static void PaintObject(Object obj, Rect selectionrect, ColorInHierarchy cih)
    {
        //���ȭ��
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

    private void OnValidate() //��������Ʈ �̺�Ʈ�� ���ö���(���콺�� �����϶�) 
    {
        if(_coloredObjects.ContainsKey(this.gameObject) == false)
        {
            _coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
