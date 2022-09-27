using UnityEngine;

public static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        if (GameObject.FindGameObjectWithTag("Systems")) return;
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
    }
}