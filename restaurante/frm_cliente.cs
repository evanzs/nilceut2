﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using restaurante.Tabelas;

namespace restaurante
{
    public partial class frm_cliente : Form
    {
        bool novo = true;
        List<Cliente> resC = new List<Cliente>();
        Cliente regAtual = new Cliente();
        public frm_cliente()
        {
            InitializeComponent();
        }

        private string RetornaCpf()
        {
            string[] cs = txtCpf.Text.Split(".-".ToArray());
            string c = "";
            for (int i = 0; i < 4; i++)
            {
                c += cs[i];
            }
            return c;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            regAtual.nome = txtNome.Text;
            regAtual.endereco = txtEnd.Text;
            regAtual.telefone = txtFone.Text;
            if (novo && regAtual.cpf == "")
            {
                regAtual.Definir_Cpf(RetornaCpf());
                if (CRUD.InsereLinha("Cliente", Cliente.Campos(), regAtual.ListarValores()) > 0)
                    InformaDiag.InformaSalvo();
            }
            else
            {
                if (CRUD.UpdateLine("Cliente", Cliente.Campos(), regAtual.ListarValores(), "Cpf='" + regAtual.cpf + "'") > 0)
                    InformaDiag.InformaSalvo();
            }
            novo = false;
        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {
            CRUD.ApagaLinha("Cliente", "Cpf='" + regAtual.cpf + "'");
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            txtCpf.Text = "";
            txtEnd.Text = "";
            txtFone.Text = "";
            txtNome.Text = "";
            novo = true;
            regAtual.Definir_Cpf("");
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string psq = RetornaCpf();
            if (psq.Length == 11)
            {
                resC = Cliente.ConverteObject(CRUD.SelecionarTabela("Cliente", Cliente.Campos(), "Cpf='" + psq + "'"));
                if (resC.Count() > 0)
                {
                    regAtual = resC.First();
                    MostraDados();
                    novo = false;
                }
            }
        }

        private void MostraDados()
        {
            txtCpf.Text = regAtual.cpf;
            txtNome.Text = regAtual.nome;
            txtEnd.Text = regAtual.endereco;
            txtFone.Text = regAtual.telefone;
        }
    }
}
