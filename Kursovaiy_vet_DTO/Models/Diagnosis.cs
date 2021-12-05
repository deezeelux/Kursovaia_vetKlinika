using System;
using System.Collections.Generic;
using System.Text;

namespace Kursovaiy_vet_DTO.Models
{
    //Модель таблицы диагнозов и причин посещения клиники для EntityFramework
    public class Diagnosis
    {
        public int Id { get; set; }
        public string DiagnosName { get; set; }
        public string SeverityDiagnosis { get; set; }
    }
}
