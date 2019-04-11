using System.Collections.Generic;
using UnityEngine;
public class RecurcivePropertyViewer
{
    
    //これを使って呼び出す
    public static string ViewLogMain(object obj)
    {
        //リストを初期化して、最初のobjectを追加してる
        var objList = new List<object>();
        if (!obj.GetType().IsValueType)//最初のobjが参照型ならリストに加える
        {
            objList.Add(obj);
        }
        string blank = "        ";
        return obj.GetType().ToString() + ":{\n" + ViewLog(obj,ref objList,blank) + "}";
    }

    //機能の実態
    //受け取ったobjのフィールドの配列でループを回し
    //それぞれの値を取得して、場合によっては再帰をかけたい
    private static string ViewLog(object obj,ref List<object> objList,string blank)
    {
        string log = "";
        var properties = obj.GetType().GetProperties();
        if(properties == null)//このobjectがフィールドを持ってないならループせずに再帰終了
        {
            return "null";
        }
        foreach (var property in properties)
        {
            try
            {
                log += blank + property.Name + ":" + GetResult(property.GetValue(obj), ref objList,blank) + ",\n";
            }catch (System.Exception){}
        }
        return log;
    }
       
    public static string GetResult(object obj,ref List<object> objList,string blank)
    {
        try
        {
            var type = obj.GetType();
            if (obj == null)//objがnullならnull
            {
                return "null";
            }
            if (type.IsPrimitive)//単純型ならどうする
            {
                //return obj.ToString();
                return obj.ToString();
            }
            if (type.IsEnum)
            {
                return obj.ToString();
            }
            if (type.IsValueType)//構造体はToStringしとく
            {
                if(type == typeof(Matrix4x4))
                {
                    return "行列インデントやだ";
                }
                return obj.ToString();
            }
            if(type == typeof(string))//stringならそのまま返す
            {
                return (string)obj;
            }
            if (IsExistProperty(obj, objList))//循環参照があったらどうする
            {
                return "もう見た";
            }
            objList.Add(obj);
            if (type.IsArray)
            {

            }
            
            return "{\n" + ViewLog(obj, ref objList,blank + "        ") + blank + "}";
        }catch (System.Exception){ return ""; }
    }

    
    //そのリストにそのobjが含まれてるかどうか
    public static bool IsExistProperty(object obj, List<object> list)
    {
        return list.Exists(youso => youso == obj);
    }

}
