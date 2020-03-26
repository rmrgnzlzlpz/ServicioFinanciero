using Domain.Base;
using Domain.Entities;
using Domain.Contracts;

namespace Application
{
  public class CrearCuentaBancariaService
  {
    readonly IUnitOfWork _unitOfWork;

    public CrearCuentaBancariaService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
    {
      FinancialService cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Number == request.Number);
      if (cuenta != null) return new CrearCuentaBancariaResponse() { Message = $"El número de cuenta ya exite" };

      FinancialService cuentaNueva = new SavingsAccount(); //Debe ir un factory que determine que tipo de cuenta se va a crear
      cuentaNueva.Name = request.Name;
      cuentaNueva.Number = request.Number;
      _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
      _unitOfWork.Commit();
      return new CrearCuentaBancariaResponse() { Message = $"Se creó con exito la cuenta {cuentaNueva.Number}." };
    }



  }
  public class CrearCuentaBancariaRequest
  {
    public string Name { get; set; }
    public string AccountType { get; set; }
    public string Number { get; set; }
  }
  public class CrearCuentaBancariaResponse
  {
    public string Message { get; set; }
  }
}