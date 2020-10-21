using AssemblyBrowserLib;
using AssemblyBrowserLib.AssemblyStruct;
using NUnit.Framework;
using System;
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
            AssemblyInfo.LoadAssembly("UnitTest.dll");
            assemblyStruct = AssemblyInfo.assemblyStruct;
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
                type => type.FullName == "public class Tests"));
        }

        [Test]
        public void TestMembersNaming()
        {
            AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.FullName == "public class Tests").
                First();

            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.FullName == "private Int32 testField").Count());
            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.FullName == "Int32 testProperty { private get; private set; }").Count());
            Assert.AreEqual(1, assemblyDataType.Fields.Where(field => field.FullName == "private Int32 testMethod(in Int32& testParam)").Count());
        }

        [Test]
        public void TestGenericType()
        {
            Assert.IsNotEmpty(assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.FullName == "private sealed struct TestStruct<T>"));

           AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.FullName == "private sealed struct TestStruct<T>").
                First();

            Assert.IsNotEmpty(
                assemblyDataType.Fields.Where(field => field.FullName == "public T testT"));
        }

        [Test]
        public void TestCounting()
        {
            var flags = BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.NonPublic |
            BindingFlags.Public | 
            BindingFlags.DeclaredOnly;

            Assembly assembly = Assembly.GetExecutingAssembly();

            Assert.AreEqual(assembly.GetTypes().GroupBy(type => type.Namespace).Count(), assemblyStruct.Namespaces.Count());

            foreach (var typeGroups in assembly.GetTypes().GroupBy(type => type.Namespace))
            {
                var assemblyNamespace = assemblyStruct.Namespaces.Where(
                    assemblyNamespace => assemblyNamespace.Name == (typeGroups.Key ?? "<Without namespace>")).First();

                Assert.AreEqual(typeGroups.ToList().Count(), assemblyNamespace.DataTypes.Count());

                foreach (var type in typeGroups.ToList())
                {
                    var assemblyType = assemblyNamespace.DataTypes.Where(
                    assemblyType => assemblyType.Name == type.Name).First();

                    Assert.AreEqual(
                        type.GetFields(flags).Count() + type.GetProperties(flags).Count() + type.GetMethods(flags).Count(),
                       assemblyType.Fields.Count());
                }
            }
        }

        [Test]
        public void TestExtensionMethod()
        {
            AssemblyInfo.LoadAssembly("ExtensionMethodsExamples.dll");
            assemblyStruct = AssemblyInfo.assemblyStruct;

            Assert.IsNotEmpty(assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System"));

            Assert.IsNotEmpty(assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System").
                First().DataTypes.Where(
                type => type.FullName.Contains("String")));

            AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == "System").
                First().DataTypes.Where(
                type => type.FullName.Contains("String")).
                First();

            Assert.IsNotEmpty(
                assemblyDataType.Fields.Where(field => field.FullName.Contains("CharCount")));
        }
    }
}