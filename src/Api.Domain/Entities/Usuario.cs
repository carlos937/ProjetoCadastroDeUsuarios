﻿
using Domain.Helpers;
using System;

namespace Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string nome { get; private set; }
        public string email { get; private set; }
        public string senha { get; private set; }

        private Usuario()
        {

        }

        public Usuario(string nome, string email, string senha)
        {
            this.nome = nome;
            this.email = email;
            this.senha = senha;
        }

        public void setNome(string nome)
        {
            if (!String.IsNullOrEmpty(nome))
            {
                this.nome = nome;
            }
            else
            {
                throw new Exception("O nome não pode ser vazio");
            }
        }
        public void setEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                this.email = email;
            }
            else
            {
                throw new Exception("O email não pode ser vazio");
            }
        }
        public void setSenha(string senha)
        {
            if (!String.IsNullOrEmpty(senha))
            {

                try
                {
                    if (!String.IsNullOrEmpty(HelperCryptografiaRSA.decrypt(senha).Result))
                    {
                        this.senha = senha;
                    }
                    else
                    {
                        throw new Exception("Formato de senha invalido.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Formato de senha invalido: "+ ex.Message);
                }
               
            }
            else
            {
                throw new Exception("A senha não pode ser vazia");
            }
        }

        public bool verificarSeASenhaEIgual(string senha)
        {
           if(HelperCryptografiaRSA.decrypt(this.senha).Result
               == HelperCryptografiaRSA.decrypt(senha).Result)
            {
                return true;
            }
            return false;
        }
    }
}
