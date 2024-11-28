using System.Text.RegularExpressions;

namespace Fase4.Contato.Core.Entities;

public class AssertionConcern
    {
        /// <summary>
        /// Validação de tamanho máximo de string
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="maximum"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// Validação de tamanho minimo e máximo (precisa estar entre os dois)
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// Validação de string se esta vazia
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// Validação se objeto é null
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }

        /// <summary>
        /// Validação se o email é válido
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AssertEmailIsValid(string email, string message)
        {
            if (!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                throw new DomainException(message);
        }

        /// <summary>
        /// Validação se o ddd é válido
        /// </summary>
        /// <param name="ddd"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AssertDDDIsValid(string ddd, string message)
        {
            var dddInt = int.Parse(ddd);
            //var dddInt = int.Parse(ddd.Remove(0,1));

            if (dddInt > 99 || dddInt < 11)
                throw new DomainException(message);
        }

        /// <summary>
        /// Validação se o telefone é válido
        /// </summary>
        /// <param name="ddd"></param>
        /// <param name="message"></param>
        /// <exception cref="DomainException"></exception>
        public static void AssertTelefoneIsValid(string telefone, string message)
        {
            string padraoTelefone = "^(?:((?:9\\d|[2-9])\\d{3})\\-?(\\d{4}))$";

            if (!Regex.Match(telefone, padraoTelefone).Success)
                throw new DomainException(message);
        }
    }