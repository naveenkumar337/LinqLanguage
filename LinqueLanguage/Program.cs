using System;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace LinqueLanguage
{
    class Program
    {
        delegate bool isnum(student s);
        static void Main(string[] args)
        {
            String[] data = { "naveen", "ajay", "lokesh" };
            var name = from dat in data
                       where dat.Contains('a')
                       select dat;
            var linqname = data.Where(s => s.Contains('a'));
            Console.WriteLine(string.Join(" ", name));
            Console.WriteLine(string.Join(" ", linqname));

            List<student> studentarray = new List<student> {
                new student { id=10,name="naveen",des="developer",salary=12500,Branch="CSE"},
                new student { id=20,name="hanumanth rao",des="developer",salary=12500,Branch="MECH"},
                new student { id=30,name="krishna",des="Senior Developer",salary=50000,Branch="CIVIL"},
                new student { id=45,name="abhishek",des="UI",salary=50000,Branch="CSE"},
                new student { id=45,name="abhishek",des="UI",salary=50000,Branch="CSE"}
            };

            List<teacher> teacherarray = new List<teacher> {
                new teacher { id=10,Name="Ramesh",Branch="CSE"},
                new teacher { id=20,Name="Priyanka",Branch="MECH"},
                new teacher { id=30,Name="MAX",Branch="CIVIL"}
            };
            //working with List
            var nameswise = (from d in studentarray
                             where d.salary > 12500 && d.salary <= 50000
                             select new { d.name, d.salary, d.id }).ToList();
            Console.WriteLine(string.Join("\n", nameswise));//.(s=>s.name + " "+s.salary)));

            var data2 = studentarray.ToList();
            var data1 = data2.Where(d => d.salary > 12500 && d.salary <= 50000).Select(s => new { s.name, s.salary });

            Console.WriteLine(string.Join(" ", data1.Select(s => s.name)));

            //using delegate
            Console.WriteLine("\n\t\t\t***using delegate***");
            isnum number = (s) =>
             {
                 int y = 10;
                 Console.WriteLine("welcome");
                 return s.salary > y;
             };
            Console.WriteLine(number(studentarray[0]));


            //ofType
            //Console.WriteLine("\n\t\t\t***using OFTYPE***");
            //IList dataset = new ArrayList { 1, "naveen", 2, "res" };
            //var oftyperes = from dt in dataset.OfType<string>() select dt;
            //Console.WriteLine(string.Join(" ", oftyperes));

            //Getting Multiple Elemenents at a time
            Console.WriteLine("\n==>Getting Multiple Elemenents at a time");
            var names = studentarray.Where(s => s.salary > 12500 && s.salary <= 50000).Select(s => new { s.name, s.salary }).FirstOrDefault();
            Console.WriteLine(string.Join("\n", names.name + " " + names.salary));//.(s=>s.name + " "+s.salary)));

            //OrderBy
            Console.WriteLine("\n\t\t\t***Using OrderBy***");

            var orderbyres = from student in studentarray where student.salary > 1000 orderby student.name ascending select student;
            Console.WriteLine(string.Join("\n", orderbyres.Select(d => new { d.name, d.salary })));
            //OrderBy in Method Syntax
            var regorderbyres = studentarray.Where(s => s.salary > 1000).OrderBy(h => h.name).Select(d => d.name);
            Console.WriteLine(string.Join("\n", regorderbyres));
            Console.WriteLine("\n");
            var regorderbyresdesc = studentarray.Where(s => s.salary > 1000).OrderByDescending(h => h.name).Select(d => d.name);
            Console.WriteLine(string.Join("\n", regorderbyresdesc));

            //ThenBy (it is secondary sorting means EX:tow class have same name but we need less age person first then we are using thenby sorting.)
            Console.WriteLine("\n\t\t\t***Using THENBY***");
            var thenbyres = from student in studentarray
                            where student.salary > 1000
                            orderby student.name, student.salary
                            select student;
            Console.WriteLine("====using tow statements in a orderby line");
            Console.WriteLine(string.Join("\n", thenbyres.Select(d => new { d.name, d.salary })));
            Console.WriteLine("====using reg and thenby");
            var regthenbyres = studentarray.OrderBy(s => s.name).ThenBy(d => d.salary).Where(s => s.salary > 20000);
            Console.WriteLine(string.Join("\n", regthenbyres.Select(d => new { d.name, d.salary })));
            /*
            ThenBy==>worked on only method syntax(using regular exp).
            Multiple fileds not allowed in Methodsyntax when using orderby with , saparator used in query based syntaxes.   
             */

            //GroupBy

            Console.WriteLine("\n\t\t\t***Using GroupBY***");
            var groupbyres = from student in studentarray
                             group student by student.name;
            Console.WriteLine("===Using ForEach Loop");
            foreach (var k in groupbyres)
            {
                Console.WriteLine(string.Join(" \n", k.Key));
                foreach (var value in k)
                {
                    Console.WriteLine(string.Join(" ", value.name + " " + value.salary));
                }
            }
            Console.WriteLine("==>Using Linq NestedQuery Loop" + string.Join(" ", groupbyres.Select(s => "\n" + s.Key + string.Join(" ", s.Select(d => "\n" + d.name + " " + d.salary)))));// s.Key.name + " " + s.Key.salary)));

            //Multiple Creteria
            Console.WriteLine("===Multiple Creteria");
            var groupbyres1 = from student in studentarray
                              group student by new { student.name, student.salary };
            Console.WriteLine("===Using ForEach Loop");
            foreach (var k in groupbyres1)
            {
                Console.WriteLine(string.Join(" \n", k.Key.name));
                foreach (var value in k)
                {
                    Console.WriteLine(string.Join(" ", value.name + " " + value.salary));
                }
            }
            Console.WriteLine("==>Using Linq NestedQuery Loop" + string.Join(" ", groupbyres1.Select(s => "\n" + s.Key.name + string.Join(" ", s.Select(d => "\n" + d.name + " " + d.salary)))));// s.Key.name + " " + s.Key.salary)));


            Console.WriteLine("===Using Reg with Multiple Creteria");
            var reggroupbyres = studentarray.GroupBy(s => new { s.name, s.salary });
            Console.WriteLine("===Using ForEach Loop");
            foreach (var k in reggroupbyres)
            {
                Console.WriteLine(string.Join(" \n", k.Key.name));
                foreach (var value in k)
                {
                    Console.WriteLine(string.Join(" ", value.name + " " + value.salary));
                }
            }
            Console.WriteLine("==>Using Linq NestedQuery Loop" + string.Join(" ", reggroupbyres.Select(s => "\n" + s.Key.name + string.Join(" ", s.Select(d => "\n" + d.name + " " + d.salary)))));// s.Key.name + " " + s.Key.salary)));
            //ToLookup
            Console.WriteLine("\n\t\t\t***Using ToLookup***");
            /*ToLookup is the same as GroupBy; the only difference is GroupBy execution is deferred, whereas ToLookup execution is immediate.
             * Also, ToLookup is only applicable in Method syntax. ToLookup is not supported in the query syntax.
             */
            var tookupres = studentarray.ToLookup(s => new { s.salary, s.name });
            Console.WriteLine("===Using ForEach Loop");
            foreach (var k in tookupres)
            {
                Console.WriteLine(string.Join(" \n", k.Key.name));
                foreach (var value in k)
                {
                    Console.WriteLine(string.Join(" ", value.name + " " + value.salary));
                }
            }

            Console.WriteLine("==>Using Linq NestedQuery Loop:" + string.Join(" ", tookupres.Select(s => "\n" + s.Key.name + string.Join(" ", s.Select(d=> "\n" + d.name+" "+d.salary)))));// s.Key.name + " " + s.Key.salary)));
            //JOIN

            Console.WriteLine("\n\t\t\t***Using JOIN***");

            /*
             * from... in outerSequence
                join... in innerSequence 
                on  outerKey equals innerKey
                select ...

            public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, 
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, 
            Func<TInner, TKey> innerKeySelector, 
            Func<TOuter, TInner, TResult> resultSelector);
            
             Ex:- Outer.join(inner,
             outer=>outer.key ==>Condition,
             inner=>inner.key ==>Condition,
             (oter,inner)=>new{selectors(outer.key,inner.key)} ==>result Set
             )
             */
            Console.WriteLine("===using Query based condition along with where condition");
            var joinres = from s in studentarray
                          join r in teacherarray
                          on s.id equals r.id
                          where s.salary > 20000
                          select new { k = s.name, r = r.Name };

            Console.WriteLine(string.Join("\n", joinres.Select(s => s.k + "===>" + s.r)));

            Console.WriteLine("===using Method based");
            var regjoinres = studentarray.Where(s => s.Branch == "CSE").Join(teacherarray, student => student.id, teacher => teacher.id, (student, teacher) => new { k = student.name, r = teacher.Name });

            Console.WriteLine(string.Join("\n", regjoinres.Select(s => s.k + "===>" + s.r)));


            //GroupJOIN

            Console.WriteLine("\n\t\t\t***USING GROUPJOIN***");
            var reggropujoinres = teacherarray.GroupJoin(studentarray, tea => tea.Branch, std => std.Branch, (tea, stdgroup) => new { tea = tea.Branch, Student = stdgroup });

            foreach (var groupkey in reggropujoinres)
            {
                Console.WriteLine(groupkey.tea);
                foreach (var value in groupkey.Student)
                {
                    Console.WriteLine(value.name + "==>" + value.salary);
                }
            }
            Console.WriteLine("===Query Based:");
            var groupjoinres = from teach in teacherarray

                               join std in studentarray
                               on teach.Branch equals std.Branch
                               into StudentGroup
                               select new { Branch = teach.Branch, Students = StudentGroup };
            foreach (var joingroup in groupjoinres)
            {

                Console.WriteLine(joingroup.Branch);
                foreach (var stdentinfo in joingroup.Students)
                {
                    Console.WriteLine(stdentinfo.name + "==>" + stdentinfo.salary);
                }
            }

            Console.WriteLine("\t\t\t***ALL and ANY***");
            var allres = studentarray.All(s => s.salary < 1200000 && s.salary > 100000);
            Console.WriteLine(string.Join("\n", "ALL==>Above 100000 and below 1200000 is contain:" + allres.ToString()));
            var anyres = studentarray.Any(s => s.salary > 1000 && s.salary < 1200000);
            Console.WriteLine(string.Join("\n", "ANY==>Above 1000 and below 1200000 is contain:" + anyres.ToString()));

            Console.WriteLine("\t\t\t***USING CONTAINS WITH EQUALITY COMPARER CLASS***");
            student stdstudent = new student() { id = 10, name = "naveen", Branch = "CSE" };
            Console.WriteLine(studentarray.Contains(stdstudent, new IScompareTrue()));

            Console.WriteLine("\t\t\t***USING AGGRIGATE alias CONTATINATION***");
            IList<string> list = new List<string>() { "A", "B", "C", "D", "E", "F", "G" };
            var aggrigateres = list.Aggregate((s1, s2) => s1 + "," + s2);
            Console.WriteLine(string.Join(" ", aggrigateres));
            Console.WriteLine("===>Using class Values");
            var Aggrigatesrudent = studentarray.Aggregate<student, string>("Student Names: ", (std, s) => std = std + s.name + ",");
            Console.WriteLine(string.Join("", Aggrigatesrudent));
            Console.WriteLine("===>Removing Last Letter:");
            var aggrigratestudent = studentarray.Where(s => s.Branch == "CSE").Aggregate(string.Empty, (std, s) => std += s.name + ",", std => std.Substring(0, std.Length - 1));
            Console.WriteLine(string.Join("", aggrigratestudent));

            Console.WriteLine("\t\t\t***USING MAX***");
            //This Was done by using IComparaBle check class(teacher) once...
            var maxres = teacherarray.Max();
            Console.WriteLine(string.Join("\n", maxres.Name));
            var maxresevensalary = teacherarray.Max(i =>
            {
                if (i.id % 2 == 0)
                    return i;
                return null;
            });
            Console.WriteLine(string.Join("\n", "Even Numbered Teacher NAME:" + maxresevensalary.Name.ToUpper() + " Worked on fllowing Branch:" + maxresevensalary.Branch));

            Console.WriteLine("\n\t\t\t***Using ElementAt && ElementAtOrDefault***");
            IList<int> intlist = new List<int>() { 10, 12, 14, 15, 65, 42, 20 };
            IList<string> stringlist = new List<string>() {"one", "two", "four", "five", "six", "seven" };
            IList<int> emptylist = new List<int>();
            Console.WriteLine("0th Element :" + intlist.ElementAt(0));
            Console.WriteLine("0th Element :" + stringlist.ElementAt(0));
            Console.WriteLine("First Element :" + intlist.ElementAt(1));
            Console.WriteLine("First Element: " + stringlist.ElementAt(1));
            Console.WriteLine("Default 0 otherwise print value :" + intlist.ElementAtOrDefault(2));
            Console.WriteLine("Default null otherwise print value :" + stringlist.ElementAtOrDefault(2));

            Console.WriteLine("\n\t\t\t***Using First & FirstOrDefault***");

            Console.WriteLine("0th Element :" + intlist.First());

            Console.WriteLine("0th Element :" + stringlist.First());
            Console.WriteLine("0th Element checking even or not :" + intlist.First(i => i % 2 == 0));
            Console.WriteLine("Default index out of range otherwise print value :" + intlist.FirstOrDefault());
            Console.WriteLine("Default null otherwise print value :" + stringlist.FirstOrDefault());
            //Console.WriteLine("IF it is Empty List it's get error:" + emptylist.First().ToString());
            Console.WriteLine("1st Element in emptyList FirstOrDefault: {0}", emptylist.FirstOrDefault());

            Console.WriteLine("\n\t\t\t***Using  Last & LastOrDefault***");
            Console.WriteLine("Last Element " + intlist.Last());
            Console.WriteLine("Last Element " + stringlist.Last());
            Console.WriteLine("Last Element or default " + intlist.LastOrDefault());
            Console.WriteLine("Last Element or defalt " + stringlist.LastOrDefault());
            Console.WriteLine("Last Element with even num " + intlist.LastOrDefault(i => i % 2 == 0));
            //Console.WriteLine("Last Element with contains {0}", stringlist.LastOrDefault(k=>k.Contains("s")));

            Console.WriteLine("\n\t\t\t***Using  Single & SingleOrDefault***");
            /*
             * List Must have single element otherwise its thrown exception.
             * if list is empty its given default value 0 or null when we use SingleOrDefault()
             */
            Console.WriteLine("Single Element " + intlist.SingleOrDefault(i => i == 10));
            Console.WriteLine("\n\t\t\t***Using  SequenceEqual***");
            /*
             *  The SequenceEqual method checks whether the number of elements, value of each element and order of elements in two collections are equal or not.
             *  Order Must Be Same.
             */
            IList<int> intlist1 = new List<int>() { 10, 12, 14, 15, 10, 42, 20 };
            Console.WriteLine(intlist.SequenceEqual(intlist1));

            Console.WriteLine("\n\t\t\t***Using  DefaultIfEmpty***");
            var isdefaultarray = new List<string>();
            var Newlist1 = isdefaultarray.DefaultIfEmpty();
            var Newlist2 = isdefaultarray.DefaultIfEmpty("NONE");

            Console.WriteLine("Defalt Value Count for Newlist1 :" + Newlist1.Count());
            Console.WriteLine("Defalt Value for Newlist1 at index 0 :" + Newlist1.ElementAt(0));

            Console.WriteLine("Defalt Value Count Newlist2 :" + Newlist2.Count());
            Console.WriteLine("Defalt Value for Newlist1 at index 0 :" + Newlist2.ElementAt(0));

            Console.WriteLine("\n\t\t\t***Using  Repeat***");
            var repeatlist = Enumerable.Repeat<int>(10, 10);
            Console.WriteLine("Repeat Count is {0}", repeatlist.Count() + "\nElements are :" + string.Join(" ", repeatlist));

            //First Year Students
            List<BtechStudents> firstyearstudents = new List<BtechStudents>()
            {
                new BtechStudents { ID=1,Name="Ajay",Branch="MECH",Year=1},
                new BtechStudents { ID=2,Name="Kumar",Branch="ECE",Year=1},
                new BtechStudents { ID=3,Name="NavaDheep",Branch="EEE",Year=1},
            };
            //Second Year Students
            List<BtechStudents> secondyearstudents = new List<BtechStudents>()
            {
                new BtechStudents { ID=1,Name="Ajay",Branch="MECH",Year=2},
                new BtechStudents { ID=2,Name="Kumar",Branch="ECE",Year=2},
                new BtechStudents { ID=3,Name="naveen",Branch="CSE",Year=2},
            };
            var FullRecords = firstyearstudents.Concat(secondyearstudents);
            // Console.WriteLine("Count IS:"+ FullRecords.Count() + "\nFrom Fist and Second Year Student are:\n"+string.Join("\n",FullRecords.Select(s=>"ID:"+s.ID +" Name:"+s.Name)));
            //Console.WriteLine("Coolection is:"); FullRecords.ToList().ForEach(s => Console.WriteLine($"ID:{s.ID} Name: {s.Name}"));
            Console.WriteLine("\n\t\t\t***Using  Distinct***");
            /*
             * The Distinct operator is Not Supported in C# Query syntax. However, you can use Distinct method of query variable or wrap whole query into brackets and then call Distinct().
             */
            var distinctres = studentarray.Distinct(new ISDistinctcompareTrue());
            Console.WriteLine("Int Distinct Collection :" + string.Join(" ", intlist1.Distinct()));
            Console.WriteLine(string.Join("\n", distinctres.Select(s => s.name + "==>" + s.Branch + "==>" + s.id + "==>" + s.des + "==>" + s.salary)));

            Console.WriteLine("\nExample: Select the Distinct ID,Name from FirstYear and Second Year data..");
            Console.WriteLine("\nFrom Fist and Second Year Student are:\n" + string.Join("\n", FullRecords.Select(s => "ID:" + s.ID + " Name:" + s.Name + " ==> " + s.Year + " year")));
            var distinctcollections = FullRecords.Distinct(new IssameasSecondyear());
            Console.WriteLine("Distinct Collections are:\n" + string.Join("\n", distinctcollections.Select(s => "ID: " + s.ID + " Name: " + s.Name + " Year: " + s.Year)));

            Console.WriteLine("\n\t\t\t***Using  Except***");
            /*
             * The Except operator is Not Supported in C# & VB.Net Query syntax. However, you can use Distinct method on query variable or wrap whole query into brackets and then call Except().
             */
            IList<student> ExceptCollectionOfstudent = new List<student>()
            {
                new student { id=10,name="naveen",des="developer",Branch="CSE",salary=12500},
                new student { id=45 ,name="abhishek",des="UI",Branch="CSE",salary=50000},
                new student { id=100 ,name="ajay",des="UI",Branch="MCA",salary=50000}
            };
            var exceptres = studentarray.Except(ExceptCollectionOfstudent, new ISDistinctcompareTrue());
            Console.WriteLine("Collection of Ecept Method\n" + string.Join(" \n", exceptres.Select(s => s.name + " " + s.id)));


            Console.WriteLine("\nExample: Select ID,Name from FirstYear and Second Year data Except Failure Candidates of First Year..");
            List<BtechStudents> FailureCandidates = new List<BtechStudents>()
            {
                  new BtechStudents { ID=3,Name="NavaDheep",Branch="EEE",Year=1}
            };

            var ExceptcollectionsofStudents = FullRecords.Except(FailureCandidates, new IssameasSecondyear());
            Console.WriteLine("Except Collections are :\n" + string.Join("\n", ExceptcollectionsofStudents.Select(s => "ID: " + s.ID + " Name:" + s.Name + " " + s.Year + " Year")));
            Console.WriteLine("\n\t\t\t***Using  Intersect***");
            /*
             *It returns a new collection that includes common elements that exists in both the collection. Consider the following example. 
             * The Intersect operator is Not Supported in C# & VB.Net Query syntax. However, you can use the Intersect method on a query variable or wrap whole query into brackets and then call Intersect().
             */
            var intersectRes = studentarray.Intersect(ExceptCollectionOfstudent, new ISDistinctcompareTrue());
            Console.WriteLine("Collection of Intersect Method\n" + string.Join(" \n", intersectRes.Select(s => s.name + " " + s.id)));

            Console.WriteLine("\nExample: Select ID,Name from FirstYear and Second Year data Who are same..");
            var studentintersectresult = firstyearstudents.Intersect(secondyearstudents, new IssameasSecondyear());

            Console.WriteLine("\nCollection of Student Intersect Records:\n" + string.Join(" \n", studentintersectresult.Select(s => "ID:" + s.ID + " Name: " + s.Name)));
            Console.WriteLine("\n\t\t\t***Using  Union***");
            /*
             *The Union extension method requires two collections and returns a new collection that includes distinct elements from both the collections. 
             *The Union operator is Not Supported in C# & VB.Net Query syntax. However, you can use Union method on query variable or wrap whole query into brackets and then call Union().
            */
            var unionres = studentarray.Union(ExceptCollectionOfstudent, new ISDistinctcompareTrue());
            Console.WriteLine("Collection of UNION Method\n" + string.Join(" \n", unionres.Select(s => s.name + " " + s.id)));
            /*
             * Example: If the first year B'tech students was goe's to Secondyear classes,But the New peoples was Joined into same class. Find the count and list of students in SecondYearClass.
             * For FirstYear And Second Year maintain saperate Data...
             */

            Console.WriteLine("\nExample: If the first year B'tech students was goe's to Secondyear classes,But the New peoples was Joined into same class. Find the count and list of students in FirstYear and SecondYear Classes Without repeating." +
                "For FirstYear And Second Year maintain saperate Data...\n");
            var Result = firstyearstudents.Union(secondyearstudents, new IssameasSecondyear());
            Console.WriteLine($"Collection List Count: {Result.Count()} \nCollection List is\n" + string.Join("\n", Result.Select(S => "ID:" + S.ID + " Name:" + S.Name)));

            Console.WriteLine("\n\t\t\t***Using  Skip & SkipWhile***");
            /*
             * Skip:	Skips elements up to a specified position starting from the first element in a sequence.
             * SkipWhile:	Skips elements based on a condition until an element does not satisfy the condition. If the first element itself doesn't satisfy the condition, it then skips 0 elements and returns all the elements in the sequence.
             * SkipWhile:- skips the elements upto condition is satisfied after satisfied once it won't work it can display the remaining elements..
            */
            Console.WriteLine("Skips elements up to a specified position starting from the first element in a sequence.\nAfter Skip upto the 2nd position the collections are:  " + string.Join(" ", intlist.Skip(2).Select(s => s)));
            Console.WriteLine("SkipWhile:\nSkip while Collection with max lenth 3 are :\n" + string.Join("\n", stringlist.SkipWhile(s => s.Length < 4)));

            Console.WriteLine("\n\t\t\t***Using  Take & TakeWhile***");
            /*
             * Take:	Takes elements up to a specified position starting from the first element in a sequence.
             * TakeWhile:	Returns elements from the first element until an element does not satisfy the condition. If the first element itself doesn't satisfy the condition then returns an empty collection.
             */
            Console.WriteLine("Take elements up to a specified position starting from the first element in a sequence.\nAfter Take upto the 2nd position the collections are:  " + string.Join(" ", intlist.Take(2).Select(s => s)));
            Console.WriteLine("TakeWhile:\nTake while Collection with max Size 3 are :\n" + string.Join("\n", stringlist.TakeWhile(s => s.Length < 4)));


            Console.WriteLine("\n\t\t\t***Using AsEnumerable & AsQueryable Conversion***");

            var asenumarableres = studentarray.Select(s => s).Where(s => s.salary > 12500).AsEnumerable();
            Console.WriteLine(string.Join(" ", asenumarableres.Select(s=>s.name)));
            Console.WriteLine(string.Join(" ", from nameunder1 in asenumarableres select nameunder1.name));
            var asenumarableresqury = studentarray.Select(s => s).Where(s => s.salary > 12500).AsQueryable();
            Console.WriteLine(string.Join(" ", from nameunder1 in asenumarableresqury select nameunder1.name));

            Console.WriteLine("\n\t\t\t***Using Expression***");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Expression<Func<student, bool>> isstudent = s => s.salary > 10000 && s.salary < 50000 && s.Branch=="Cse";
            Func<student, bool> isreallyeligible = isstudent.Compile();
            var result = isreallyeligible(new student { id = 20, name = "naveen", des = "gdsjb", Branch = "Cse", salary = 20000 });
            watch.Stop();
            Console.WriteLine(result + "  ==>" + watch.ElapsedMilliseconds);
            watch.Restart();
            Func<student,bool> isfunc = s => s.salary > 10000 && s.salary < 50000 && s.Branch == "Cse";
            var resultfunc = isfunc(new student { id = 20, name = "naveen", des = "gdsjb", Branch = "Cse", salary = 10000 });
            watch.Stop();
            Console.WriteLine(resultfunc+"  ==>"+watch.ElapsedMilliseconds);
            Console.ReadKey();

        }
    }
    class student
    {
        public int id;
        public string name;
        public string des;
        public int salary;
        public string Branch;
    }
    class teacher : IComparable<teacher>
    {
        public string Name;
        public int id;
        public string Branch;
        public int CompareTo(teacher other)
        {
            if (this.Name.Length >= other.Name.Length)
                return 1;
            return 0;

        }
    }
    class IScompareTrue : IEqualityComparer<student>
    {
        public bool Equals(student x, student y)
        {
            if (x.Branch.ToLower() == y.Branch.ToLower() && x.id == y.id && x.name.ToLower() == y.name.ToLower())
                return true;
            return false;
        }
        public int GetHashCode(student obj)
        {

            return obj.GetHashCode();
        }
    }
    class ISDistinctcompareTrue : IEqualityComparer<student>
    {
        public bool Equals(student x, student y)
        {
            if (x.Branch.ToLower() == y.Branch.ToLower() && x.id == y.id && x.name.ToLower() == y.name.ToLower())
                return true;
            return false;
        }
        public int GetHashCode(student obj)
        {

            return obj.id.GetHashCode();
        }
    }
    class BtechStudents
    {
        public int ID;
        public string Name;
        public String Branch;
        public int Year;
    }
    class IssameasSecondyear : IEqualityComparer<BtechStudents>
    {
        public bool Equals(BtechStudents firstyear, BtechStudents secondyear)
        {
            if (firstyear.ID == secondyear.ID && firstyear.Name.ToLower() == secondyear.Name.ToLower() && firstyear.Branch.ToLower() == secondyear.Branch.ToLower())
                return true;
            return false;
        }
        public int GetHashCode(BtechStudents obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
