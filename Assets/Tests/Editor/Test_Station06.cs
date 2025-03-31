using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station06
    {
        [UnityTest]
        [Category("Station06")]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var wallsObject = GameObject.Find("Walls");
            Assert.That(wallsObject, Is.Not.Null, "Wallsが存在しません");

            var wallCount = wallsObject.transform.GetComponentsInChildren<MeshRenderer>().Count(x => x.name.EndsWith("Wall"));
            Assert.That(wallCount, Is.EqualTo(4), "Wallsの子に壁ゲームオブジェクトが4つ存在していません");

            var playerObject = GameObject.Find("Player");
            Assert.That(playerObject, Is.Not.Null, "Playerが存在しません");

            var sphereCollider = playerObject.GetComponent<SphereCollider>();
            Assert.That(sphereCollider, Is.Not.Null, "PlayerにSphereColliderがアタッチされていません");

            TestUtility.InputSystemQueueStateEvent("LeftArrow");

            var elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                var colliders = Physics.OverlapSphere(sphereCollider.transform.position, sphereCollider.bounds.size.x);
                if (colliders.Any(x => x.name.EndsWith("Wall")))
                {
                    Assert.That(true, Is.True);
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Assert.That(false, Is.True, "Playerが壁にぶつかりません");
        }
    }
}
