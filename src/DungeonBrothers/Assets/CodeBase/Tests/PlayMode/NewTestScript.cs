using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CodeBase.Tests.PlayMode
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            int i = 5;
        
            i.Should().Be(5);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            int i = 5;
        
            yield return new WaitForSeconds(2f);
            i = 6;
        
            i.Should().Be(6);
        }
    }
}
