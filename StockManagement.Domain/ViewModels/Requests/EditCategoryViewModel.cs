using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.ViewModels.Requests;

public class EditCategoryViewModel
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }
}