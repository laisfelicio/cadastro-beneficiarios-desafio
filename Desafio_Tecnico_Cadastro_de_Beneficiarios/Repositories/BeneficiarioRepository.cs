using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Data;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Beneficiario;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories
{
    public class BeneficiarioRepository : IBeneficiarioRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BeneficiarioRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool BeneficiarioExiste(BeneficiarioCriacaoDto beneficiarioCriacaoDto)
        {
            return _context.Beneficiarios.Any(item => item.Cpf == beneficiarioCriacaoDto.Cpf);
        }

        public async Task<BeneficiarioModel> BuscarBeneficiariosPorId(int id)
        {
            try
            {
                var beneficiario = await _context.Beneficiarios.FindAsync(id);
                return beneficiario;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BeneficiarioModel> DeletarBeneficiario(int id)
        {
            try
            {
                var beneficiario = await _context.Beneficiarios.FindAsync(id);

                _context.Beneficiarios.Remove(beneficiario);
                await _context.SaveChangesAsync();

                return beneficiario;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BeneficiarioModel> EditarBeneficiarios(BeneficiarioEdicaoDto beneficiarioEdicaoDto)
        {
            try
            {
                var beneficiarioBanco = await _context.Beneficiarios.FindAsync(beneficiarioEdicaoDto.Id);

                beneficiarioBanco.NomeCompleto = beneficiarioEdicaoDto.NomeCompleto;
                beneficiarioBanco.Cpf = beneficiarioEdicaoDto.Cpf;
                beneficiarioBanco.DataNascimento = beneficiarioEdicaoDto.DataNascimento;
                beneficiarioBanco.Status = beneficiarioEdicaoDto.Status;

                _context.Update(beneficiarioBanco);
                await _context.SaveChangesAsync();

                return beneficiarioBanco;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BeneficiarioModel>> ListarBeneficiarios()
        {
            try
            {
                var beneficiarios = await _context.Beneficiarios.ToListAsync();
                return beneficiarios;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<BeneficiarioModel> CriarBeneficiario(BeneficiarioCriacaoDto beneficiarioCriacaoDto)
        {
            try
            {
                BeneficiarioModel beneficiario = _mapper.Map<BeneficiarioModel>(beneficiarioCriacaoDto);

                _context.Add(beneficiario);
                await _context.SaveChangesAsync();

                return await _context.Beneficiarios.Include(p => p.Plano).FirstOrDefaultAsync(p => p.Id == beneficiario.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
