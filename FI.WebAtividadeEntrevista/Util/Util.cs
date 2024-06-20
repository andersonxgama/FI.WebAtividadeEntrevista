using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FI.WebAtividadeEntrevista.Util
{
    public static class Util
    {
        public static string RemoveCaracteresCpfCliente(string cpf)
        {
            return Regex.Replace(cpf, "[.-]", "");
        }

        public static string FormataCPFCliente(string cpf)
        {
            string padrao = @"(\d{3})(\d{3})(\d{3})(\d{2})";
            
            return Regex.Replace(cpf, padrao, "$1.$2.$3-$4");
        }

        public static bool ValidarCPF(string cpf)
        {
            cpf = RemoveCaracteresCpfCliente(cpf);

            if(string.IsNullOrEmpty(cpf) || cpf.Length < 11)
            {
                return false;
            }

            if (TodosDigitosIguais(cpf))
            {
                return false;
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (digitoVerificador1 != int.Parse(cpf[9].ToString()))
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            if (digitoVerificador2 != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            return true;
        }

        public static bool TodosDigitosIguais(string cpf)
        {
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                    return false;
            }
            return true;
        }
    }
}