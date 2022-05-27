using Web.Models.Enums;

namespace Web.ViewModels;

public class SortViewModel
{
    public SortState CurrentState { get; set; }
    public bool CurrentStateAscending { get; set; }
}
