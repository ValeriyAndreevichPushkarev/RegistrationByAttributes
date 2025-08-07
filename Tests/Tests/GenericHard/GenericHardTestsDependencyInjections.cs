using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes;
using RegistrationByAttributes.Realizations;
using test.Tests.Simple.TestOneToOne;
using Tests.Tests.GenericHard.Mixed;

namespace Tests
{
    [TestClass]
    public sealed class GenericHardTestsDependencyInjections
    {
        [TestMethod]
        public void TestOneToOne()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneGenericHard<int>>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneGenericHard<int>>() == sp.GetService<IOneToOneGenericHard<int>>());
        }


        [TestMethod]
        public void TestOneToOneStr()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneGenericHard<string>>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneGenericHard<string>>() == sp.GetService<IOneToOneGenericHard<string>>());
        }

        [TestMethod]
        public void TestOneToOneOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IOneToOneGenericHardOverride<int>>() != null);

            Assert.IsTrue(sp.GetService<IOneToOneGenericHardOverride<int>>() != sp.GetService<IOneToOneGenericHardOverride<int>>());
        }
        [TestMethod]
        public void TestOneToMany()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToManyGenericHard<int>>()?.Count() == 2);

            Assert.IsTrue(sp.GetServices<IOneToManyGenericHard<int>>().First() == sp.GetServices<IOneToManyGenericHard<int>>().First());
            Assert.IsTrue(sp.GetServices<IOneToManyGenericHard<int>>().Skip(1).First() == sp.GetServices<IOneToManyGenericHard<int>>().Skip(1).First());
        }

        [TestMethod]
        public void TestOneToManyOverrride()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetServices<IOneToManyGenericHardOverride<int>>()?.Count() == 2);

            Assert.IsTrue(sp.GetServices<IOneToManyGenericHardOverride<int>>().Where(i => i is OneToManyOneGenericHardOverride<int>).First() != sp.GetServices<IOneToManyGenericHardOverride<int>>().Where(i => i is OneToManyOneGenericHardOverride<int>).First());
            Assert.IsTrue(sp.GetServices<IOneToManyGenericHardOverride<int>>().Where(i => i is OneToManyTwoGenericHardOverride<int>).First() == sp.GetServices<IOneToManyGenericHardOverride<int>>().Where(i => i is OneToManyTwoGenericHardOverride<int>).First());
        }


        [TestMethod]
        public void GenericHardMixed()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetService<IGenericHardMixed<string>>() != null);

            Assert.IsTrue(sp.GetService<IGenericHardMixed<string>>() != sp.GetService<IGenericHardMixed<string>>());

            Assert.IsTrue(sp.GetService<IGenericHardMixed<int>>() == sp.GetService<IGenericHardMixed<int>>());
        }
    }
}
