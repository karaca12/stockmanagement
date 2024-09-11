using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.ViewModels.Requests;

public class CreateCustomerViewModel
{
    [Required] public string Name { get; set; }

    [Required] public string Surname { get; set; }
}