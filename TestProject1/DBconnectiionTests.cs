using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClassLibrary1;
namespace TestProject1
{
    internal class DBconnectiionTests
    {

        private DBconnection dbconnection;
        [SetUp]
        public void SetUp()
        {
            dbconnection = new DBconnection();
            dbconnection.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            dbconnection.Disconnect();
        }

        [Test]
        public void isConnected()
        {
            Assert.IsTrue(dbconnection.Connection);
        }

        [Test]
        public void isDisconnect()
        {
            Assert.IsNotNull(dbconnection.Connection);
        }
    }
}
