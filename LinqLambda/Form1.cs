using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLambda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string SurName { get; set; }
            public int Id { get; set; }
            public int ManagerId { get; set; }


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Person p1 = new Person();
            p1.FirstName = "Ali";
            p1.SurName = "Tan";
            p1.Id = 1;
            p1.ManagerId = 1;


            Person p2 = new Person();
            p2.FirstName = "Muhammet";
            p2.SurName = "Bel";
            p2.Id = 2;
            p2.ManagerId = 2;

            Person p3 = new Person();
            p3.FirstName = "Murat";
            p3.SurName = "Ekşi";
            p3.Id = 3;
            p3.ManagerId = 1;

            Person p4 = new Person();
            p4.FirstName = "Seçil";
            p4.SurName = "Topak";
            p4.Id = 4;
            p4.ManagerId = 4;

            Person p5 = new Person();
            p5.FirstName = "Seçil";
            p5.SurName = "Topak";
            p5.Id = 4;
            p5.ManagerId = 4;


            List<Person> pList = new List<Person>();
            pList.Add(p1);
            pList.Add(p2);
            pList.Add(p3);
            pList.Add(p4);
            pList.Add(p5);

            List<Person> pList2 = new List<Person>();
            pList2.Add(p1);
            pList2.Add(p3);
            pList2.Add(p2);


            //tek bir sonuç döndürür
            //aranacak kaydın eşleşen bir kaydının olması gerekiyor.
            //Person single = pList.Single(x => x.Id == 1);

            //tek bir kayıt döndürür. Eğer eşleşen kayıt varsa eşleşen kaydı döndürür. Eşleşen kayıt yoksa null döndürür.
            Person singleOrDefault = pList.SingleOrDefault(x => x.Id == 11);

            //aranacak kaydın içerisinde eşleşen ilk kaydı bulmak için kullanılır.
            //First birden fazla eşleşen kayıt var ve sadece ilki alınması isteniyor ise tercih edilir.
            // Person first = pList.First(x => x.ManagerId == 1);
            //birden fazla eşleşen kayıt var ise single hata verir.
            //Person singleManagerId = pList.Single(x => x.ManagerId == 1);

            //eşleşen bir kayıt yoksa patlar. bu durumda firstordefault kullanılır.
            //Person fistPatlak = pList.First(x => x.ManagerId == 4);

            //Bulursa eşleşen kayıtlardan ilkimi bulamaz ise null döndürür.
            Person firstPatlamayan = pList.FirstOrDefault(x => x.ManagerId == 4);

            //eşleşen kayıtlardan sonuncusunu getirir.(Kayıt eşleşmiyosa patlar)
            //Person last = pList.Last(x => x.ManagerId == 88);
            //eşleşen kayıt yoksa null
            Person lastOrDefault = pList.LastOrDefault(x => x.ManagerId == 1);


            //eşleşen kayıt yoksa indexoutofrange exception
            //Person elementAt = pList.ElementAt(3); //Murat
            //defaultlar hepsi null
            Person elementDef = pList.ElementAtOrDefault(3);

            //listenin içerisinde ilgi objeyi item olarak arar bulamaz ise -1 bulursa bulunduğu indeksi verir.
            int a = pList.LastIndexOf(p3, 2);

            //iki listenin itemlerinin aynı sırada olup olmadığını kontrol eder.
            bool sequence = pList.SequenceEqual(pList2);

            //ElementAt
            //First
            //Single
            //SingleOrDefault
            //Last
            //SequenceEqual
            //FirstOrDefault
            //LastOrDefault

            var firstTwoItem = pList2.Take(2);

            //veri sayfalama mantıkları bu ikisi ile yazılıyor.
            var firstTwoItemafterOneItem = pList2.Skip(1).Take(2);


            var ekadarAl = pList.TakeWhile(x => x.ManagerId < 2);

            //SkipWhile();
            //TakeWhile();

            //kurala uyduğu sürece listedekileri atlar, uymadığı andan itibaren hepsi getirir.
            var ekadarAtlat = pList.SkipWhile(x => x.ManagerId == 2);

            //alanın tekrar etmesini engelle
            var data = pList.Select(h => h.ManagerId).Distinct();

            //bugun satış yapan çalışanları seçmek istiyoruz. Bu durumda birden fazla satış yapan çalışanlar tekrarlı gelecektir. Bunun tekrarını engellemek için distinct atılabilir. 



            //seçilden 2 tane varsa 1 seçil al
            //tablolarda inner join işlemlerin tekrarlı nesneler gelebilir. Tekrarlı nesneleri tekilleştirmek için IEqualityComparer interface kullanarak iki nesnenin birbirinin aynısı olduğunu belirtmeliyiz. PersonEquality sınıf açarak IEqualityComparer interface den implemente eder ve disitinct içerisinde tanımlarız.
            var data3 = pList.Distinct(new PersonEquality());


            Person s = new Person();
            s.FirstName = "Ali";
            s.SurName = "Can";
            s.Id = 1;
            s.ManagerId = 1;

            Person s1 = new Person();
            s1.FirstName = "Ali";
            s1.SurName = "Can";
            s1.Id = 1;
            s1.ManagerId = 1;


        }

        public class PersonEquality : IEqualityComparer<Person>
        {

            public bool Equals(Person x, Person y)
            {
                if (x.Id == y.Id && x.FirstName == y.FirstName && x.SurName == y.SurName && x.ManagerId == y.ManagerId)
                {
                    return true;
                }

                return false;
            }

            public int GetHashCode(Person obj)
            {
                return obj.FirstName.GetHashCode() ^ obj.SurName.GetHashCode() ^ obj.ManagerId.GetHashCode() ^ obj.Id.GetHashCode();
            }
        }
    }
}
