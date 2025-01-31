using NUnit.Framework;
using System;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests
{
    public static class TestUtility
    {
        public static IEnumerator LoadScene(string sceneName = "MiniGame")
        {
            if (!Application.CanStreamedLevelBeLoaded(sceneName))
            {
                Assert.That(false, Is.True, $"Build SettingsのScenes In Buildに{sceneName}が追加されていません");
                yield break;
            }

            var sceneLoading = new TaskCompletionSource<bool>();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.completed += _ => sceneLoading.SetResult(true);
            yield return new WaitUntil(() => sceneLoading.Task.IsCompleted);
        }

        public static Component GetComponent(GameObject gameObject, string type, string assembly = "Assembly-CSharp")
        {
            var methodInfo = typeof(GameObject).GetMethod("GetComponent", new Type[] {});
            var t = Type.GetType($"{type}, {assembly}");
            if (t == null)
            {
                Assert.That(false, Is.True, $"{type}が存在しません");
                return null;
            }

            var genericMethod = methodInfo.MakeGenericMethod(t);
            return (Component)genericMethod.Invoke(gameObject, null);
        }

        public static void InputSystemQueueStateEvent(string key)
        {
            var keyboardType = Type.GetType("UnityEngine.InputSystem.Keyboard, Unity.InputSystem");
            var keyboardCurrentProperty = keyboardType.GetProperty("current");
            var keyboardCurrent = keyboardCurrentProperty.GetValue(null);

            var keyType = Type.GetType("UnityEngine.InputSystem.Key, Unity.InputSystem");
            var leftArrowField = keyType.GetField(key, BindingFlags.Static | BindingFlags.Public);
            var keyboardStateType = Type.GetType("UnityEngine.InputSystem.LowLevel.KeyboardState, Unity.InputSystem");
            var keyboardState = Activator.CreateInstance(keyboardStateType, new object[] { leftArrowField.GetValue(null) });

            var inputSystemType = Type.GetType("UnityEngine.InputSystem.InputSystem, Unity.InputSystem");
            var queueStateEventMethod = inputSystemType.GetMethod("QueueStateEvent", BindingFlags.Public | BindingFlags.Static);
            var genericQueueStateEventMethod = queueStateEventMethod.MakeGenericMethod(keyboardStateType);
            genericQueueStateEventMethod.Invoke(null, new object[] { keyboardCurrent, keyboardState, -1.0 });
        }
    }
}
