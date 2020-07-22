using Interfaces;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelLoaderArgs
    {
        public static void Load (string sceneName, int args)
        {
            UnityAction<Scene, Scene> changeHandler = null;

            changeHandler = (from, to) =>
            {
                if (to.name == sceneName)
                {
                    SceneManager.activeSceneChanged -= changeHandler;
                               
                    foreach (var rootObject in to.GetRootGameObjects())
                    foreach (var handler in rootObject.GetComponentsInChildren<ILevelLoaderArgsHandler>())
                        handler.OnLevelLoad(args);
                }
            };

            SceneManager.activeSceneChanged += changeHandler;
            SceneManager.LoadScene(sceneName);
        }
    }
}