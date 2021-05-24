namespace Knewin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("algoritimos")]
    [ApiController]
    [AllowAnonymous]
    public class AlgoritimosController : Controller
    {

        /// POST /algoritimos/DuplicadosNaLista
        /// <summary>
        /// Verifica se existe valores duplicados em lista informada
        /// e retorna o indice do primeiro valor repetido.
        /// </summary>
        /// <remarks>
        /// Não necessita de Autenticação
        /// </remarks>        
        /// <param name="listaInteiros"></param>
        /// <returns>indice do primeiro valor duplicado</returns>
        [HttpPost]
        [Route("DuplicadosNaLista")]
        public ActionResult Duplicados(int[] listaInteiros)
        {

            if (listaInteiros == null || listaInteiros.Length == 0)
                return Json(new { resultado = "Lista nao informada ", index = 0 });


            for (int i = 0; i <= listaInteiros.Length; i++)
                for (int j = i + 1; j < listaInteiros.Length; j++)
                    if (listaInteiros[j] == listaInteiros[i])
                        return Json(new { indice = i });
                    
 
            return Json(new { resultado = "Sem Valor Duplicado" });
        }

        /// GET /algoritimos/Palindromo
        /// <summary>
        /// Verifica se a palavra informada é Palindromo
        /// </summary>
        /// <remarks>
        /// Não necessita de Autenticação
        ///
        /// Definição: Um palindromo é um string que pode ser lida da mesma forma de trás para frente.
        /// Por exemplo, "abcba" ou "arara" é um palindromo.
        ///
        /// </remarks>        
        /// <param name="palavra"></param>
        /// <returns>Palindromo: true  ou Palindromo: false</returns>
        [HttpGet("Palindromo/{palavra}")]
        public ActionResult Palindromo(string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
                return Json(new { resultado = "Palavra nao informada" });

            return Json(new{ Palindromo = palavra.Equals(new string(palavra.Reverse().ToArray()))});
        }
    }
}