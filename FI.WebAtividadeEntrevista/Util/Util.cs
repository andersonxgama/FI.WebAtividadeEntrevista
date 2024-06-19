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

            // Verifica se todos os dígitos são iguais, o que é inválido para CPF
            if (TodosDigitosIguais(cpf))
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verifica o primeiro dígito verificador
            if (digitoVerificador1 != int.Parse(cpf[9].ToString()))
            {
                return false;
            }

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verifica o segundo dígito verificador
            if (digitoVerificador2 != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            // Se passou por todas as verificações, o CPF é válido
            return true;
        }

        public static bool TodosDigitosIguais(string cpf)
        {
            // Verifica se todos os dígitos do CPF são iguais
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                    return false;
            }
            return true;
        }
    }
}