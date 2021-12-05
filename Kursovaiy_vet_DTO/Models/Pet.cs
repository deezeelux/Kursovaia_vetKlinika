using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    //Модель таблицы записей в клинику для EntityFramework
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        //Следующие две строки осуществляют связь между сущностями EntityFramework типа "один ко многим"
        public int OwnerId { get; set; } 
        public Owner? Owner { get; set; }
        //Следующие две строки осуществляют связь между сущностями EntityFramework типа "один ко многим"
        public int DiagnosisId { get; set; }
        public Diagnosis DiagnosName { get; set; }
        //Следующие две строки осуществляют связь между сущностями EntityFramework типа "один ко многим"
        public int BreedId { get; set; }
        public Breed BreedName { get; set; }
    }
}
