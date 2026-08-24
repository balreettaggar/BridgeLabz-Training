using NUnit.Framework;
using Week4Review;
using System;
using System.Collections.Generic;
using System.IO;

namespace Week4Review.Tests
{
    public class TextTestClass
    {
        [SetUp]
        public void SetUp()
        {
            Document.head = null;
        }

        [Test]
        public void AddCharacterTest()
        {
            Document document = new Document();

            document.AddCharacter('A');
            document.AddCharacter('B');
            document.AddCharacter('C');

            Assert.That(Document.head.data, Is.EqualTo('A'));
            Assert.That(Document.head.next.data, Is.EqualTo('B'));
            Assert.That(Document.head.next.next.data, Is.EqualTo('C'));
        }

        // 2. Test updating a character
        [Test]
        public void UpdateCharacterTest()
        {
            Document document = new Document();

            document.AddCharacter('A');
            document.AddCharacter('B');
            document.AddCharacter('C');

            document.UpdateDeleteChar('B', "update", 'X');

            Assert.That(Document.head.next.data, Is.EqualTo('X'));
        }

        // 3. Test deleting a character
        [Test]
        public void DeleteCharacterTest()
        {
            Document document = new Document();

            document.AddCharacter('A');
            document.AddCharacter('B');
            document.AddCharacter('C');

            document.UpdateDeleteChar('B', "delete", 'X');

            Assert.That(Document.head.data, Is.EqualTo('A'));
            Assert.That(Document.head.next.data, Is.EqualTo('C'));
        }

        // 4. Test invalid cursor direction
        [Test]
        public void CursorInvalidDirectionTest()
        {
            Document document = new Document();

            document.AddCharacter('A');
            document.AddCharacter('B');
            document.AddCharacter('C');

            bool result = false;

            try
            {
                Document.Cursor(2, "up", 10);
            }
            catch (Exception e)
            {
                if (e.Message == "Provide correct cursor movement")
                    result = true;
            }

            Assert.That(result, Is.True);
        }

        // 5. Test cursor out of range
        [Test]
        public void CursorOutOfRangeTest()
        {
            Document document = new Document();

            document.AddCharacter('A');
            document.AddCharacter('B');
            document.AddCharacter('C');

            bool result = false;

            try
            {
                Document.Cursor(10, "Right", 3);
            }
            catch (Exception e)
            {
                if (e.Message == "Input out of range") result = true;
            }
            Assert.That(result, Is.True);
        }

        // 6. Test recent files circular traversal
        [Test]
        public void RecentFileCycleTest()
        {
            RecentFile recentFile = new RecentFile();

            recentFile.AddFile("File1");
            recentFile.AddFile("File2");
            recentFile.AddFile("File3");

            Assert.That(recentFile.CtrlTab(), Is.EqualTo("File1"));
            Assert.That(recentFile.CtrlTab(), Is.EqualTo("File2"));
            Assert.That(recentFile.CtrlTab(), Is.EqualTo("File3"));
            Assert.That(recentFile.CtrlTab(), Is.EqualTo("File1"));
        }

        [Test]
        public void UndoRedoTest()
        {
            RedoUndo redoUndo = new RedoUndo();
            redoUndo.AddOperation('A');
            redoUndo.AddOperation('B');            
            redoUndo.Undo();
            redoUndo.Redo();
            Assert.Pass("Undo and Redo operations completed without error.");
        }

        [Test]
        public void WordFreqTest()
        {
            Document document = new Document();
            string input = "hello world hello";
            foreach (char ch in input)
            {
                document.AddCharacter(ch);
            }

            Dictionary<string, int> result = WordFreq.Frequency();
            Assert.That(result["hello"], Is.EqualTo(2));
            Assert.That(result["world"], Is.EqualTo(1));
        }

        [Test]
        public void BSnotFound()
        {
            List<int> frequencies = new List<int>{1, 2, 3, 4, 5};
            bool result = Sort.BinarySearch(frequencies, 0, frequencies.Count - 1, 10);
            Assert.That(result, Is.False);
        }
    }
}