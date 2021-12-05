using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    //Модель таблицы пород животных для EntityFramework
    public class Breed
    {
        public int Id { get; set; }
        public string BreedName { get; set; }
        public string Information { get; set; }
    }
}
