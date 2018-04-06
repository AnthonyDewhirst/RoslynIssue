using System;
using TechTalk.SpecFlow;

namespace TechTalk.SpecFlow
{
    public abstract class StepDefinitionBaseAttribute : Attribute
    {
    }

    public class GivenAttribute : StepDefinitionBaseAttribute
    {
        public GivenAttribute(string msg) { }
    }
}

namespace ConsoleApplication1
{
    class TypeName
    {
        [Given("hdhd")]
        void Method0() { }

        [Given("hdhd")]
        public void Method1() { }

        /// <summary>
        ///
        /// </summary
        [Given("hdhd")]
        public void Method2() { }

        /// <summary>
        ///
        /// </summary>
        [Given("hdhd")]
        public void Method3() { }
    }
}