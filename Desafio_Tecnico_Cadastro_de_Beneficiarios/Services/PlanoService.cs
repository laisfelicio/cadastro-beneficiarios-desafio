using AutoMapper;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Data;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Dto.Plano;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Models;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Repositories.Interface;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Services
{
    public class PlanoService : IPlanoInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPlanoRepository _planoRepository;


        public PlanoService(AppDbContext context, IMapper mapper, IPlanoRepository planoRepository)
        {
            _context = context;
            _mapper = mapper;
            _planoRepository = planoRepository;
        }

        public async Task<ResponseModel<PlanoModel>> CriarPlano(PlanoCriacaoDto planoCriacaoDto)
        {
            ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

            try
            {
                if (_planoRepository.ExistePlano(planoCriacaoDto))
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "Plano já criado";
                    response.Details.Add(new ValidacaoModel
                    {
                        Field = "id",
                        Rule = "não encontrado"
                    });

                    return response;
                }

                var planoComBeneficiarios = await _planoRepository.CriarPlano(planoCriacaoDto);

                response.Dados = planoComBeneficiarios;
                response.Mensagem = "Plano criado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
            
        }

        public async Task<ResponseModel<List<PlanoModel>>> ListarPlanos()
        {
            ResponseModel<List<PlanoModel>> response = new ResponseModel<List<PlanoModel>>();

            try
            {
                var beneficiarios = await _planoRepository.ListarPlanos();

                response.Dados = beneficiarios;
                response.Mensagem = "Planos listados com sucesso";
                return response;


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }
        public async Task<ResponseModel<PlanoModel>> BuscarPlanoPorId(int id)
        {
            ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

            try
            {
                var plano = await _planoRepository.BuscarPlanoPorId(id);

                if (plano == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "Plano não localizado";
                    response.Details.Add(new ValidacaoModel
                    {
                        Field = "id",
                        Rule = "not_found"
                    });

                    return response;
                }
                response.Dados = plano;
                response.Mensagem = "Beneficiário localizado com sucesso";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<PlanoModel>> DeletarPlano(int id)
        {
            ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

            try
            {
                var plano = await _planoRepository.BuscarPlanoPorId(id);

                if (plano == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "Plano não localizado";
                    response.Details.Add(new ValidacaoModel
                    {
                        Field = "id",
                        Rule = "não encontrado"
                    });
                    return response;

                }
                response.Dados = await _planoRepository.DeletarPlano(id);
                response.Mensagem = "Plano removido com sucesso";


                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<PlanoModel>> EditarPlano(PlanoEdicaoDto planoEdicaoDto)
        {
            ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

            try
            {
                var PlanoBanco = await _planoRepository.BuscarPlanoPorId(planoEdicaoDto.Id);

                if (PlanoBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Plano não localizado";
                    response.Error = "ValidationError";
                    response.Details.Add(new ValidacaoModel
                    {
                        Field = "id",
                        Rule = "não encontrado"
                    });
                    return response;
                }

                response.Dados = await _planoRepository.EditarPlano(planoEdicaoDto);
                response.Mensagem = "Plano editado com sucesso";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }
    }
}
