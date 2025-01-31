using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station03
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var playerRigidbody = playerObject.GetComponent<Rigidbody>();
            Assert.That(playerRigidbody, Is.Not.Null, "PlayerにRigidbodyがアタッチされていません");

            var playerController = TestUtility.GetComponent(playerObject, "PlayerController");
            Assert.That(playerController, Is.Not.Null, "PlayerにPlayerControllerがアタッチされていません");
        }
    }
}
