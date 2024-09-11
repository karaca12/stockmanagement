using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.ViewModels.Requests;

public class CreateCategoryViewModel
{
    [Required] public string Name { get; set; }
}