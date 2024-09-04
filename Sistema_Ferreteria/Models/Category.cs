using System;

namespace Sistema_Ferreteria.Models
{
    class Category
    {
        private int id;
        private string name;
        private DateTime creationDate;
        private DateTime updateDate;

        // Constructor 
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.creationDate = DateTime.Now; 
            this.updateDate = DateTime.Now; 
        }

        // Propiedades
        public int Id
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

        // Método para actualizar la categoría
        public void Update(string newName)
        {
            this.name = newName;
            this.updateDate = DateTime.Now; // Actualiza la fecha al momento de la modificación
        }
    }
}