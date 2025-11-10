using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories.Interface
{
    public interface IPlanoRepository
    {
        Task<PlanoModel> CriarPlano(PlanoCriacaoDto planoCriacaoDto);
        Task<PlanoModel> EditarPlano(PlanoEdicaoDto planoEdicaoDto);
        Task<PlanoModel> BuscarPlanoPorId(int id);
        Task<PlanoModel> DeletarPlano(int id);
        Task<List<PlanoModel>> ListarPlanos();
        bool ExistePlano(PlanoCriacaoDto planoCriacaoDto);
    }
}
