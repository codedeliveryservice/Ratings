using Web.Models.Enums;
using Web.ViewModels;

namespace Web.Interfaces;

public interface IPlayersService
{
    Task<PlayerViewModel> GetPlayer(int id);
    Task<IReadOnlyList<PlayerViewModel>> GetPlayers(SortState sortState, bool ascending);
    public Task<int> CreatePlayer(PlayerViewModel model);
    public Task UpdatePlayer(PlayerViewModel model);
    public Task DeletePlayer(int id);
}