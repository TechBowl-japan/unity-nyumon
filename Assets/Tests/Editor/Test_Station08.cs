using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station08
    {
        [UnityTest]
        [Category("Station08")]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var pickUpParentObject = GameObject.Find("PickUp Parent");
            Assert.That(pickUpParentObject, Is.Not.Null, "PickUp Parentが存在しません");

            var pickUpRenderers = pickUpParentObject.transform.GetComponentsInChildren<MeshRenderer>().Where(x => x.CompareTag("PickUp")).ToArray();
            Assert.That(pickUpRenderers.Length, Is.EqualTo(4), "PickUp Parentの子にPickUpが4つ存在していません");

            TestUtility.InputSystemQueueStateEvent("LeftArrow");

            var elapsedTime = 0f;
            while (elapsedTime < 3f)
            {
                if (pickUpRenderers.Any(x => !x.gameObject.activeSelf))
                {
                    Assert.That(true, Is.True);
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Assert.That(false, Is.True, "PickUpが消えません");
        }
    }
}
