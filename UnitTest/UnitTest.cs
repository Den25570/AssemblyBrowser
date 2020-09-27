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
            assemblyStruct = AssemblyInfo.GetAssemblyInfo();
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

            Assert.IsNotEmpty(
                assemblyDataType.Fields.Where(field => field.FullName == "private Int32 testField"));
            Assert.IsNotEmpty(
               assemblyDataType.Fields.Where(field => field.FullName == "Int32 testProperty { private get; private set;}"));
            Assert.IsNotEmpty(
               assemblyDataType.Fields.Where(field => field.FullName == "private Int32 testMethod(in Int32& testParam)"));
        }

        [Test]
        public void TestGenericType()
        {
            Assert.IsNotEmpty(assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.FullName == "private sealed struct TestStruct`1<T >"));

           AssemblyDataType assemblyDataType = assemblyStruct.Namespaces.Where(
                assemblyNamespace => assemblyNamespace.Name == typeof(Tests).Namespace).
                First().DataTypes.Where(
                type => type.FullName == "private sealed struct TestStruct`1<T >").
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
            BindingFlags.Public;

            Assembly assembly = Assembly.GetExecutingAssembly();

            var a = assembly.GetTypes().GroupBy(type => type.Namespace);
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
    }
}