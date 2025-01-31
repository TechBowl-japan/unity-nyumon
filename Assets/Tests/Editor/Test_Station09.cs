using NUnit.Framework;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station09
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var playerController = TestUtility.GetComponent(playerObject, "PlayerController");
            Assert.That(playerController, Is.Not.Null, "PlayerにPlayerControllerがアタッチされていません");

            var playerControllerType = Type.GetType("PlayerController, Assembly-CSharp");
            var countField = playerControllerType.GetField("count", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(countField, Is.Not.Null, "PlayerControllerにcountフィールドがありません");

            TestUtility.InputSystemQueueStateEvent("LeftArrow");

            var elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                if ((int)countField.GetValue(playerController) > 0)
                {
                    var countTextObject = GameObject.Find("CountText");
                    Assert.That(countTextObject, Is.Not.Null, "CountTextが存在しません");

                    var countText = TestUtility.GetComponent(countTextObject, "TMPro.TextMeshProUGUI", "Unity.TextMeshPro");
                    var textMeshProType = Type.GetType("TMPro.TextMeshProUGUI, Unity.TextMeshPro");
                    var textField = textMeshProType.GetProperty("text", BindingFlags.Public | BindingFlags.Instance);
                    var text = (string)textField.GetValue(countText);
                    Assert.That(text[^1] == '1', Is.True, "countTextが更新されません");
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Assert.That(false, Is.True, "countが増えません");
        }
    }
}
