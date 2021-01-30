using UnityEngine;
using UnityEditor;

public class AutomaticUIAnchoringEditor : Editor
{
    private static void Anchor(RectTransform rectTransform)
    {
        RectTransform parentRectTransform = null;
        if (rectTransform.transform.parent)
            parentRectTransform = rectTransform.transform.parent.GetComponent<RectTransform>();

        if (!parentRectTransform)
            return;

        Undo.RecordObject(rectTransform, "Anchor UI Object");
        Rect parentRect = parentRectTransform.rect;
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + (rectTransform.offsetMin.x / parentRect.width), rectTransform.anchorMin.y + (rectTransform.offsetMin.y / parentRect.height));
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + (rectTransform.offsetMax.x / parentRect.width), rectTransform.anchorMax.y + (rectTransform.offsetMax.y / parentRect.height));
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    [MenuItem("Tools/Automatic UI Anchoring/Anchor Selected UI Objects _F1")]
    private static void AnchorSelectedUIObjects()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            RectTransform rectTransform = Selection.gameObjects[i].GetComponent<RectTransform>();
            if (rectTransform)
                Anchor(rectTransform);
        }
    }

    // Added "deanchoring" for the cases when resizing the parent transform is needed without resizing the anchored children
    private static void Deanchor(RectTransform rectTransform)
    {
        RectTransform parentRectTransform = null;
        if (rectTransform.transform.parent)
            parentRectTransform = rectTransform.transform.parent.GetComponent<RectTransform>();

        if (!parentRectTransform)
            return;

        Undo.RecordObject(rectTransform, "Deanchor UI Object");
        Rect parentRect = parentRectTransform.rect;
        rectTransform.offsetMin = new Vector2(parentRect.width * rectTransform.anchorMin.x, -(parentRect.height - parentRect.height * rectTransform.anchorMin.y));
        rectTransform.offsetMax = new Vector2(parentRect.width * rectTransform.anchorMax.x, -(parentRect.height - parentRect.height * rectTransform.anchorMax.y));
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    [MenuItem("Tools/Automatic UI Anchoring/Deanchor Selected UI Objects _#F1")]
    private static void DeanchorSelectedUIObjects()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            RectTransform rectTransform = Selection.gameObjects[i].GetComponent<RectTransform>();
            if (rectTransform)
                Deanchor(rectTransform);
        }
    }
}
