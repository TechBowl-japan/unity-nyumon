using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station04
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var initialPosition = playerObject.transform.position;

            TestUtility.InputSystemQueueStateEvent("LeftArrow");
            yield return new WaitForSeconds(0.2f);

            Assert.That(playerObject.transform.position, Is.Not.EqualTo(initialPosition), "左矢印キーを押してもPlayerが移動しません");
        }
    }
}
