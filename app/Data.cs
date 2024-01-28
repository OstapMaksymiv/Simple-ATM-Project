using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Data
    {
        private DateTime date;

        public Data()
        {
            this.date = DateTime.Now;
        }

        public DateTime PobierzDate()
        {
            return date;
        }

        public void PrzesunNaprzod()
        {
            date = date.AddDays(7);
        }

        public void PrzesunDoTylu()
        {
            date = date.AddDays(-14);
        }
    }
}
