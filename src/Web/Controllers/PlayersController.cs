using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.Models.Enums;
using Web.ViewModels;

namespace Web.Controllers;

[Authorize(Roles = Constants.Administrator)]
public class PlayersController : Controller
{
    private readonly IPlayersService _playersService;

    public PlayersController(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> Index(SortState sortBy = SortState.Classical, bool ascending = false)
    {
        ViewData["Ascending"] = !ascending;

        return View(new IndexViewModel
        {
            Players = await _playersService.GetPlayers(sortBy, ascending),
            SortViewModel = new SortViewModel
            {
                CurrentState = sortBy,
                CurrentStateAscending = ascending
            }
        });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> Details(int id)
    {
        return View(await _playersService.GetPlayer(id));
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        return View(await _playersService.GetPlayer(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(PlayerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _playersService.UpdatePlayer(model);
        return RedirectToAction(nameof(Details), new { model.Id });
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View(new PlayerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(PlayerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var id = await _playersService.CreatePlayer(model);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        await _playersService.DeletePlayer(id);
        return RedirectToAction(nameof(Index));
    }
}
