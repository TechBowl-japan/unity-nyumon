using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station02
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var groundObject = GameObject.Find("Ground");
            Assert.That(groundObject, Is.Not.Null, "Groundが存在しません");

            var groundMaterial = groundObject.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(groundMaterial, Is.Not.Null, "GroundにMaterialがアタッチされていません");
            Assert.That((Color32)groundMaterial.color, Is.EqualTo(new Color32(130, 130, 130, 255)), "Background Materialのcolorが設定と違います");

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var playerMaterial = playerObject.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(playerMaterial, Is.Not.Null, "PlayerにMaterialがアタッチされていません");
            Assert.That((Color32)playerMaterial.color, Is.EqualTo(new Color32(0, 220, 255, 255)), "Player Materialのcolorが設定と違います");

            var lightObject = GameObject.Find("Directional Light");
            Assert.That(lightObject, Is.Not.Null, "Directional Lightが存在しません");
            
            var light = lightObject.GetComponent<Light>();
            Assert.That((Color32)light.color, Is.EqualTo(new Color32(255, 255, 255, 255)), "Directional Lightのcolorが設定と違います");
        }
    }
}
