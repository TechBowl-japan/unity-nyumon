using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station05
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var cameraObject = GameObject.Find("Main Camera");
            Assert.That(cameraObject, Is.Not.Null, "Main Cameraが存在しません");

            var initialPosition = cameraObject.transform.position;

            TestUtility.InputSystemQueueStateEvent("LeftArrow");
            yield return new WaitForSeconds(0.2f);

            Assert.That(cameraObject.transform.position, Is.Not.EqualTo(initialPosition), "Playerが移動してもCameraが追従しません");
        }
    }
}
