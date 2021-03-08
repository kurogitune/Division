using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow//マップ作成に必要な情報設定用ウィンド
{
    [MenuItem("Editor/MapEditor")]
    static void Open()
    {
        GetWindow<MapEditor>("MapDataSetEditor");
        GetWindow<MapSet>("MapSetEditor",typeof(MapEditor));//こいつで指定したウィンドにくっつけて作成する
    }

    int[,] Mapdata = new int[0, 0];//マップ配列
    int MaxImages;//使用する画像数
    int SelctImageNo=-1;//選択している画像番号
    Texture[] UsedImage=new Texture[95];//作成に使用するテクスチャ
    Object[] UseSetImage = new Sprite[50];//作成に使用する画像
    bool ImagDataLoock;//画像変更できなくする
    Vector2 ImagV = Vector2.zero;
    [System.Obsolete]
    public void OnGUI()
    {
        using (new EditorGUILayout.HorizontalScope(GUI.skin.box))
        {
            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("作成時全画面推奨推奨");

                ImagDataLoock = EditorGUILayout.Toggle("画像変更ロック", ImagDataLoock);
                EditorGUI.BeginDisabledGroup(ImagDataLoock);
                MaxImages = EditorGUILayout.IntField("使用画像数", MaxImages);
                if (UseSetImage.Length != MaxImages)//画像個数変更処理
                {
                    UseSetImage = new Object[MaxImages];
                    UsedImage = new Texture[MaxImages];
                }

                for (int i = 0; i < UseSetImage.Length; i++)//画像代入処理
                {
                    UseSetImage[i] = EditorGUILayout.ObjectField(UseSetImage[i], objType: typeof(Sprite));
                    UsedImage[i] = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(UseSetImage[i]), typeof(Texture));
                }
                EditorGUI.EndDisabledGroup();
            }

            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                ImagV = EditorGUILayout.BeginScrollView(ImagV, GUI.skin.box);
                for (int i = 0; i < UsedImage.Length; i++)//画像選択処理
                {
                    if (i == SelctImageNo) GUI.backgroundColor = Color.red;
                    if (GUILayout.Button(UsedImage[i], GUILayout.Width(100), GUILayout.Height(100))& UsedImage[i])
                    {
                        SelctImageNo = i;
                        MapSet.IMageDataIN(UsedImage[i]);
                    }
                    GUI.backgroundColor = GUI.color;
                }
                EditorGUILayout.EndScrollView();
            }
        }       
    }
}

public class MapSet : EditorWindow//マップ配置ウィンド
{
    static Texture SetIMage;
    public static void IMageDataIN(Texture IMageIN)
    {
        SetIMage = IMageIN;
    }

    int WidthSize = 20;
    int HeightSize = 20;
    int Max_X = 95, Max_Y = 50;//最大マップデータ数
    int ToolNo = 0;//ツール番号
    Vector2 MousPoz;
    Texture[,] Map = new Texture[95, 50];
    private void OnGUI()
    {
        Handles.color = Color.white;
        for (int i=0;i< Max_X + 2 ; i++)//横線
        {
            Handles.DrawLine(new Vector2(WidthSize*i,0),new Vector2(WidthSize*i,position.size.y));//こいつで線を引ける
        }

        for(int i=0;i< Max_Y + 2; i++)//縦線
        {
            Handles.DrawLine(new Vector2(0,HeightSize*i), new Vector2(position.size.x,HeightSize*i));
        }
        MousPoz = Event.current.mousePosition;

        var e = Event.current;
        switch (e.type)
        {
            case EventType.ContextClick://右クリックメニュー
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("設置"), false, () => { ToolNo = 1;  });
                menu.AddItem(new GUIContent("削除"), false, () => { ToolNo = 2; });
                menu.AddItem(new GUIContent("コピー"), false, () => { ToolNo = 3; });
                menu.AddItem(new GUIContent("移動"), false, () => { ToolNo = 4; });
                menu.ShowAsContext();
                break;
        }
    }

    private void Update()
    {
        Repaint();
    }
}
