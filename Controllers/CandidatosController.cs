using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aec_mvc_entity_framework.Models;
using aec_mvc_entity_framework.Services;
using Microsoft.Data.SqlClient;

namespace aec_mvc_entity_framework.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly DbContexto _context;

        public CandidatosController(DbContexto context)
        {
            _context = context;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            var dbContexto = _context.Candidatos.Include(c => c.Profissao);
            return View(await dbContexto.ToListAsync());
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .Include(c => c.Profissao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao");
            return View();
            
            
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Nome,Nascimento,Email,Telefone,ProfissaoId,Logradouro,Complemento,Numero,Bairro,Cidade,Cep,Estado")] Candidato candidato)
        {                   
                if (ModelState.IsValid)
                {
                    var mensagem = "";

                    try{                    
                        _context.Add(candidato);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (SqlException){
                        mensagem += "ddds";
                    }
                    catch(DbUpdateException){
                        mensagem += "ddds";

                        //return BadRequest("DCDB");                        
                        
                    }
                
                    ViewBag.Erro = mensagem;
                    ViewBag.Candidato = candidato;
                    return View("Create");

                }
                ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", candidato.ProfissaoId);
                return View(candidato);           
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", candidato.ProfissaoId);
            return View(candidato);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Nome,Nascimento,Email,Telefone,ProfissaoId,Logradouro,Complemento,Numero,Bairro,Cidade,Cep,Estado")] Candidato candidato)
        {
            if (id != candidato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoExists(candidato.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissaoId"] = new SelectList(_context.Profissoes, "Id", "Descricao", candidato.ProfissaoId);
            return View(candidato);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .Include(c => c.Profissao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidato = await _context.Candidatos.FindAsync(id);
            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoExists(int id)
        {
            return _context.Candidatos.Any(e => e.Id == id);
        }
    }
}
