using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursovaiy_vet_DTO.Models;

namespace Kursovaia_vet.Models
{
    //Модель слоя представления, использование которой позволяет передать список всех пород, клиентов и диагнозов из БД
    //в HTML-страницу
    public class TableForBreedOwnerAndDiagnos
    {
        public List<Breed> Breeds { get; set; }
        public List<Owner> Owners { get; set; }
        public List<Diagnosis> Diagnoses { get; set; }
        //Конструктор класса, который вызывается при создании экземпляра данной модели
        public TableForBreedOwnerAndDiagnos(List<Breed> breeds, List<Owner> owners, List<Diagnosis> diagnoses)
        {
            Breeds = breeds;
            Owners = owners;
            Diagnoses = diagnoses;
        }
    }
}
