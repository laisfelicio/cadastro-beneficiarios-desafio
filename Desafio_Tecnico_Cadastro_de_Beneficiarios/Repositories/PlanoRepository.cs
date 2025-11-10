using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Data;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlanoRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PlanoModel> CriarPlano(PlanoCriacaoDto planoCriacaoDto)
        {
            try
            {
                PlanoModel plano = _mapper.Map<PlanoModel>(planoCriacaoDto);

                _context.Add(plano);
                await _context.SaveChangesAsync();

                var planoComBeneficiarios = await _context.Planos.Include(p => p.Beneficiarios).FirstOrDefaultAsync(p => p.Id == plano.Id);
                return planoComBeneficiarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<PlanoModel> DeletarPlano(int id)
        {
            try
            {
                var plano = await _context.Planos.FindAsync(id);

                _context.Planos.Remove(plano);
                await _context.SaveChangesAsync();

                return plano;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlanoModel> EditarPlano(PlanoEdicaoDto planoEdicaoDto)
        {
            try
            {
                var PlanoBanco = _context.Planos.Find(planoEdicaoDto.Id);

                PlanoBanco.Nome = planoEdicaoDto.Nome;
                PlanoBanco.Codigo_registro_ans = planoEdicaoDto.Codigo_registro_ans;

                _context.Planos.Update(PlanoBanco);
                await _context.SaveChangesAsync();

                var planoAtualizado = await _context.Planos.Include(p => p.Beneficiarios).FirstOrDefaultAsync(p => p.Id == PlanoBanco.Id);
                return planoAtualizado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PlanoModel>> ListarPlanos()
        {
            try
            {
                return await _context.Planos.ToListAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PlanoModel> BuscarPlanoPorId(int id)
        {
            try
            {
                var plano = await _context.Planos.FindAsync(id);
                return plano;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public bool ExistePlano(PlanoCriacaoDto planoCriacaoDto)
        {
            return _context.Planos.Any(item => item.Nome == planoCriacaoDto.Nome);
        }
    }
}
