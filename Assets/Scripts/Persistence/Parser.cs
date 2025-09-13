using UnityEngine;

public interface IReadonlyStorage<T> where T : class
{
    T? LoadFrom(string origin);
}


public sealed class ResourcesJsonParser<T> : IReadonlyStorage<T> where T : class
{
    public T? LoadFrom(string path)
    {
        TextAsset json = Resources.Load<TextAsset>(path);
        if (json == null)
        {
            return null;
        }
        
        return JsonUtility.FromJson<T>(json.text);
    }
}