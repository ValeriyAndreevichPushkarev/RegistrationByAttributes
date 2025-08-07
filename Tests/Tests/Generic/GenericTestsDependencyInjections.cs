using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes;
using RegistrationByAttributes.Realizations;
using test.Tests.Simple.TestOneToOne;

namespace Tests
{
    [TestClass]
    public sealed class GenericTestsDependencyInjections
    {
        [TestMethod]
        public void TestOneToOne()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneGeneric<int>>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneGeneric<int>>() == sp.GetService<IOneToOneGeneric<int>>());
        }

        [TestMethod]
        public void TestOneToOneOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneGenericOverride<int>>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneGenericOverride<int>>() != sp.GetService<IOneToOneGenericOverride<int>>());
        }
        [TestMethod]
        public void TestOneToMany()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToManyGeneric<int>>()?.Count() == 2);

            Assert.IsTrue(sp.GetServices<IOneToManyGeneric<int>>().First() == sp.GetServices<IOneToManyGeneric<int>>().First());
            Assert.IsTrue(sp.GetServices<IOneToManyGeneric<int>>().Skip(1).First() == sp.GetServices<IOneToManyGeneric<int>>().Skip(1).First());
        }

        [TestMethod]
        public void TestOneToManyOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToManyGenericOverride<int>>()?.Count() == 2);

            Assert.IsTrue(sp.GetServices<IOneToManyGenericOverride<int>>().Where(i => i is OneToManyOneGenericOverride).First() != sp.GetServices<IOneToManyGenericOverride<int>>().Where(i => i is OneToManyOneGenericOverride).First());
            Assert.IsTrue(sp.GetServices<IOneToManyGenericOverride<int>>().Where(i => i is OneToManyTwoGenericOverride).First() == sp.GetServices<IOneToManyGenericOverride<int>>().Where(i => i is OneToManyTwoGenericOverride).First());
        }
    }
}
