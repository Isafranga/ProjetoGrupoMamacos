using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMacacos.Data;
using ProjetoMacacos.Models;

namespace ProjetoMacacos.Views
{
    public class Veiculoes1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Veiculoes1Controller(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Veiculoes1

        public async Task<IActionResult> Index()
        {
            var veiculosDisponiveis = await _context.Veiculos
                .Where(v => v.Disponivel == true)
                .ToListAsync();

            return View(veiculosDisponiveis);
        }


        // GET: Veiculoes1/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(m => m.VeiculoId == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculoes1/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculoes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeiculoId,Placa,Modelo,Marca,Ano,Cor,UrlFoto")] Veiculo veiculo, IFormFile? UrlFoto)
        {
            if (ModelState.IsValid)
            {
                if (UrlFoto != null && UrlFoto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Photos");

                    var fileExtension = Path.GetExtension(UrlFoto.FileName);
                    var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        uniqueFileName = $"{Guid.NewGuid()}_{fileExtension}";
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    }

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await UrlFoto.CopyToAsync(fileStream);
                    }

                    veiculo.UrlFoto = Path.Combine("Resources", "Photos", uniqueFileName).Replace("\\", "/");
                }

                _context.Add(veiculo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Veiculoes1");
            }

            return View(veiculo);
        }

        // GET: Veiculoes1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculoes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VeiculoId,Placa,Modelo,Marca,Ano,Cor,UrlFoto")] Veiculo veiculo, IFormFile? UrlFoto)
        {
            if (id != veiculo.VeiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(u => u.VeiculoId == id);
                    if (usuarioExistente == null)
                        return NotFound();


                    if (UrlFoto != null && UrlFoto.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Photos");
                        var fileExtension = Path.GetExtension(UrlFoto.FileName);
                        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await UrlFoto.CopyToAsync(fileStream);
                        }

                        veiculo.UrlFoto = Path.Combine("Resources", "Photos", uniqueFileName).Replace("\\", "/");
                    }

                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculosExists(veiculo.VeiculoId))
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
            return View(veiculo);
        }

        private bool VeiculosExists(Guid id)
        {
            return _context.Veiculos.Any(e => e.VeiculoId == id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var registro = await _context.Registros.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (registro != null && registro.UserId == user.Id)
            {
                // ✅ Libera o veículo novamente
                var veiculo = await _context.Veiculos.FindAsync(registro.VeiculoId);
                if (veiculo != null)
                {
                    veiculo.Disponivel = true;
                    _context.Update(veiculo);
                }

                _context.Registros.Remove(registro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool VeiculoExists(Guid id)
        {
            return _context.Veiculos.Any(e => e.VeiculoId == id);
        }

        public IActionResult BuscarFoto(Guid id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null || string.IsNullOrEmpty(veiculo.UrlFoto))
                return NotFound();

            // Monta o caminho físico absoluto a partir do diretório base do projeto
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), veiculo.UrlFoto.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (!System.IO.File.Exists(caminho))
                return NotFound();

            var contentType = "image/*";
            return PhysicalFile(caminho, contentType);
        }
    }
}
