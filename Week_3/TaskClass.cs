using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Week_3
{
    public class Task<T>
    {
        public string Id { get; set; }

        public T Type { get; set; }

        public int Priority { get; set; }

        public List<string> Depends_On { get; set; }

        public int ETA { get; set; }

        public Task(
            string id,
            T type,
            int priority,
            List<string> depends_on,
            int eta)
        {
            Id = id;
            Type = type;
            Priority = priority;
            Depends_On = depends_on;
            ETA = eta;
        }
    }

    public class TaskScheduler<T>
    {
 
        private Dictionary<string, Task<T>> allTasks = new Dictionary<string, Task<T>>();

        private HashSet<string> completedTasks = new HashSet<string>();
        public void AddTask(Task<T> task)
        {
            allTasks.Add(task.Id, task);
        }

        public void AddTask(string input)
        {
            Task<T> task = ParseTask(input);

            AddTask(task);
        }

        public void CompleteTask(string taskId)
        {
            completedTasks.Add(taskId);
        }

        private Task<T> ParseTask(string input)
        {
            string pattern =
                @"^TASK:(?<id>T\d+)" +
                @"\|TYPE:(?<type>[^|]+)" +
                @"\|PRIORITY:(?<priority>\d+)" +
                @"\|DEPENDS_ON:(?<dependencies>[^|]*)" +
                @"\|ETA:(?<eta>\d+)s$";


            Match match = Regex.Match(input, pattern);


            if (!match.Success)
            {
                throw new ArgumentException(
                    "Invalid task format."
                );
            }


            string id = match.Groups["id"].Value;


            string typeValue = match.Groups["type"].Value;


            int priority = int.Parse(match.Groups["priority"].Value);


            string dependencyValue = match.Groups["dependencies"].Value;

            List<string> dependencies;

            if (string.IsNullOrWhiteSpace(dependencyValue))
            {
                dependencies = new List<string>();
            }
            else
            {
                dependencies = dependencyValue.Split(',').ToList();
            }

            int eta =int.Parse(match.Groups["eta"].Value);

            T type =(T)Convert.ChangeType(typeValue,typeof(T));

            return new Task<T>(id,type,priority,dependencies,eta);
        }

        private bool IsTaskReady(Task<T> task)
        {
            if (completedTasks.Contains(task.Id))
            {
                return false;
            }

            foreach (string dependency in task.Depends_On)
            {
                if (!completedTasks.Contains(dependency))
                {
                    return false;
                }
            }
            return true;
        }

        public List<Task<T>> GetReadyTasks()
        {
            List<Task<T>> readyTasks = new List<Task<T>>();

            foreach (Task<T> task in allTasks.Values)
            {
                if (IsTaskReady(task))
                {
                    readyTasks.Add(task);
                }
            }

            return readyTasks;
        }

        public List<string> GetDanglingDependencies()
        {
            List<string> danglingDependencies =
                new List<string>();

            foreach (Task<T> task in allTasks.Values)
            {
                foreach (string dependency in task.Depends_On)
                {
                    if (!allTasks.ContainsKey(dependency))
                    {
                        if (!danglingDependencies.Contains(dependency))
                        {
                            danglingDependencies.Add(dependency);
                        }
                    }
                }
            }

            return danglingDependencies;
        }
    }
}

//Problem — Warehouse Robot Task Scheduler (Priority + Dependency Hybrid)

//A warehouse uses robots to execute operational tasks. Each task is received as a structured string, for example:
//TASK: T4471 | TYPE:pick | PRIORITY:3 | DEPENDS_ON:T4468, T4469|ETA:180s
//Some tasks cannot start until all of their dependencies are completed. Among the tasks that are ready to run, the task with the higher priority should be selected first.
//Build a generic task scheduling system that tracks dependencies and manages the execution order of ready tasks.
//Requirements
//Create a generic TaskScheduler<T> where T represents a task type.
//Maintain task information using appropriate collections to support:
//Fast task lookup by task ID.
//Dependency tracking.
//Tracking completed tasks.
//Managing tasks that are currently ready to execute.
//The scheduler must combine two different collection strategies:
//A dependency-tracking structure to determine whether a task is ready.
//A priority-ordering structure to determine which ready task should execute first.
//A task is considered ready only when all of its dependencies have been completed.
//Among all currently-ready tasks, tasks with a higher priority must appear before lower-priority tasks.
//Use Regex with named groups to parse:
//Task ID
//Task type
//Numeric priority
//Dependency list
//ETA in seconds
//DEPENDS_ON may contain:
//Multiple dependencies: T4468, T4469
//A single dependency: T4468
//No dependencies
//Choose and document one convention for a task with no dependencies. For example:
//DEPENDS_ON:
//Use LINQ to:
//Compute the complete list of currently-ready tasks.
//Return ready tasks in the correct priority order.
//Detect dependency references that point to task IDs that do not exist in the current batch.
//The dangling dependency check should be expressed using a lambda-based set difference, such as comparing referenced dependency IDs against the set of existing task IDs.
//Regex Considerations
//The DEPENDS_ON field can be completely empty:
//DEPENDS_ON:
//Your Regex must correctly handle this case without causing the entire task record to fail.
//Be deliberate about distinguishing between:
//A dependency field that is present but empty.
//A dependency field that contains one or more task IDs.
//The priority and ETA values should be parsed as numeric values rather than remaining strings.
//NUnit Focus
//Write NUnit tests covering at least the following cases:
//Task with no dependencies
//The task should be immediately ready.
//Task with multiple dependencies
//Only some dependencies are completed.
//The task should not be considered ready.
//Task whose dependencies are all completed
//The task should become ready.
//Dangling dependency
//A task references a dependency ID that does not exist in the batch.
//The scheduler should detect and report the invalid reference.
//Priority ordering
//Multiple tasks are simultaneously ready.
//Verify that higher-priority tasks appear before lower-priority tasks.
//Multiple ready tasks with different priorities
//Verify that the complete ready-task list is returned in the expected priority order.
//ETA parsing
//Verify that values such as 180s are correctly converted into numeric seconds.
//Expected Outcome
//The system should be able to:
//Parse task records using Regex named groups.
//Track task dependencies.
//Identify tasks that are currently ready.
//Detect invalid or dangling dependency references.
//Maintain correct priority ordering among ready tasks.
//Update task readiness when dependencies are marked as completed.
//Handle tasks with no dependencies correctly.
//Provide reliable scheduling results verified through NUnit tests.
