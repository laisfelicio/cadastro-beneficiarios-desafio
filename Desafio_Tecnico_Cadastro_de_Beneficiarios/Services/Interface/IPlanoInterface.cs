using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Services.Interface
{
    public interface IPlanoInterface
    {
        Task<ResponseModel<PlanoModel>> CriarPlano(PlanoCriacaoDto planoCriacaoDto);
        Task<ResponseModel<PlanoModel>> EditarPlano(PlanoEdicaoDto planoEdicaoDto);
        Task<ResponseModel<PlanoModel>> DeletarPlano(int id);
        Task<ResponseModel<PlanoModel>> BuscarPlanoPorId(int id);
       Task<ResponseModel<List<PlanoModel>>> ListarPlanos();
    }
}
