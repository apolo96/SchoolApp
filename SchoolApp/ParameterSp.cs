using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp
{
    class ParameterSp
    {
        private string name;
        private object val;
        private DbType type;

        public ParameterSp(string name, object val, DbType type)
        {
            this.name = name;
            this.val = val;
            this.type = type;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public object Val
        {
            get { return val; }
            set { val = value; }
        }

        public DbType Type
        {
            get { return type; }
            set { val = value; }
        }
    }
}
