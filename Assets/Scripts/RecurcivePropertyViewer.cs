using System.Collections.Generic;
using System.Reflection;
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
        return obj.GetType().ToString() + ":" + ViewLog(obj,ref objList);
    }

    //機能の実態
    //受け取ったobjのフィールドの配列でループを回し
    //それぞれの値を取得して、場合によっては再帰をかけたい
    private static string ViewLog(object obj,ref List<object> objList)
    {
        string log = "";
        System.Type type = obj.GetType();

        var properties = type.GetProperties();


        if(properties == null)//このobjectがフィールドを持ってないならループせずに再帰終了
        {
            return "";
        }
        foreach (var property in properties)
        {
            try
            {
                var propertyObj = property.GetValue(obj);
                string result = GetResult(propertyObj,ref objList);
                log += property.Name + ":" + result + ",";
            }
            catch (System.Exception e)
            {
                 //log = field.Name + ":" + e.ToString() + ",";
            }
        }
        return log;
    }


    //再帰するときに毎回この処理したいなって思ったけどきもい
    public static string GetNestViewLog(object nestObj, ref List<object> objList)
    {
        return GetNestLog(ViewLog(nestObj, ref objList));
    }
    
    
    
    public static string GetResult(object obj,ref List<object> objList)
    {
        try
        {
            if (obj == null)//objがnullならnull
            {
                return "null";
            }
            if (IsPrimitive(obj))//単純型ならどうする
            {
                return obj.ToString();
            }
            if (IsExistProperty(obj, objList))//循環参照があったらどうする
            {
                return "zyunkan";
            }
            if (!obj.GetType().IsValueType)//値型じゃないならリストに保存
            {
                objList.Add(obj);
            }
            return GetNestViewLog(obj, ref objList);
            return "sonota";
        }
        catch (System.Exception e)
        {
            return e.ToString();
        }
    }

    //自己参照型および相互参照型の定義はプリミティブ型にのみ許された行為らしい
    //いれたobjがプリミティブかどうか判定
    public static bool IsPrimitive(object obj)
    {
        return obj.GetType().IsPrimitive;
    }

    //そのリストにそのobjが含まれてるかどうか
    public static bool IsExistProperty(object obj, List<object> list)
    {
        return list.Exists(youso => youso == obj);
    }

    //ネストするときは改行して中括弧つけたいなって思った
    public static string GetNestLog(string nestLog)
    {
        if (nestLog != "")
        {
            return "{" + nestLog + "}";
        }
        else
        {
            return "null";
        }
    }
}
