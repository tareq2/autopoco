using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenUsingDefaultSession
    {
        // This one is for you Hanselman.. ;-)
        [Test]
        [Ignore("Not just yet though")]
        public void It_Just_Works()
        {
            var session = AutoPocoContainer.CreateDefaultSession();
            var user = session.Single<SimpleUser>().Get();

            Assert.NotNull(user, "User was not created");
            Assert.NotNull(user.FirstName, "User did not get first name");
        }
    }
}
