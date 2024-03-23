using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models;

namespace TechStore.Controllers;

public class ProductoController : Controller
{
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

        [HttpPost]
        public IActionResult Create([Bind("Nom,Descrip,Precio")] Producto producto)
{
    if (ModelState.IsValid)
    {
        producto.Igv = producto.Precio * 0.18;
        double sumTotal = producto.Igv + producto.Precio;

        StringBuilder messageBuilder = new StringBuilder();
        messageBuilder.AppendLine("Se registró el producto: ");
        messageBuilder.AppendLine(producto.Nom);
        messageBuilder.AppendLine("El igv del producto es:  " + producto.Igv);
        messageBuilder.AppendLine("Lo que debe pagar el consumidor por el producto es:  " + sumTotal);

        ViewData["Message"] = messageBuilder.ToString();

        return View("Index", producto);
    }

    return View("Index", producto);
}
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}