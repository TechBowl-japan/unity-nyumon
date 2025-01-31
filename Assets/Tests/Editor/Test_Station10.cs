using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station10
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var playerController = TestUtility.GetComponent(playerObject, "PlayerController");
            Assert.That(playerController, Is.Not.Null, "PlayerにPlayerControllerがアタッチされていません");

            var type = Type.GetType("PlayerController, Assembly-CSharp");
            var onTriggerEnterMethod = type.GetMethod("OnTriggerEnter", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(onTriggerEnterMethod, Is.Not.Null, "PlayerControllerにOnTriggerEnterメソッドがありません");

            var pickUpParentObject = GameObject.Find("PickUp Parent");
            Assert.That(pickUpParentObject, Is.Not.Null, "PickUp Parentが存在しません");

            var pickUpColliders = pickUpParentObject.transform.GetComponentsInChildren<BoxCollider>().Where(x => x.CompareTag("PickUp")).ToArray();
            Assert.That(pickUpColliders.Length, Is.EqualTo(4), "PickUp Parentの子にPickUpが4つ存在していません");

            foreach (var collider in pickUpColliders)
            {
                onTriggerEnterMethod.Invoke(playerController, new object[] { collider });
            }

            var canvas = UnityEngine.Object.FindAnyObjectByType<Canvas>();
            Assert.That(canvas, Is.Not.Null, "Canvasが存在しません");

            var winTextTransform = canvas.transform.Find("WinText");
            Assert.That(winTextTransform, Is.Not.Null, "WinTextが存在しません");
            Assert.That(winTextTransform.gameObject.activeSelf, Is.True, "WinTextが表示されていません");
        }
    }
}
