using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;

namespace Analyzer1.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {

        //No diagnostics expected to show up
        [TestMethod]
        public void EmptyStringNotErrors()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void MissingCommentsNotPublicNoDiagnostics()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    namespace TechTalk.SpecFlow
    {
        public abstract class StepDefinitionBaseAttribute : Attribute
        {
        }

        public class GivenAttribute : StepDefinitionBaseAttribute
        {
            public GivenAttribute(string msg) {}
        }
    }

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            [Given(""hdhd"")]
            void Method1(){}
        }
    }";
            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void MissingCommentsGetsDiagnostics()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    namespace TechTalk.SpecFlow
    {
        public abstract class StepDefinitionBaseAttribute : Attribute
        {
        }

        public class GivenAttribute : StepDefinitionBaseAttribute
        {
            public GivenAttribute(string msg) {}
        }
    }

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            [Given(""hdhd"")]
            public void Method1(){}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "SpecFlowAnalyzer1",
                Message = String.Format("Type name '{0}' contains lowercase letters", "Method1"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 27, 25)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }
        
        [TestMethod]
        public void CommentsInvalidXmlGetsDiagnostics()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    namespace TechTalk.SpecFlow
    {
        public abstract class StepDefinitionBaseAttribute : Attribute
        {
        }

        public class GivenAttribute : StepDefinitionBaseAttribute
        {
            public GivenAttribute(string msg) {}
        }
    }

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            /// <summary>
            ///
            /// </summary
            [Given(""hdhd"")]
            public void Method1(){}
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "SpecFlowAnalyzer1",
                Message = String.Format("Type name '{0}' contains lowercase letters", "Method1"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                        new DiagnosticResultLocation("Test0.cs", 30, 25)
                    }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        [TestMethod]
        public void CommentsValidXmlNoDiagnostics()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using TechTalk.SpecFlow;

    namespace TechTalk.SpecFlow
    {
        public abstract class StepDefinitionBaseAttribute : Attribute
        {
        }

        public class GivenAttribute : StepDefinitionBaseAttribute
        {
            public GivenAttribute(string msg) {}
        }
    }

    namespace ConsoleApplication1
    {
        class TypeName
        {   
            /// <summary>
            ///
            /// </summary>
            [Given(""hdhd"")]
            public void Method1(){}
        }
    }";

            VerifyCSharpDiagnostic(test);
        }


        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer1Analyzer();
        }
    }
}
