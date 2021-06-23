using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Volvo.Models;


namespace Volvo.Controllers
{
    public class CaminhaoController : Controller
    {
       
        //Construtor vazio para possibilitar o acesso dos testes unitários
        //Retirada a injeção de dependencia e instanciado o DBContext  Models.Caminhaocontext
        public CaminhaoController()
        {

        }

        //Método que aquisita e ordena a lista de caminhoes e retorna a view index com a lista 
        //Teste executado verifica se a view é retornada corretamente
        public async Task<IActionResult> Index(string? erro)
        {
            if (erro == null)
            {
                ViewBag.erro = "";
            }
            else
            {
                ViewBag.erro = erro;
            }
            var _context = new CaminhaoContext();
            return View(await _context.Caminhao.OrderByDescending(x => x.Id).ToListAsync());
        }

        //Método que aquisita um objeto Caminhao e retorna a view Details 
        //Teste executado id nulo
        //Teste executado id inexistente
        public async Task<IActionResult> Details(int? id)
        {
            var _context = new CaminhaoContext();
            var caminhao = await _context.Caminhao.FirstOrDefaultAsync(m => m.Id == id);
            if (caminhao != null)
                return View(caminhao);
            else
                throw new Exception("id nulo ou inexistente");
            throw new Exception("id nulo ou inexistente");
        }

        //Método que aquisita um objeto Caminhao e retorna a view Create 
        //As ViewsBags irão popular os dropdownlists na view, uma para os anomodelos permitidos e outra para os modelos permitidos
        //Teste executado verifica se a view é retornada corretamente
        public IActionResult Create(string? erro)
        {
            if(erro == null)
            {
                ViewBag.erro = "";
            }
            else
            {
                ViewBag.erro = erro;
            }
            
            try
            {
                ModeloCaminhao m = new ModeloCaminhao();
                ViewBag.Modelo = new SelectList(m.GetModelos(), "Modelo", "Modelo");
                AnoModeloCaminhao a = new AnoModeloCaminhao();
                ViewBag.AnoModelo = new SelectList(a.GetAnosModelos(), "AnoModelo", "AnoModelo");
                return View("Create");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { erro = "ERRO, TENTE NOVAMENTE" });
            }

        }

        //Método que insere um objeto Caminhao e retorna a view Index 
        //As ViewsBags irão popular os dropdownlists na view, uma para os anomodelos permitidos e outra para os modelos permitidos
        //Teste executado modelo FH ou FM obrigatório
        //Teste executado anomodelo diferente 0
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modelo,Ano,AnoModelo,Chassi")] Caminhao caminhao)
        {
            if (caminhao.Modelo != "FH" && caminhao.Modelo != "FM")
                return RedirectToAction("Create", new { erro = "MODELO NÃO PERMITIDO" });

            if (caminhao.AnoModelo == 0)
                return RedirectToAction("Create", new { erro = "ANO DO MODELO NÃO PERMITIDO" });

            if (ModelState.IsValid)
            {     
                try
                {
                    var _context = new CaminhaoContext();
                    _context.Add(caminhao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Create", new { erro = ex.ToString() });
                }               
            }
            else
            {
                return RedirectToAction("Create", new { erro = "OCORREU UM ERRO TENTE NOVAMENTE" });
            }

        }


        //Método que aquisita um objeto Caminhao e retorna a view Edit 
        //As ViewsBags irão popular os dropdownlists na view, uma para os anomodelos permitidos e outra para os modelos permitidos
        //Teste executado id inexistente
        //Teste executado id 0
        public async Task<IActionResult> Edit(int? id, string? erro)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", new { erro = "ERRO NO ID DO CAMINHÃO" });
            }

            if (erro == null)
            {
                ViewBag.erro = "";
            }
            else
            {
                ViewBag.erro = erro;
            }            

            var _context = new CaminhaoContext();  
            
            try
            {
                var caminhao = await _context.Caminhao.FindAsync(id);
                if(caminhao==null)
                    return RedirectToAction("Index", new { erro = "ERRO NO ID DO CAMINHÃO" });

                ModeloCaminhao m = new ModeloCaminhao();
                ViewBag.Modelo = new SelectList(m.GetModelos(), "Modelo", "Modelo", caminhao.Modelo);
                AnoModeloCaminhao a = new AnoModeloCaminhao();
                ViewBag.AnoModelo = new SelectList(a.GetAnosModelos(), "AnoModelo", "AnoModelo", caminhao.AnoModelo);

                return View(caminhao);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { erro = "ERRO NO ID DO CAMINHÃO" });
            }           
        }


        //Método que edita um objeto Caminhao e retorna a view Index         
        //Teste executado id false
        //Teste executado caminhao.id falso
        //Teste executado modelo FH ou FM obrigatório
        //Teste executado anomodelo diferente 0
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Ano,AnoModelo,Chassi")] Caminhao caminhao)
        {
            if (caminhao.Modelo != "FH" && caminhao.Modelo != "FM")
                return RedirectToAction("Edit", new { erro = "MODELO NÃO PERMITIDO" });

            if (caminhao.AnoModelo == 0)
                return RedirectToAction("Edit", new { erro = "ANO DO MODELO NÃO PERMITIDO" });

            CaminhaoContext _context = new CaminhaoContext();
            if (id != caminhao.Id)
            {
                return RedirectToAction("Edit", new { erro = "OCORREU UM ERRO, TENTE NOVAMENTE" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context = new CaminhaoContext();
                    _context.Update(caminhao);                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaminhaoExists(caminhao.Id))
                    {
                        return RedirectToAction("Edit", new { erro = "OCORREU UM ERRO, TENTE NOVAMENTE" });
                    }
                    else
                    {
                        return RedirectToAction("Edit", new { erro = "OCORREU UM ERRO, TENTE NOVAMENTE" });
                    }
                }               
            }
            
            return RedirectToAction(nameof(Index));
        }

        //Método que aquisita um objeto Caminhao e retorna a view Delete         
        //Teste executado id nulo
        //Teste executado id falso
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Objeto não encontrado");
            }

            var _context = new CaminhaoContext();
            var caminhao = await _context.Caminhao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caminhao == null)
            {
                throw new Exception("Objeto não encontrado");
            }

            return View(caminhao);
        }

        //Método que exclui um objeto Caminhao e retorna a view Index         
        //Teste executado id falso
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _context = new CaminhaoContext();
            try
            {
                var caminhao = await _context.Caminhao.FindAsync(id);
                _context.Caminhao.Remove(caminhao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw new Exception("Objeto não encontrado");
            }          
        }

        //Método que verifica se um objeto Caminhao existe      
        //Teste executado id falso       
        public bool CaminhaoExists(int id)
        {
            var _context = new CaminhaoContext();
            return _context.Caminhao.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
