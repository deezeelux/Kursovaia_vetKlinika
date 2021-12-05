using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    //Модель таблицы персонала клиники для EntityFramework
    public class Personal
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
    }
}
