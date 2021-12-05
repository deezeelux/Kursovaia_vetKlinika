using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    //Модель таблицы клиентов клиники (владельцев животных) для EntityFramework
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Telefon { get; set; }
    }
}
