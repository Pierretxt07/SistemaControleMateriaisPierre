using System.Diagnostics;
using Sistema_Controle_Materiais_Web_Pierre.Data;
using Microsoft.AspNetCore.Mvc;
using Sistema_Controle_Materiais_Web_Pierre.Models;

namespace Sistema_Controle_Materiais_Web_Pierre.Controllers;

public class HomeController : Controller
{
    private readonly BancoDados Banco;

    public HomeController(BancoDados banco)
    {
        Banco = banco;
    }

    public IActionResult Index()
    {
        List<Material> materiais = Banco.ListarMateriais();

        DashboardViewModel dashboard = new DashboardViewModel();

        dashboard.TotalMateriais = materiais.Count;

        foreach (Material material in materiais)
        {
            dashboard.TotalUnidades += material.Quantidade;

            if (material.Quantidade == 0)
            {
                dashboard.EstoqueZerado++;
            }
            else if (material.Quantidade <= material.EstoqueMinimo)
            {
                dashboard.EstoqueBaixo++;
            }
        }

        return View(dashboard);
    }
}
