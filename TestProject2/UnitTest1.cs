using NUnit.Framework;
using System.Collections.Generic;
using Week_3;

using TaskClass = Week_3.Task<string>;

namespace TestProject2
{
    public class TaskSchedulerTests
    {
        private TaskScheduler<string> scheduler;


        [SetUp]
        public void Setup()
        {
            scheduler = new TaskScheduler<string>();
        }


        [Test]
        public void NoDepend()
        {
            scheduler.AddTask(
                "TASK:T1|TYPE:pick|PRIORITY:5|DEPENDS_ON:|ETA:100s"
            );

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks.Count, Is.EqualTo(1));
            Assert.That(readyTasks[0].Id, Is.EqualTo("T1"));
        }


        [Test]
        public void taskDepRed()
        {
            scheduler.AddTask(
                "TASK:T1|TYPE:pick|PRIORITY:5|DEPENDS_ON:|ETA:100s"
            );

            scheduler.AddTask(
                "TASK:T2|TYPE:pack|PRIORITY:8|DEPENDS_ON:T1|ETA:200s"
            );

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks.Count, Is.EqualTo(1));
            Assert.That(readyTasks[0].Id, Is.EqualTo("T1"));
        }


        [Test]
        public void DepComp()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:5|DEPENDS_ON:|ETA:100s");

            scheduler.AddTask("TASK:T2|TYPE:pack|PRIORITY:8|DEPENDS_ON:T1|ETA:200s");

            scheduler.CompleteTask("T1");

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();
            Assert.That(readyTasks.Count, Is.EqualTo(1));
            Assert.That(readyTasks[0].Id, Is.EqualTo("T2"));
        }


       
        [Test]
        public void TaskDep()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:2|DEPENDS_ON:|ETA:100s");

            scheduler.AddTask("TASK:T2|TYPE:pick|PRIORITY:3|DEPENDS_ON:|ETA:100s");

            scheduler.AddTask("TASK:T3|TYPE:pack|PRIORITY:8|DEPENDS_ON:T1,T2|ETA:200s");


            scheduler.CompleteTask("T1");

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks.Count, Is.EqualTo(1));
            Assert.That(readyTasks[0].Id, Is.EqualTo("T2"));

            scheduler.CompleteTask("T2");

            readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks.Count, Is.EqualTo(1));
            Assert.That(readyTasks[0].Id, Is.EqualTo("T3"));
        }

        [Test]
        public void ReadyTaskPriority()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:2|DEPENDS_ON:|ETA:100s");

            scheduler.AddTask("TASK:T2|TYPE:pack|PRIORITY:8|DEPENDS_ON:|ETA:100s");

            scheduler.AddTask("TASK:T3|TYPE:load|PRIORITY:5|DEPENDS_ON:|ETA:100s");


            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks[0].Id, Is.EqualTo("T2"));

            Assert.That(readyTasks[1].Id, Is.EqualTo("T3"));

            Assert.That(readyTasks[2].Id, Is.EqualTo("T1"));
        }


        [Test]
        public void isDependency()
        {
            scheduler.AddTask("TASK:T1|TYPE:pack|PRIORITY:5|DEPENDS_ON:T999|ETA:100s");

            List<string> dangling =scheduler.GetDanglingDependencies();

            Assert.That(dangling.Contains("T999"), Is.True);
        }

        [Test]
        public void ETAparsing()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:5|DEPENDS_ON:|ETA:180s");

            List<TaskClass> readyTasks =scheduler.GetReadyTasks();

            Assert.That(readyTasks[0].ETA, Is.EqualTo(180));
        }


        
        [Test]
        public void PriorityScheduling()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:7|DEPENDS_ON:|ETA:100s");

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks[0].Priority, Is.EqualTo(7));
        }

        [Test]
        public void DanglingDependency()
        {
            scheduler.AddTask("TASK:T1|TYPE:pick|PRIORITY:5|DEPENDS_ON:|ETA:100s");

            scheduler.CompleteTask("T1");

            List<TaskClass> readyTasks = scheduler.GetReadyTasks();

            Assert.That(readyTasks.Count, Is.EqualTo(0));
        }
    }
}