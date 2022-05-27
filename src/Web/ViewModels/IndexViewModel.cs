namespace Web.ViewModels;

public class IndexViewModel
{
    public IEnumerable<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
    public SortViewModel SortViewModel { get; set; } = new SortViewModel();
}