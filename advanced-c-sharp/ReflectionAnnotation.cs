using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace advanced_c_sharp
{

    public interface IGreeting
    {
        void SayHello();
    }

    public class Greeting : IGreeting
    {
        public void SayHello()
        {
            Console.WriteLine("Hello, Balreet!");
        }
    }

    public class LoggingProxy<T> : DispatchProxy
    {
        private T _realObject;

        public void SetTarget(T realObject)
        {
            _realObject = realObject;
        }

        protected override object Invoke(
            MethodInfo targetMethod,
            object[] args)
        {
            Console.WriteLine(
                $"Calling method: {targetMethod.Name}"
            );

            return targetMethod.Invoke(_realObject,args);
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
    }

    class ObjectMapper
    {
        public static T ToObject<T>(
            Type clazz,
            Dictionary<string, object> properties)
        {
            object obj = Activator.CreateInstance(clazz);

            foreach (PropertyInfo property in clazz.GetProperties())
            {
                if (properties.ContainsKey(property.Name))
                {
                    object value = properties[property.Name];

                    property.SetValue(obj, value);
                }
            }

            return (T)obj;
        }
    }


    internal class MathOperations
    {
        internal int AddFn(int a, int b)
        {
            return a + b;
        }
        internal int SubFn(int a, int b)
        {
            return a - b;
        }

        internal int MulFn(int a, int b)
        {
            return a * b;
        }
    }
    internal class ReflectionAnnotation
    {
        internal static void MathMethods()
        {

            MathOperations mops = new MathOperations();

            Console.Write("Enter operation (Add/Subtract/Multiply): ");
            string operation = Console.ReadLine();

            Console.Write("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Type type = mops.GetType();

            MethodInfo method = type.GetMethod(operation);

            if (method != null)
            {
                object result = method.Invoke(mops, new object[] { a, b });

                Console.WriteLine("Result: " + result);
            }
            else
            {
                Console.WriteLine("Method not found.");
            }
        }

        internal static void ObjMap()
        {
            Dictionary<string, object> properties =
                new Dictionary<string, object>
                {
                { "Name", "Balreet" },
                { "Age", 21 },
                { "Salary", 50000.0 }
                };

            Person person =
                ObjectMapper.ToObject<Person>(
                    typeof(Person),
                    properties
                );

            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.Salary);
        }

        static void GreetingMeth()
        {
            IGreeting greeting =
                DispatchProxy.Create<
                    IGreeting,
                    LoggingProxy<IGreeting>
                >();

            LoggingProxy<IGreeting> proxy =
                (LoggingProxy<IGreeting>)greeting;

            proxy.SetTarget(new Greeting());

            greeting.SayHello();
        }

    }
}
