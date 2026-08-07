using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TestProject1
{
    internal class ListManagerTests
    {
        private ListManager listManager;

        [SetUp]
        public void SetUp()
        {
            listManager = new ListManager();
        }

        [Test]
        public void AddElementTest()
        {
            List<int> myList = [ 1, 2, 3, 4, 5 ];
            listManager.AddElement(myList, 6);
            Assert.Contains(6, myList);
            //Console.WriteLine(myList);
            for(int i=0; i<myList.Count; i++)
            {
                Console.WriteLine(myList[i]+" ");
            }
        }

        [Test]
        public void RemoveElementTest()
        {
            List<int> myList = [1, 2, 3, 4, 5];
            listManager.RemoveElement(myList, 3);
            Assert.IsFalse(myList.Contains(3));
            //Console.WriteLine(myList);
            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i] + " ");
            }
        }

        [Test]
        public void GetSizeTest()
        {
            List<int> myList = [1, 2, 3, 4, 5];
            int size = listManager.GetSize(myList);
            Assert.AreEqual(5, size);
            Console.WriteLine(myList.Count);
        }
    }
}
