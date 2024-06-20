using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;
using FI.WebAtividadeEntrevista.Util;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult IncluirBenef()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (bo.VerificarExistencia(Util.Util.RemoveCaracteresCpfCliente(model.CPF)))
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF já cadastrado!"));
                }
                else
                {
                    model.Id = bo.Incluir(new Beneficiario()
                    {
                        CPF = Util.Util.RemoveCaracteresCpfCliente(model.CPF),
                        Nome = model.Nome,
                        CpfCliente = model.CpfCliente
                    });
                }

                return Json(new { message = "Cadastro efetuado com sucesso", id = model.Id });
            }
        }

        [HttpPost]
        public JsonResult Excluir(long idBeneficiario)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Excluir(idBeneficiario);
                return Json("Exclusão efetuada com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(long idBeneficiario, string cpf, string nome)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(idBeneficiario, cpf, nome);
                return Json("Alteração efetuada com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Listar(string cpfCliente)
        {
            try
            {
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                List<Beneficiario> lstBenef = boBeneficiario.Listar(cpfCliente);

                return Json(new { Result = "OK", Records = lstBenef });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}