using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Station07
    {
        [UnityTest]
        [Category("Station07")]
        public IEnumerator Test()
        {
            yield return TestUtility.LoadScene();

            var pickUpParentObject = GameObject.Find("PickUp Parent");
            Assert.That(pickUpParentObject, Is.Not.Null, "PickUp Parentが存在しません");

            var pickUpRenderers = pickUpParentObject.transform.GetComponentsInChildren<MeshRenderer>().Where(x => x.name.StartsWith("PickUp")).ToArray();
            Assert.That(pickUpRenderers.Length, Is.EqualTo(4), "PickUp Parentの子にPickUpが4つ存在していません");

            var initialRotation = pickUpRenderers[0].transform.rotation;

            yield return new WaitForSeconds(0.1f);

            Assert.That(pickUpRenderers[0].transform.rotation, Is.Not.EqualTo(initialRotation), "PickUpが回転していません");
        }
    }
}
