using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes;
using RegistrationByAttributes.Realizations;
using test.Tests.Simple.TestOneToOne;

namespace Tests
{
    [TestClass]
    public sealed class SimpleTestsDependencyInjections
    {
        [TestMethod]
        public void TestOneToOne()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOne>() != null);

            Assert.IsTrue(sp.GetService<IOneToOne>() == sp.GetService<IOneToOne>());
        }

        [TestMethod]
        public void TestOneToOneOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneOverride>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneOverride>() != sp.GetService<IOneToOneOverride>());
        }
        [TestMethod]
        public void TestOneToMany()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToMany>()?.Count()==2);

            Assert.IsTrue(sp.GetServices<IOneToMany>().First() == sp.GetServices<IOneToMany>().First());
            Assert.IsTrue(sp.GetServices<IOneToMany>().Skip(1).First() == sp.GetServices<IOneToMany>().Skip(1).First());
        }

        [TestMethod]
        public void TestOneToManyOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToManyOverride>()?.Count() == 2);

            Assert.IsTrue(sp.GetServices<IOneToManyOverride>().Where(i=>i is OneToManyOneOverride).First() != sp.GetServices<IOneToManyOverride>().Where(i => i is OneToManyOneOverride).First());
            Assert.IsTrue(sp.GetServices<IOneToManyOverride>().Where(i => i is OneToManyTwoOverride).First() == sp.GetServices<IOneToManyOverride>().Where(i => i is OneToManyTwoOverride).First());
        }
    }
}
