using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Beneficiario;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories.Interface
{
    public interface IBeneficiarioRepository
    {
        Task<BeneficiarioModel> CriarBeneficiario(BeneficiarioCriacaoDto beneficiarioCriacaoDto);
        Task<List<BeneficiarioModel>> ListarBeneficiarios();
        Task<BeneficiarioModel> BuscarBeneficiariosPorId(int id);
        Task<BeneficiarioModel> EditarBeneficiarios(BeneficiarioEdicaoDto beneficiarioEdicaoDto);
        Task<BeneficiarioModel> DeletarBeneficiario(int id);
        bool BeneficiarioExiste(BeneficiarioCriacaoDto beneficiarioCriacaoDto);
    }
}
