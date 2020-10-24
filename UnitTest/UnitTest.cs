using AssemblyBrowserLib;
using AssemblyBrowserLib.AssemblyStruct;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace UnitTest
{
    public class Tests
    {
        private AssemblyStruct assemblyStruct;

        private int testField;
        private int testProperty {get; set;}

        private struct TestStruct<T>
        {
            public T testT;
        }

        private int testMethod(in int testParam)
        {
            return 0;
        }

        [SetUp]
        public void Setup()
        {
            assemblyStruct = new AssemblyStruct(Assembly.LoadFrom("UnitTest.dll"));
        }

        [Test]
        public void TestNamespaces()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach(AssemblyNamespace assemblyNamespace in assemblyStruct.Namespaces)
            {
                Assert.IsNotEmpty(assembly.GetTypes().Where(type => assemblyNamespace.Name == (type.Namespace ?? "<Without namespace>")));
            }                    
        }

        [Test]
        public void TestTypesNaming()
        {
            Assert.IsNotEmpty(
                assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.GetFullName() == "public class Tests"));
        }

        [Test]
        public void TestMembersNaming()
        {
            AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.GetFullName() == "public class Tests").
                First();

            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.GetFullName() == "private Int32 testField").Count());
            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.GetFullName() == "Int32 testProperty { private get; private set; }").Count());
            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.GetFullName() == "private Int32 testMethod(in Int32& testParam)").Count());
        }

        [Test]
        public void TestGenericType()
        {
            Assert.IsNotEmpty(assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.GetFullName() == "private sealed struct TestStruct<T>"));

           AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.GetFullName() == "private sealed struct TestStruct<T>").
                First();

            Assert.IsNotEmpty(
                assemblyDataType.Fields.Where(field => field.GetFullName() == "public T testT"));
        }
        
        [Test]
        public void TestCounting()
        {
            var flags = BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.NonPublic |
            BindingFlags.Public | 
            BindingFlags.DeclaredOnly;

            Assert.AreEqual(2, assemblyStruct.Namespaces.Count());

            var dataTypes = assemblyStruct.Namespaces.Where(@namespace => @namespace.Name == "UnitTest").First().DataTypes;
            Assert.AreEqual(4, dataTypes.Count());

            var typeMembers = dataTypes.Where(member => member.Name == "Tests").First().Fields;
            Assert.AreEqual(14, typeMembers.Count());
        }

        [Test]
        public void TestExtensionMethod()
        {
            assemblyStruct = new AssemblyStruct(Assembly.LoadFrom("ExtensionMethodsExamples.dll"));

            Assert.AreEqual(1, assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System").Count());

            Assert.AreEqual(1, assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System").
                First().DataTypes.Where(
                type => type.GetFullName().Contains("String")).Count());

            AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System").
                First().DataTypes.Where(
                type => type.GetFullName().Contains("String")).
                First();

            Assert.AreEqual(1, 
                assemblyDataType.Fields.Where(field => field.GetFullName().Contains("CharCount")).Count());
        }
    }
}