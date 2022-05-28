using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Web.Data;
using Web.Exceptions;
using Web.Interfaces;
using Web.Models;
using Web.Models.Enums;
using Web.ViewModels;

namespace Web.Services;

public class PlayersService : IPlayersService
{
    private readonly ApplicationDbContext _context;
    private readonly IImageStorageService _imageStorageService;
    private readonly IMapper _mapper;

    public PlayersService(ApplicationDbContext context, IImageStorageService imageStorageService, IMapper mapper)
    {
        _context = context;
        _imageStorageService = imageStorageService;
        _mapper = mapper;
    }

    public async Task<PlayerViewModel> GetPlayer(int id)
    {
        var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        if (player is null)
        {
            throw new NotFoundException(nameof(Player), id);
        }

        return _mapper.Map<PlayerViewModel>(player);
    }

    public async Task<IReadOnlyList<PlayerViewModel>> GetPlayers(SortState sortState, bool ascending)
    {
        Expression<Func<Player, dynamic>> sortExpression = sortState switch
        {
            SortState.Name => (x => x.Name),
            SortState.Country => (x => x.Country),
            SortState.Classical => (x => x.ClassicalRating),
            SortState.Rapid => (x => x.RapidRating),
            SortState.Blitz => (x => x.BlitzRating),
            _ => (x => x.ClassicalRating)
        };

        var query = _context.Players.AsQueryable();

        query = (ascending == true)
            ? query.OrderBy(sortExpression)
            : query.OrderByDescending(sortExpression);

        var players = await query
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IReadOnlyList<PlayerViewModel>>(players);
    }

    public async Task<int> CreatePlayer(PlayerViewModel model)
    {
        var path = await _imageStorageService.UploadImage(model.FormFile);
        if (path is not null)
        {
            model.ImageUrl = path;
        }

        var entry = _context.Players.Add(_mapper.Map<Player>(model));
        await _context.SaveChangesAsync();

        return entry.Entity.Id;
    }

    public async Task UpdatePlayer(PlayerViewModel model)
    {
        var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (player is null)
        {
            throw new NotFoundException(nameof(Player), model.Id);
        }

        var path = await _imageStorageService.UploadImage(model.FormFile);
        if (path is not null)
        {
            model.ImageUrl = path;
        }
        else
        {
            // NOTE: Set the previous image in case the new one hasn't been uploaded
            model.ImageUrl = player.ImageUrl;
        }

        _mapper.Map(model, player);

        await _context.SaveChangesAsync();
    }

    public async Task DeletePlayer(int id)
    {
        var entry = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
        if (entry is null)
        {
            // NOTE: Consider throwing an exception
            return;
        }

        // NOTE: Images are still persisted,
        // it might be better to hide the entity rather than remove it completely
        _context.Players.Remove(entry);

        await _context.SaveChangesAsync();
    }
}