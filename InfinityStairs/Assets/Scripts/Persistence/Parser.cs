using UnityEngine;

public interface IParser<T>
{
    T LoadFrom(string origin);
}

public class ResourcesJsonParser<T> : IParser<T>
{
    public T LoadFrom(string path)
    {
        // Resource 에 존재하는 파일을 읽어들인다.
        TextAsset textAsset = Resources.Load<TextAsset>(path);

        // json 역직렬화 한다.
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}