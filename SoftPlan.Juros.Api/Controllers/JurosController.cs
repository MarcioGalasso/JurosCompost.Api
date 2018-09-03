using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftPlan.Juros.Domain.Service.Interface;

namespace Softplan.Api.Controllers
{
    [Authorize(Policy = "AuthorizationServer")]
    [Route("api/[controller]")]
    [ApiController]
    public class JurosController : ControllerBase
    {
        IJurosService jurosService;

        public JurosController(IJurosService jurosService)
        {
            this.jurosService = jurosService;
        }

        /// <summary>
        /// Serviço para calculo de juros composto
        /// </summary>
        /// <param name="valorInicial">Valor Inicial</param>
        /// <param name="meses">Periodo de tempo</param>
        /// <returns></returns>
        [HttpGet("calculajuros")]
        public ActionResult CalcularJuros(double valorInicial, int meses)
        {
            return Ok(jurosService.CalcularJuros(valorInicial, meses));
        }

        /// <summary>
        /// Serviço que forneçe url do projeto no gitHub
        /// </summary>
        /// <returns></returns>
        [HttpGet("showmethecode")]
        public ActionResult<string> Showmethecode()
        {
            return jurosService.Showmethecode();
        }


    }
}
