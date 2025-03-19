using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station11
    {
        [UnityTest]
        [Category("Station11")]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var playerMaterial = playerObject.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(playerMaterial, Is.Not.Null, "PlayerにMaterialがアタッチされていません");

            Assert.That((Color32)playerMaterial.color, Is.Not.EqualTo(new Color32(0, 220, 255, 255)), "Playerの色が変更されていません");
            Assert.That(playerObject.transform.position, Is.Not.EqualTo(new Vector3(0f, 0.5f, 0f)), "Playerの位置が変更されていません");

            var groundObeject = GameObject.Find("Ground");
            Assert.That(groundObeject, Is.Not.Null, "Groundが存在しません");

            var groundMaterial = groundObeject.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(groundMaterial, Is.Not.Null, "GroundにMaterialがアタッチされていません");

            var wallsObject = GameObject.Find("Walls");
            Assert.That(wallsObject, Is.Not.Null, "Wallsが存在しません");

            var isUnmatchGroundColor = !((Color32)groundMaterial.color).Equals(new Color32(130, 130, 130, 255));
            var wallRenderers = wallsObject.transform.GetComponentsInChildren<MeshRenderer>().Where(x => x.name.EndsWith("Wall")).ToArray();
            var isUnmatchWallColor = wallRenderers
                .Select(x => (Color32)x.sharedMaterial.color)
                .Any(color => !color.Equals(new Color32(79, 79, 79, 255)));
            Assert.That(isUnmatchGroundColor || isUnmatchWallColor, Is.True, "プレイエリアの色が変更されていません");

            var isUnmatchGroundPosition = groundObeject.transform.position != Vector3.zero;
            var initialWallPositions = new []
            {
                new Vector3(-10f, 0f, 0f),
                new Vector3(10f, 0f, 0f),
                new Vector3(0f, 0f, 10f),
                new Vector3(0f, 0f, -10f),
            };
            var isUnmatchWallPosition = wallRenderers
                .Select(x => x.transform.position)
                .Any(position => !initialWallPositions.Contains(position));
            Assert.That(isUnmatchGroundPosition || isUnmatchWallPosition, Is.True, "プレイエリアの配置が変更されていません");

            var pickUpParentObject = GameObject.Find("PickUp Parent");
            Assert.That(pickUpParentObject, Is.Not.Null, "PickUp Parentが存在しません");

            var pickUpRenderers = pickUpParentObject.transform.GetComponentsInChildren<MeshRenderer>().Where(x => x.CompareTag("PickUp")).ToArray();
            Assert.That(pickUpRenderers.Length, Is.GreaterThanOrEqualTo(8), "PickUp Parentの子にPickUpが8つ以上存在していません");

            var isUnmatchPickUpColor = pickUpRenderers
                .Select(x => (Color32)x.sharedMaterial.color)
                .Any(color => !color.Equals(new Color32(255, 200, 0, 255)));
            Assert.That(isUnmatchPickUpColor, Is.True, "PickUpの色が変更されていません");

            var initialPickUpPositions = new[]
            {
                new Vector3(0f, 0.5f, 5f),
                new Vector3(5f, 0.5f, 0f),
                new Vector3(0f, 0.5f, -5f),
                new Vector3(-5f, 0.5f, 0f),
            };
            var isUnmatchPickUpPosition = pickUpRenderers
                .Select(x => x.transform.position)
                .Any(position => !initialPickUpPositions.Contains(position));
            Assert.That(isUnmatchPickUpPosition, Is.True, "PickUpの配置が変更されていません");

            var countTextObject = GameObject.Find("CountText");
            Assert.That(countTextObject, Is.Not.Null, "CountTextが存在しません");

            var countText = TestUtility.GetComponent(countTextObject, "TMPro.TextMeshProUGUI", "Unity.TextMeshPro");
            var textMeshProType = Type.GetType("TMPro.TextMeshProUGUI, Unity.TextMeshPro");
            var colorField = textMeshProType.GetProperty("color", BindingFlags.Public | BindingFlags.Instance);
            var countTextColor = (Color)colorField.GetValue(countText);
            Assert.That((Color32)countTextColor, Is.Not.EqualTo(new Color32(255, 255, 255, 255)), "CountTextの色が変更されていません");
            Assert.That(countTextObject.transform.localPosition, Is.Not.EqualTo(new Vector3(-553f, 336f, 0f)), "CountTextの位置が変更されていません");

            var canvas = UnityEngine.Object.FindAnyObjectByType<Canvas>();
            Assert.That(canvas, Is.Not.Null, "Canvasが存在しません");

            var winTextTransform = canvas.transform.Find("WinText");
            Assert.That(winTextTransform, Is.Not.Null, "WinTextが存在しません");

            var winText = TestUtility.GetComponent(winTextTransform.gameObject, "TMPro.TextMeshProUGUI", "Unity.TextMeshPro");
            var winTextColor = (Color)colorField.GetValue(winText);
            Assert.That((Color32)winTextColor, Is.Not.EqualTo(new Color32(0, 0, 0, 255)), "WinTextの色が変更されていません");
            Assert.That(winTextTransform.localPosition, Is.Not.EqualTo(new Vector3(0f, 100f, 0f)), "WinTextの位置が変更されていません");

            var pickUp2Colliders = pickUpParentObject.transform.GetComponentsInChildren<BoxCollider>().Where(x => x.CompareTag("PickUp2")).ToArray();
            Assert.That(pickUp2Colliders.Length, Is.GreaterThanOrEqualTo(4), "PickUp Parentの子にPickUp2が4つ以上存在していません");

            var playerController = TestUtility.GetComponent(playerObject, "PlayerController");
            Assert.That(playerController, Is.Not.Null, "PlayerにPlayerControllerがアタッチされていません");

            var type = Type.GetType("PlayerController, Assembly-CSharp");
            var onTriggerEnterMethod = type.GetMethod("OnTriggerEnter", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(onTriggerEnterMethod, Is.Not.Null, "PlayerControllerにOnTriggerEnterメソッドがありません");

            var pickUpCollider = pickUpRenderers[0].gameObject.GetComponent<BoxCollider>();
            onTriggerEnterMethod.Invoke(playerController, new object[] { pickUpCollider });
            var textField = textMeshProType.GetProperty("text", BindingFlags.Public | BindingFlags.Instance);
            var countTextText = (string)textField.GetValue(countText);
            Assert.That(countTextText[^1] == '1', Is.True, "PickUpと衝突しても1点カウントされません");

            var pickUp2Collider = pickUp2Colliders[0];
            onTriggerEnterMethod.Invoke(playerController, new object[] { pickUp2Collider });
            countTextText = (string)textField.GetValue(countText);
            Assert.That(countTextText[^1] == '3', Is.True, "PickUp2と衝突しても2点カウントされません");
        }
    }
}
