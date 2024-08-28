using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Ferreteria.Models
{
    internal class Category
    {
        private int id;
        private string name;
        private string creationDate;
        private string updateDate;

        public int Id { 
            get { return id; } 
            set {    id = value; } 
        }
        public string Name {
            get { return name; }
            set { name = value; } 
        }
        public string CreationDate { 
            get { return creationDate; } 
            set {    creationDate = value; } 
        }
        public string UpdateDate { 
            get { return updateDate; }
            set { updateDate = value; }
        }
    }
}
