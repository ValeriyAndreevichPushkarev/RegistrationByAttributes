using Microsoft.Extensions.DependencyInjection;
using RegistrationByAttributes;
using test.Tests.Simple.TestOneToOne;

namespace Tests
{
    [TestClass]
    public sealed class SimpleTestsNamedDependencyInjections
    {
        [TestMethod]
        public void TestOneToMany()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetKeyedService<IOneToManyNamed>("One") is OneToManyNamedOne);
            Assert.IsTrue(sp.GetKeyedService<IOneToManyNamed>("Two") is OneToManyNamedTwo);
        }

        [TestMethod]
        public void TestOneToManyLifetime()
        {
            var app = new ServiceCollection();
            app.RegisterByAttributes();
            var sp = app.BuildServiceProvider();

            Assert.IsTrue(sp.GetKeyedService<IOneToManyNamed>("One") == sp.GetKeyedService<IOneToManyNamed>("One"));
            Assert.IsTrue(sp.GetKeyedService<IOneToManyNamed>("Two") != sp.GetKeyedService<IOneToManyNamed>("Two"));
        }
    }
}
