using System;

namespace Sistema_Ferreteria.Models
{
    public class Category
    {
        private string id;
        private string name;
        private DateTime creationDate;
        private DateTime updateDate;

        // Constructor que genera un nuevo ID
        public Category(string name)
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.creationDate = DateTime.Now;
            this.updateDate = DateTime.Now;
        }

        // Constructor que recibe un ID
        public Category(string id, string name, DateTime creationDate, DateTime updatedDate)
        {
            this.id = id;
            this.name = name;
            this.creationDate = creationDate;
            this.updateDate = updatedDate;
        }

        // Constructor por defecto
        public Category() { }

        // Propiedades
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }


        public void Update(string newName)
        {
            this.name = newName;
            this.updateDate = DateTime.Now;
        }
    }
}