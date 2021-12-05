using Kursovaia_vet.Models;
using Kursovaiy_vet_DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaia_vet.Controllers
{
    public class HomeController : Controller
    {
        //Автоматически сгенерированный фреймворком код
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Обработчик GET-запроса, позволяющий вернуть представление главной страницы
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Обработчик для POST-запроса, который выполняет проверку при нажатии на кнопку Log in
        [HttpPost]
        public IActionResult Index(string login, string pass)
        {
            //Обработчик try-catch позволяет вывести ошибку через alert, если в базе данных не найдется соответствующих логина и пароля
            try
            {
                using (var context = new ApplicationContext())
                {
                    //Пытаемся получить из БД логин и пароль, с помощью LINQ запроса к EntityFramework
                    //При первом совпадении поиск прекращается [метод First()]

                    //Обращаемся к контексту, к таблице Personals, запрашивая первое совпадение по правилу
                    //"Login записи таблицы БД == login, который получен от представления"
                    var currentLogin = context.Personals.Where(x => x.Login == login).First();
                    var currentPass = context.Personals.Where(x => x.Pass == pass).First();
                    //Если в БД нашлись как подходящий логин, так и подходящий пароль, то перенаправляем пользователя на страницу Table
                    return RedirectPermanent("/Home/Table");
                }
            }
            catch
            {
                //Если не найден логин или пароль, то записываем в TempData текст, который необходимо отобразить в alert и перезагружаем представление
                TempData["alert"] = "Login or Password is not registered!";
                return View();
            }
        }
        /* Пример расширенного правила для LINQ
        bool Pravilo(Personal x)
        {
            if (x.Login == "полученный от представления Login (введенный пользователем)") {
                return true;
            }
            else { return false; }
        } */
        

        //Добавление записи на прием к врачу в базу данных
        public IActionResult AddPet(string name, double age, string breed, string ownersname, string diagnosis)
        {
            //Создание файла связи (труба)
            //Using нужен для уничтожения трубы после использования
            try
            {
                using (var context = new ApplicationContext())
                {
                    var currentBreed = context.Breeds.Where(x => x.BreedName == breed).First();
                    var currentOwner = context.Owners.Where(x => x.SecondName == ownersname).First();
                    var currentDiagnos = context.Diagnoses.Where(x => x.DiagnosName == diagnosis).First();
                    var pet = new Pet()
                    {
                        Name = name,
                        Age = age,
                        BreedName = currentBreed,
                        Owner = currentOwner,
                        DiagnosName = currentDiagnos
                    };
                    context.Add(pet);
                    context.SaveChanges();
                }
                TempData["alert"] = "Pet added successfully!!";
                return RedirectPermanent("/Home/Registr");
            }
            catch
            {
                TempData["alert"] = "Breed, owner or diagnosis is not registered!";
                return RedirectPermanent("/Home/Registr");
            }
        }
        public IActionResult Adding()
        {
            return RedirectPermanent("/Home/Registr");
        }

        
        public IActionResult Registr()
        {
            List<Breed> breeds = new List<Breed>();
            List<Owner> owners = new List<Owner>();
            List<Diagnosis> diagnoses = new List<Diagnosis>();

            using (var context = new ApplicationContext())
            {
                //Загружаем из таблиц БД данные в ранее созданные списки
                breeds = context.Breeds.ToList();
                owners = context.Owners.ToList();
                diagnoses = context.Diagnoses.ToList();
            }
            //Объединяем три полученных списка (с породами, клиентами и диагнозами) в один объект и передаем его в качестве модели представления
            TableForBreedOwnerAndDiagnos tableModel = new TableForBreedOwnerAndDiagnos(breeds, owners, diagnoses);
            return View(tableModel);
        }

        public IActionResult AddingBreed(string breedname, string info)
        {
            using (var context = new ApplicationContext())
            {
                var breed = new Breed()
                {
                    BreedName = breedname,
                    Information = info
                };
                context.Add(breed);
                context.SaveChanges();
            }
            TempData["alert"] = "Breed added successfully!";
            return RedirectPermanent("/Home/Registr");
        }

        public IActionResult AddingOwner(string firstname, string secondname, string telefon)
        {
            using (var context = new ApplicationContext())
            {
                var owner = new Owner()
                {
                    FirstName = firstname,
                    SecondName = secondname,
                    Telefon = telefon
                };
                context.Add(owner);
                context.SaveChanges();
            }
            TempData["alert"] = "Owner added successfully!";
            return RedirectPermanent("/Home/Registr");
        }

        public IActionResult AddingDiagnosis(string diagnosname, string diagnosseverity)
        {
            using (var context = new ApplicationContext())
            {
                var diagnosis = new Diagnosis()
                {
                    DiagnosName = diagnosname,
                    SeverityDiagnosis = diagnosseverity
                };
                context.Add(diagnosis);
                context.SaveChanges();
            }
            TempData["alert"] = "Diagnos added successfully!";
            return RedirectPermanent("/Home/Registr");
        }


        //Создание веб-страницы с таблицей зверей из БД
        public IActionResult Table()
        {
            //Тип Pet берем из DTO
            List<Pet> Pets = new List<Pet>();
            using (var context = new ApplicationContext()) 
            {
                //Includ'ы необходимы, чтобы подгрузить данные из связанных с таблицей Pets таблиц
                Pets = context.Pets.Include(x => x.DiagnosName).Include(x => x.BreedName).Include(x => x.Owner).ToList();
            }
            return View(Pets);
        }

        //Удаление выделенной строки
        public IActionResult DelPet(bool[] isChecked, int[] id)
        {
            using (var context = new ApplicationContext())
            {
                for (int i = 0; i < id.Length; i++){
                    if (isChecked[i])
                    {
                        //Метод внутри метода. Удалить диапазон объектов подходящих под условие в скобках
                        context.Pets.RemoveRange(context.Pets.Where(x => x.Id == id[i]));
                    }
                }
                context.SaveChanges();
            }
            return RedirectPermanent("/Home/Table");
        }
        
        //Автоматически сгенерированный фреймворком код
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
