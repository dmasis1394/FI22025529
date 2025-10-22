using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;
        if (ViewBag.Valid)
        {
            // Usar LINQ para filtrar caracteres (excluir espacios)
            var characters = model.Phrase.Where(c => c != ' ').ToList();
            
            // Procesar cada caracter sin espacios
            characters.ForEach(c =>
            {
                if (!model.Counts!.ContainsKey(c))
                {
                    model.Counts[c] = 0;
                }
                model.Counts[c]++;
            });

            // Construir Lower y Upper sin espacios usando LINQ
            model.Lower = new string(characters.Select(c => char.ToLower(c)).ToArray());
            model.Upper = new string(characters.Select(c => char.ToUpper(c)).ToArray());
        }
        return View(model);
    }
}