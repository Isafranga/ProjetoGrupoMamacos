using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMacacos.Data;
using ProjetoMacacos.Models;

public class RegistrosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public RegistrosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Registros
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var registros = _context.Registros
            .Include(r => r.Veiculo)
            .Where(r => r.UserId == user.Id);

        return View(await registros.ToListAsync());
    }

    // GET: Registros/Create
    public IActionResult Create(Guid? veiculoId)
    {
        if (veiculoId.HasValue)
        {
            var veiculo = _context.Veiculos.FirstOrDefault(v => v.VeiculoId == veiculoId);
            ViewBag.VeiculoSelecionado = veiculo;
        }

        return View();
    }
    // GET: Registros/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
            return NotFound();

        var clientes = await _context.Registros.FindAsync(id);
        if (clientes == null)
            return NotFound();

        // Popular dropdown de veículos
        ViewBag.VeiculoId = new SelectList(_context.Veiculos, "VeiculoId", "Modelo", clientes.VeiculoId);
        return View(clientes);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("RegistroId, Nome, VeiculoId, UserId, DataRetirada, DataDevolucao")] Registros registros)
    {
        if (id != registros.RegistroId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                registros.UserId = user.Id;

                _context.Update(registros);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrosExists(registros.RegistroId))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // Recarregar o dropdown se der erro
        ViewBag.VeiculoId = new SelectList(_context.Veiculos, "VeiculoId", "Modelo", registros.VeiculoId);
        return View(registros);
    }
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
            return NotFound();

        var clientes = await _context.Registros
            .Include(r => r.Veiculo)
            .FirstOrDefaultAsync(m => m.RegistroId == id);

        if (clientes == null)
            return NotFound();

        return View(clientes);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Registros registro)
    {
        var user = await _userManager.GetUserAsync(User);

        if (ModelState.IsValid && user != null)
        {
            registro.RegistroId = Guid.NewGuid();
            registro.UserId = user.Id;

            _context.Add(registro);

            var veiculo = await _context.Veiculos.FindAsync(registro.VeiculoId);
            if (veiculo != null)
            {
                veiculo.Disponivel = false;
                _context.Update(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.VeiculoId = new SelectList(_context.Veiculos, "VeiculoId", "Modelo", registro.VeiculoId);
        return View(registro);
    }

    // GET: Registros/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();

        var registro = await _context.Registros
            .Include(r => r.Veiculo)
            .FirstOrDefaultAsync(m => m.RegistroId == id);

        if (registro == null) return NotFound();

        // Garante que o usuário só possa deletar o próprio registro
        var user = await _userManager.GetUserAsync(User);
        if (registro.UserId != user.Id)
            return Forbid();

        return View(registro);
    }

    // POST: Registros/DeleteConfirmed
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var registro = await _context.Registros.FindAsync(id);
        var user = await _userManager.GetUserAsync(User);

        if (registro != null && registro.UserId == user.Id)
        {
            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
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
    private bool RegistrosExists(Guid id)
    {
        return _context.Registros.Any(e => e.RegistroId == id);
    }
}
