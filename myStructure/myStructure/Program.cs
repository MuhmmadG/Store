using myStructure.Servries;

namespace myStructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            MyList<string> list = new MyList<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.Add("4");
            list.Add("5");
            list.Add("6");
            list.Insert(2, "3");
            list.RemoveRenge(2,3);
            list.AddRenge(new string[] {"a",  "b", "c", "d", "e", "f"});
            list.Remove("Muhmmad1");
          var  index =  list.IndexOf("Ali");
            Console.WriteLine(index);
           
            //list = new MyList<string>(10);
            list[0] = "Muhmmad";
            list[1] = "gamal";
            list[2] = "Ahmed";
            List<string> list2 = list["0,1,3"];
            MyList<Person> list3 = new MyList<Person>(new PersonComparer());
            list3.Add(new Person() { Id = 1 });
            list3.Add(new Person() { Id = 2 });
            list3.Add(new Person() { Id = 3 });
            list3.Add(new Person() { Id = 4 });
            MyList<Person> list4 = new MyList<Person>(new PersonComparer());
            list4.Add(new Person() { Id = 1 });
            list4.Add(new Person() { Id = 2 });
            list4.Add(new Person() { Id = 3 });
            list4.Add(new Person() { Id = 4 });




        }
    }
}
